using System;
using System.Runtime.CompilerServices;

public abstract class TheaterBase
{
    public char[,] seats;
    protected int[,] pricingCategories;

    public TheaterBase(int rows, int columns, int[,] pricingCategories)
    {
        this.pricingCategories = pricingCategories;
        InitializeSeats();
    }

    protected void InitializeSeats()
    {
        seats = new char[pricingCategories.GetLength(0), pricingCategories.GetLength(1)];
        for (int i = 0; i < seats.GetLength(0); i++)
        {
            for (int j = 0; j < seats.GetLength(1); j++)
            {
                seats[i, j] = pricingCategories[i, j] == 0 ? ' ' : 'A';
            }
        }
    }

    // protected void UpdateSeatsArray(List<long> reservedSeats)
    // {
    //     for (int i = 0; i < seats.GetLength(0); i++)
    //     {
    //         for (int j = 0; j < seats.GetLength(1); j++)
    //         {
    //             int seatId = (i * seats.GetLength(1)) + j;
                
    //             if (reservedSeats.Contains(seatId))
    //             {
    //                 seats[i, j] = 'R';
    //             }
                
    //         }
    //     }
    // }

    public void DisplaySeats(long showId)
    {
        Console.Clear();
        List<long> reservedSeats = ReservationAccess.GetReservedSeatsByShowId(showId);

        foreach (long seatId in reservedSeats)
        {
            SeatsModel seat = SeatsLogic.GetById((int)seatId);
            int row = seats.GetLength(0) - seat.RowNumber;
            int col = seat.ColumnNumber - 1;

            if (row >= 0 && row < seats.GetLength(0) && col >= 0 && col < seats.GetLength(1))
            {
                seats[row, col] = 'R';
            }
        }

        int rows = seats.GetLength(0);
        int columns = seats.GetLength(1);

        Console.Write("   ");
        for (int j = 1; j <= columns; j++)
        {
            Console.Write($"{j,2}  ");
        }
        Console.WriteLine();

        for (int i = 0; i < rows; i++)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{rows - i,2}  ");

            for (int j = 0; j < columns; j++)
            {
                if (seats[i, j] == ' ')
                {
                    Console.Write("    ");
                }
                else
                {
                    if (seats[i, j] == 'R')
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("■   ");
                        continue;
                    }
                    else
                    {
                        switch (pricingCategories[i, j])
                        {
                            case 1:
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                        }
                    }

                    if (seats[i, j] == 'A')
                    {
                        Console.Write("■   ");
                    }
                    else if (seats[i, j] == 'C')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("■   ");
                    }
                    else if (seats[i, j] == 'R')
                    {
                        Console.Write("■   ");
                    }
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();
        Console.WriteLine("\nChair Prices:");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("■ Premium: [€15.00] ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("■ Standard: [€12.50] ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("■ Basic: [€10.00] ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("■ Reserved chairs");
        Console.ResetColor();
        Console.WriteLine();

        Console.ResetColor();
    }

    public void SelectSeats(long showId, UserModel acc)
    {
        List<long> reserved_seats = ReservationAccess.GetReservedSeatsByShowId(showId);
        // UpdateSeatsArray(reserved_seats);
        DisplaySeats(showId);

        Console.WriteLine("How many seats do you want to book?");
        int how_many_people = Convert.ToInt32(Console.ReadLine());

        List<SeatsModel> selected_seats = new List<SeatsModel>();
        
        List<int> countConnectedEmptySeats = new() { };
        int selectedSeatGroupIndex = 0;

        for (int i = 0; i < how_many_people; i++)
        {
            do
            {
                Console.WriteLine($"Booking seat {i + 1}");
                Console.WriteLine("Enter the row and column of the seat (e.g., 5 6):");
                string input = Console.ReadLine();

                string[] parts = input.Split(' ');

                if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
                {
                    Console.WriteLine("Invalid input format.");
                    continue;
                }

                row = seats.GetLength(0) - row;
                col -= 1;

                if (row < 0 || row >= seats.GetLength(0) || col < 0 || col >= seats.GetLength(1))
                {
                    Console.WriteLine("Invalid seat selection.");
                    continue;
                }

                if (seats[row, col] == 'A')
                {
                    if (i == 0 && how_many_people > 1) // For the first person out of the group
                    {
                        // Check if there are enough consecutive empty seats to fit the group

                        for (int j = 0; j < seats.GetLength(1) / 2 + 1; j++) // set the max amount of indexes that are possible
                        {
                            countConnectedEmptySeats.Add(0);
                        }

                        int currentEmptySeatGroupIndex = 0;
                        for (int j = 0; j < seats.GetLength(1); j++)
                        {
                            if (seats[row, j] == 'A') // Count consecutive empty seats ('A')
                            {
                                countConnectedEmptySeats[currentEmptySeatGroupIndex]++;
                            }
                            else if (seats[row, j] == 'R' && countConnectedEmptySeats[currentEmptySeatGroupIndex] != 0)
                            {
                                // Move to the next group of empty seats when encountering a reserved seat ('R')
                                currentEmptySeatGroupIndex++;
                            }

                            if (j == col) // Identify the group index of the selected seat
                            {
                                selectedSeatGroupIndex = currentEmptySeatGroupIndex;
                            }
                        }

                        // Console.WriteLine($"{countConnectedEmptySeats[selectedSeatGroupIndex]} == {how_many_people} + 1 = {countConnectedEmptySeats[selectedSeatGroupIndex] == how_many_people + 1}");
                        // Console.WriteLine($"{countConnectedEmptySeats[selectedSeatGroupIndex]} == {how_many_people} + 3 = {countConnectedEmptySeats[selectedSeatGroupIndex] == how_many_people + 3}");

                        if (countConnectedEmptySeats[selectedSeatGroupIndex] == how_many_people + 1)
                        {
                            Console.WriteLine("Please choose another row. Make sure no empty seat is left unfilled.");
                            continue;
                        }
                        else if (countConnectedEmptySeats[selectedSeatGroupIndex] < how_many_people)
                        {
                            Console.WriteLine("There are not enough seats here to seat everyone.");
                            continue;
                        }
    	                /*
                        Only validate individual seat selection if 2 or more seats stay empty
                        Else the group can just seat all empty seats and this logic doesn't apply
                        */
                        if (countConnectedEmptySeats[selectedSeatGroupIndex] != how_many_people)
                        {
                            int peopleLeftToSeat = how_many_people - i;
                            if (!IsValidSingleSeat(row, col, peopleLeftToSeat, how_many_people))
                            {
                                Console.WriteLine("Sorry, you can't take this seat.");
                                Console.WriteLine("Make sure there is no empty seat between you and someone else.");
                                continue;
                            }
                        }
                    }
                    else if (i > 0 && how_many_people > 1) // For the other people out of the group
                    {
                        // Validate group seat selection
                        if (!IsValidGroupSeat(row, col))
                        {
                            Console.WriteLine("Sorry, you can't take this seat.");
                            Console.WriteLine("Make sure all selected seats are next to each other.");
                            continue;
                        }
    	                /*
                        Only validate individual seat selection if 2 or more seats stay empty
                        Else the group can just seat all empty seats and this logic doesn't apply
                        */
                        if (countConnectedEmptySeats[selectedSeatGroupIndex] != how_many_people)
                        {
                            // Validate individual seat selection
                            int peopleLeftToSeat = how_many_people - i;
                            if (!IsValidSingleSeat(row, col, peopleLeftToSeat, how_many_people))
                            {
                                Console.WriteLine("Sorry, you can't take this seat.");
                                Console.WriteLine("Make sure there is no empty seat between you and someone else.");
                                continue;
                            }
                        }
                    }
                    else // For individuals
                    {
                        if (!IsValidSingleSeat(row, col, 1, 1))
                        {
                            Console.WriteLine("Sorry, you can't take this seat.");
                            Console.WriteLine("Make sure there is no empty seat between you and someone else.");
                            continue;
                        }
                    }

                    seats[row, col] = 'C';
                    // Console.WriteLine($"You have selected seat ({seats.GetLength(0) - row}, {col + 1}).");

                    int chairType = pricingCategories[row, col];
                    double price;
                    //THIS HAS TO BE CHANGED TO DOUBLES IN THE CODE AND NUMERIC IN DATABASE
                    //SO THAT WE CAN DO 12.5O RIGHT NOW THIS WORKS WITH ONLY WHOLE NUMMERS BUT
                    //SHOULD EASILY WORK WITH DATABASE CHANGES
                    //CHECK IF IT WORKS WITH ANOUSKA ADMIN MONEY SYSTEM.

                    if (chairType == 1)
                    {
                        price = 10.00;
                    }
                    else if (chairType == 2)
                    {
                        price = 12.50;
                    }
                    else if (chairType == 3)
                    {
                        price = 15.00;
                    }
                    else
                    {
                        Console.WriteLine("Invalid chair type. Please select a valid seat.");
                        continue;
                    }

                    var selectedSeat = new SeatsModel
                    {
                        RowNumber = seats.GetLength(0) - row,
                        ColumnNumber = col + 1,
                        Price = price
                    };

                    long seatId = SeatsAccess.InsertSeatAndGetId(selectedSeat);

                    selected_seats.Add(new SeatsModel
                    {
                        Id = seatId,
                        RowNumber = selectedSeat.RowNumber,
                        ColumnNumber = selectedSeat.ColumnNumber,
                        Price = selectedSeat.Price
                    });

                    DisplaySeats(showId);
                    break;
                }
                else if (seats[row, col] == 'C')
                {
                    Console.WriteLine("Sorry, that seat is already taken.");
                }
                else
                {
                    Console.WriteLine("Sorry, that seat is not available.");
                }
            } while(true);
        }

        if (acc != null)
        {
            Console.WriteLine("Making reservation...");
            MakeReservation(selected_seats, acc, showId);
        }
        else
        {
            Console.WriteLine("No user logged in, cannot make a reservation.");
        }
    }


    private void MakeReservation(List<SeatsModel> selectedSeats, UserModel acc, long showId)
    {
        Int64 userId = acc.Id;
        Console.WriteLine("Do you want bar service? (yes/no):");
        bool barService = Console.ReadLine().ToLower() == "yes" && IsBarAvailable(selectedSeats.Count, showId);

        foreach (var seat in selectedSeats)
        {
            //SeatsLogic.WriteSeat(seat);
            var reservation = new ReservationModel
            {
                Id = 0,
                Bar = barService,
                SeatsId = (int)seat.Id,
                // SeatsId = (seat.RowNumber - 1) * seats.GetLength(1) + (seat.ColumnNumber - 1),
                UserId = Convert.ToInt32(userId),
                ShowId = (int)showId,
            };

            ReservationLogic.WriteReservation(reservation);
        }
        Console.WriteLine($"Successfully reserved seats for {acc.FirstName} {acc.LastName}.");
        User.Start(acc);
    }

    private bool IsValidSingleSeat(int row, int col, int peopleLeftToSeat, int totalAmountOfPeople)
    {
        bool leftIsValid = false;
        bool rightIsValid = false;

        int countSeatPlusLeftSpace = 0;
        int countEmptyLeftSpace = 0;

        // Count the total number of seats and spaces on the left side in the row
        for (int j = 0; j < seats.GetLength(1); j++)
        {
            if (j < seats.GetLength(1) / 2)
            {
                // Console.WriteLine($"Left column: {seats[row, j]}");
                countSeatPlusLeftSpace++;
            }
            else if (seats[row, j] != ' ')
            {
                // Console.WriteLine($"Seat: {seats[row, j]}");
                countSeatPlusLeftSpace++;
            }
            else
            {
                // Console.WriteLine($"No seat: {seats[row, j]}");
                countEmptyLeftSpace++;
            }
        }

        // Console.WriteLine($"Number of seats: {countSeatPlusLeftSpace}");
        // Console.WriteLine();

        // Validate left side of the row
        // Console.WriteLine("Checking left side of the row...");
        // Console.WriteLine($"{col} + 1 == 1 = {col + 1 == 1}");
        // Console.WriteLine($"{col} + 1 > 1 = {col + 1 > 1}");

        if (col + 1 == 1) // No seats to the left
        {
            leftIsValid = true;
        }
        else if (col + 1 > 1) // Seats exist to the left
        {
            if (seats[row, col - 1] == 'C') // Group members sitting next to each other
            {
                leftIsValid = true;
            }

            // Check seat conditions on the left
            bool twoEmptySeat = false;
            bool nextToTwo = false;
            bool nextToOne = seats[row, col - 1] == 'R';
            bool seatGroup = false;

            // Console.WriteLine($"{col} > 2 = {col > 2}");
            if (col + 1 - 2 > countEmptyLeftSpace - 1) // At least two seats away from the far-left edge
            {
                twoEmptySeat = seats[row, col - 2] == 'A' && seats[row, col - 1] == 'A';
                nextToTwo = seats[row, col - 2] == 'R' && seats[row, col - 1] == 'R';
                nextToOne = nextToOne && seats[row, col - 2] == 'A';
                seatGroup = peopleLeftToSeat < 1;

                // Console.WriteLine($"Expected = 'A', 'A' or 'A', 'R' or 'R', 'R'");
                // Console.WriteLine($"Actual = '{seats[row, col - 2]}', '{seats[row, col - 1]}'");

                if (seats[row, col - 2] == 'R' && seats[row, col - 1] == 'A') // Adjacent to another group
                {
                    seatGroup = peopleLeftToSeat >= 2 && peopleLeftToSeat < totalAmountOfPeople;
                    // Console.WriteLine("seats[row, col - 2] == 'R' && seats[row, col - 1] == 'A'");
                    // Console.WriteLine($"More than 1 person left to seat: {seatGroup}");
                }
            }
            else if (col + 1 - 2 == countEmptyLeftSpace - 1) // At the edge with exactly two seats on the left
            {
                seatGroup = peopleLeftToSeat >= 2;
                // Console.WriteLine($"More than 1 person left to seat: {seatGroup}");
            }
            Console.WriteLine($"space left: {col} + {1} - {2} == {countEmptyLeftSpace - 1}");

            // Determine validity for the left side
            bool groupMember = seats[row, col - 1] == 'C';
            leftIsValid = twoEmptySeat || nextToTwo || nextToOne || groupMember || seatGroup;
            /*
            Console.WriteLine($"Left is valid if one of these conditions is true:");
            Console.WriteLine($"Two empty seats: {twoEmptySeat}");
            Console.WriteLine($"Two reserved seats: {nextToTwo}");
            Console.WriteLine($"Next to one reserved seat: {nextToOne}");
            Console.WriteLine($"Next to groupmate: {groupMember}");
            */
        }

        // Validate right side of the row
        // Console.WriteLine("Checking right side of the row...");
        // Console.WriteLine($"{col} + 1 == {countSeatPlusLeftSpace} = {col + 1 == countSeatPlusLeftSpace}");
        // Console.WriteLine($"{col} + 1 < {countSeatPlusLeftSpace} = {col + 1 < countSeatPlusLeftSpace}");

        if (col + 1 == countSeatPlusLeftSpace) // No seats to the right
        {
            rightIsValid = true;
        }
        else if (col + 1 < countSeatPlusLeftSpace) // Seats exist to the right
        {
            // Check seat conditions on the right
            bool twoEmptySeat = false;
            bool nextToTwo = false;
            bool nextToOne = seats[row, col + 1] == 'R';
            bool seatGroup = false;

            // Console.WriteLine($"{countSeatPlusLeftSpace} - {col} > 2 = {countSeatPlusLeftSpace - col > 2}");
            if (countSeatPlusLeftSpace - col > 2) // At least two seats away from the far-right edge
            {
                twoEmptySeat = seats[row, col + 1] == 'A' && seats[row, col + 2] == 'A';
                nextToTwo = seats[row, col + 1] == 'R' && seats[row, col + 2] == 'R';
                nextToOne = nextToOne && seats[row, col + 2] == 'A';

                // Console.WriteLine($"Expected = 'A', 'A' or 'A', 'R' or 'R', 'R'");
                // Console.WriteLine($"Actual = '{seats[row, col + 2]}', '{seats[row, col + 1]}'");

                if (seats[row, col + 2] == 'R' && seats[row, col + 1] == 'A') // Adjacent to another group
                {
                    seatGroup = peopleLeftToSeat >= 2 && peopleLeftToSeat < totalAmountOfPeople;
                    // Console.WriteLine("seats[row, col + 2] == 'R' && seats[row, col + 1] == 'A'");
                    // Console.WriteLine($"More than 1 person left to seat: {seatGroup}");
                }
            }
            else if (countSeatPlusLeftSpace - col + 1 == 2) // At the edge with exactly two seats on the right
            {
                seatGroup = peopleLeftToSeat >= 2;
                // Console.WriteLine($"space left: {countSeatPlusLeftSpace} - column: {col + 1} == 2");
                // Console.WriteLine($"More than 1 person left to seat: {seatGroup}");
            }

            // Determine validity for the right side
            bool groupMember = seats[row, col + 1] == 'C';
            rightIsValid = twoEmptySeat || nextToTwo || nextToOne || groupMember || seatGroup;

            /*
            Console.WriteLine($"Right is valid if one of these conditions is true:");
            Console.WriteLine($"Two empty seats: {twoEmptySeat}");
            Console.WriteLine($"Two reserved seats: {nextToTwo}");
            Console.WriteLine($"Next to one reserved seat: {nextToOne}");
            Console.WriteLine($"Next to groupmate: {groupMember}");
            */
        }

        // Output overall validation result
        // Console.WriteLine($"Left valid: {leftIsValid}");
        // Console.WriteLine($"Right valid: {rightIsValid}");
        return leftIsValid && rightIsValid;
    }

    private bool IsValidGroupSeat(int row, int col)
    {
        /*
        Group seats must be next to each other
        */
        if (seats[row, col - 1] == 'C' || seats[row, col + 1] == 'C')
        {
            return true;
        }

        return false;
    }

    static private bool IsBarAvailable(int sizeOfGroup, long showId)
    {
        int countBarReservations = 0;

        ShowModel userShow = ShowLogic.GetByID((int)showId);
        //MoviesModel userMovie = MoviesLogic.GetById((int)userShow.MovieId);
        MoviesModel userMovie = MoviesLogic.GetById((int)userShow.MovieId);

        DateTime userMovieBeginTime = DateTime.Parse(userShow.Date);
        DateTime userBarReservationTimeStart = userMovieBeginTime.AddMinutes(userMovie.TimeInMinutes);

        List<ReservationModel> reservations = ReservationLogic.GetBarReservations();

        foreach (ReservationModel reservation in reservations)
        {
            ShowModel show = ShowLogic.GetByID(reservation.ShowId);
            MoviesModel movie = MoviesLogic.GetById((int)show.MovieId);

            DateTime movieBeginTime = DateTime.Parse(show.Date);
            DateTime barReservationTimeStart = movieBeginTime.AddMinutes(movie.TimeInMinutes);

            if (userBarReservationTimeStart == barReservationTimeStart)
            {
                countBarReservations++;
            }
        }

        if (countBarReservations + sizeOfGroup <= 40)
        {
            Console.WriteLine($"Your bar reservation has been accepted. We still have seats available!");
            return true;
        }
        else
        {
            Console.WriteLine("We’re unable to take any more bar reservations. It is fully booked");
            return false;
        }
    }
}

public class ConcreteTheater : TheaterBase
{
    public ConcreteTheater(int[,] pricingCategories)
        : base(pricingCategories.GetLength(0), pricingCategories.GetLength(1), pricingCategories) { }
}


public class Theater
{
    public static TheaterBase GetTheater(int theaterType)
    {
        int[,] pricingCategories = null;

        switch (theaterType)
        {
            case 150:
                pricingCategories = new int[14, 12]
                {
                    { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 },
                    { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
                    { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
                    { 3, 3, 3, 3, 3, 2, 2, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 2, 2, 2, 2, 3, 3, 3, 3 },
                    { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
                    { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
                    { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
                    { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
                    { 3, 3, 3, 3, 2, 2, 2, 2, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 3, 2, 2, 3, 3, 3, 3, 3 },
                    { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
                    { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 },
                    { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 }
                };
                return new ConcreteTheater(pricingCategories);

            case 300:
                pricingCategories = new int[19, 18]
                {
                    { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
                    { 0, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 0 },
                    { 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0 },
                    { 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0 },
                    { 0, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 0 },
                    { 0, 3, 3, 3, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 3, 3, 3, 0 },
                    { 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 },
                    { 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3 },
                    { 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
                    { 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
                    { 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
                    { 0, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 0 },
                    { 0, 3, 3, 3, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 3, 3, 3, 0 },
                    { 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0 },
                    { 0, 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0, 0 },
                    { 0, 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0, 0 },
                    { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 },
                    { 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0 },
                    { 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0 }
                };
                return new ConcreteTheater(pricingCategories);

            case 500:
                pricingCategories = new int[20, 30]
                {
                    { 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0 },
                    { 0, 0, 0, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 0, 0, 0 },
                    { 0, 0, 0, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 0, 0, 0 },
                    { 0, 0, 0, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 0, 0, 0 },
                    { 0, 0, 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0, 0, 0 },
                    { 0, 0, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 0, 0 },
                    { 0, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 0 },
                    { 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3 },
                    { 0, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 0 },
                    { 0, 0, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 0, 0 },
                    { 0, 0, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 0, 0 },
                    { 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0 },
                    { 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0 }
                };
                return new ConcreteTheater(pricingCategories);

            default:
                return null;
        }
    }
}