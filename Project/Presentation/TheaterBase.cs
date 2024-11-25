using System;
using System.Runtime.CompilerServices;

public abstract class TheaterBase
{
    protected char[,] seats;
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

    protected void UpdateSeatsArray(List<long> reservedSeats)
    {
        for (int i = 0; i < seats.GetLength(0); i++)
        {
            for (int j = 0; j < seats.GetLength(1); j++)
            {
                int seatId = (i * seats.GetLength(1)) + j;
                
                if (reservedSeats.Contains(seatId))
                {
                    seats[i, j] = 'C';
                }
                
            }
        }
        
    }

    public void DisplaySeats(long showId)
    {
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
    }

    public void SelectSeats(long showId, UserModel acc)
    {
        List<long> reserved_seats = ReservationAccess.GetReservedSeatsByShowId(showId);
        UpdateSeatsArray(reserved_seats);
        DisplaySeats(showId);

        Console.WriteLine("How many seats do you want to book?");
        int how_many_people = Convert.ToInt32(Console.ReadLine());

        List<SeatsModel> selected_seats = new List<SeatsModel>();

        for (int i = 0; i < how_many_people; i++)
        {
            do
            {
                
                Console.WriteLine("Enter the row and column of the seat (e.g., 5 6):");
                string input = Console.ReadLine();

                string[] parts = input.Split(' ');

                if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
                {
                    Console.WriteLine("Invalid input format.");
                    continue;
                }

                row = seats.GetLength(0) - row; col -= 1;

                if (row < 0 || row >= seats.GetLength(0) || col < 0 || col >= seats.GetLength(1))
                {
                    Console.WriteLine("Invalid seat selection.");
                    continue;
                }

                if (seats[row, col] == 'A')
                {
                    if (how_many_people == 1 && !IsValidSingleSeat(row, col))
                    {
                        Console.WriteLine("Sorry, you can't take this seat.");
                        Console.WriteLine("Make sure their is no empty seat between you and someone else");
                        continue;
                    }
                    else
                    {
                        List<(int row, int col)> currentSelection = selected_seats
                            .Select(s => (seats.GetLength(0) - s.RowNumber, s.ColumnNumber - 1))
                            .ToList();
                        currentSelection.Add((row, col));

                        if (!IsValidGroupSelection(currentSelection))
                        {
                            Console.WriteLine("Sorry, you can't take this seat.");
                            Console.WriteLine("Make sure all selected seats are next to each other.");
                            continue;
                        }
                    }
                    
                    seats[row, col] = 'C';
                    Console.WriteLine($"You have selected seat ({seats.GetLength(0) - row}, {col + 1}).");

                    int chairType = pricingCategories[row, col];
                    int price;
                    //THIS HAS TO BE CHANGED TO DOUBLES IN THE CODE AND NUMERIC IN DATABASE
                    //SO THAT WE CAN DO 12.5O RIGHT NOW THIS WORKS WITH ONLY WHOLE NUMMERS BUT
                    //SHOULD EASILY WORK WITH DATABASE CHANGES
                    //CHECK IF IT WORKS WITH ANOUSKA ADMIN MONEY SYSTEM.

                    if (chairType == 1)
                    {
                        price = 10;
                    }
                    else if (chairType == 2)
                    {
                        price = 12;
                    }
                    else if (chairType == 3)
                    {
                        price = 15;
                    }
                    else
                    {
                        Console.WriteLine("Invalid chair type. Please select a valid seat.");
                        return;
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
    }

    private bool IsValidSingleSeat(int row, int col)
    {
        bool leftValid = false;
        bool rightValid = false;

        int countLeftSpace = 0;

        for (int j = 0; j < seats.GetLength(1); j++)
        {
            if (j < seats.GetLength(1) / 2)
            {
                System.Console.WriteLine("Left column:");
                System.Console.WriteLine(seats[row, j]);
                countLeftSpace++;
            }
            else if (seats[row, j] != ' ')
            {
                System.Console.WriteLine("Seat:");
                System.Console.WriteLine(seats[row, j]);
                countLeftSpace++;
            }
            else
            {
                System.Console.WriteLine("No seat:");
                System.Console.WriteLine(seats[row, j]);
            }
        }

        Console.WriteLine($"number of seats: {countLeftSpace}");

        Console.WriteLine();
        Console.WriteLine("if - left");
        Console.WriteLine($"{col} + {1} >= {3} = {col + 1 >= 3}");
        Console.WriteLine("else if - left");
        Console.WriteLine($"{col} + {1} == {1} = {col + 1 == 1}");
        Console.WriteLine();

        if (col + 1 >= 3)
        {
            bool leftValidCond1 = seats[row, col - 2] == 'A' && seats[row, col - 1] == 'A';
            bool leftValidCond2 = seats[row, col - 2] == 'C' && seats[row, col - 1] == 'C';

            leftValid = leftValidCond1 || leftValidCond2;

            Console.WriteLine("Left valid:");
            Console.WriteLine($"Expected = 'A', 'A' or 'C', 'C'\nActual === '{seats[row, col - 2]}', '{seats[row, col - 1]}'\n{leftValid}");
            Console.WriteLine();
        }
        else if (col + 1 == 1)
        {
            leftValid = true;
        }
        else if (seats[row, col - 1] == 'C')
        {
            leftValid = true;
        }

        Console.WriteLine();
        Console.WriteLine("if - right");
        Console.WriteLine($"{col} + {1} < {countLeftSpace} = {col + 1 < countLeftSpace}");
        Console.WriteLine("else if - right");
        Console.WriteLine($"{col} + {1} == {countLeftSpace} = {col + 1 == countLeftSpace}");
        Console.WriteLine();

        if (col + 1 < countLeftSpace)
        {
            bool rightValidCond1 = seats[row, col + 2] == 'A' && seats[row, col + 1] == 'A';
            bool rightValidCond2 = seats[row, col + 2] == 'C' && seats[row, col + 1] == 'C';

            rightValid = rightValidCond1 || rightValidCond2;

            Console.WriteLine("Right valid:");
            Console.WriteLine($"Expected = 'A', 'A' or 'C', 'C'\nActual === '{seats[row, col + 2]}', '{seats[row, col + 1]}'\n{rightValid}");
            Console.WriteLine();
        }
        else if (col + 1 == countLeftSpace)
        {
            rightValid = true;
        }
        else if (seats[row, col + 1] == 'C')
        {
            leftValid = true;
        }

        System.Console.WriteLine($"leftValid: {leftValid}");
        System.Console.WriteLine($"rightValid: {rightValid}");
        System.Console.WriteLine();

        return leftValid && rightValid;
    }

    private bool IsValidGroupSelection(List<(int row, int col)> selectedSeats)
    {
        foreach (var seat in selectedSeats)
        {
            int row = seat.row;
            int col = seat.col;

            if (!selectedSeats.All(s => s.row == row && Math.Abs(s.col - col) <= selectedSeats.Count - 1))
            {
                return false;
            }
        }
        return true;
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