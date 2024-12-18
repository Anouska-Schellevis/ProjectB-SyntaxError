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
        DisplaySeats(showId);

        Console.WriteLine("How many seats do you want to book?");
        int how_many_people = Convert.ToInt32(Console.ReadLine());

        List<SeatsModel> selected_seats = new List<SeatsModel>();

        /*
        This loop looks for consecutive empty seats in per row
        The count wil be inserted in the empty seats
        */
        int[,] countAvailableSeats = new int[seats.GetLength(0), seats.GetLength(1)];
        for (int i = 0; i < seats.GetLength(0); i++)
        {
            int rowCountAvailableSeats = 0;

            for (int j = 0; j < seats.GetLength(1); j++)
            {
                if (seats[i, j] == 'A')
                {
                    rowCountAvailableSeats++;
                    countAvailableSeats[i, j] = rowCountAvailableSeats;
                }
                else
                {
                    rowCountAvailableSeats = 0;
                    countAvailableSeats[i, j] = 0;
                }
            }

            for (int jBack = seats.GetLength(1) - 1; jBack > 0; jBack--)
            {
                if (seats[i, jBack] == 'A')
                {
                    if (seats[i, jBack - 1] == 'A')
                    {
                        countAvailableSeats[i, jBack - 1] = countAvailableSeats[i, jBack];
                    }
                }
                else
                {
                    countAvailableSeats[i, jBack] = 0;
                }
            }
        }

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
                    /*
                    If the row contains enough consecutive empty seats, 
                    but there will always be an empty seat somewhere in between
                    */
                    if (countAvailableSeats[row, col] == how_many_people + 1)
                    {
                        Console.WriteLine("Please choose another row. Make sure no empty seat is left unoccupied.");
                        continue;
                    }
                    else if (countAvailableSeats[row, col] < how_many_people) // Not enough consecutive empty seats
                    {
                        Console.WriteLine("There aren't enough seats here for everyone.");
                        continue;
                    }
                    else if (how_many_people > 1) // For groups
                    {
                        /*
                        Use countAvailableSeats to determine the amount of available seats next to each other
                        If the first person wants to sit in the middle, 
                        but if you seat everyone, 
                        there will always be an empty seat left on one of the edges.
                        */
                        if (i == 0 && countAvailableSeats[row, col] == how_many_people + 3)
                        {
                            int countAvailableSeatsLeft = 0, countAvailableSeatsRight = 0;
                            int rowWidth = seats.GetLength(1);

                            for (int j = 0; j < rowWidth; j++)
                            {
                                if (seats[row, j] == 'A' && j < col) countAvailableSeatsLeft++;
                                if (seats[row, rowWidth - 1 - j] == 'A' && rowWidth - 1 - j > col) countAvailableSeatsRight++;
                            }

                            if (countAvailableSeatsLeft > 0 && countAvailableSeatsRight > 0) // So the first person should sit on the left or right edge
                            {
                                Console.WriteLine("Sorry, you can't take this seat");
                                Console.WriteLine("Your seat may only be located on the left or right edge of the row");
                                continue;
                            }
                        }
                        else if (i > 0 && !IsValidGroupSeat(row, col)) // Does not apply to the first person to sit down
                        {
                            Console.WriteLine("Sorry, you can't take this seat.");
                            Console.WriteLine("Make sure all selected seats are next to each other.");
                            continue;
                        }
                        /*
                        Only validate individual seat selection if seats stay empty
                        Else the group can just seat all the available seats and this logic doesn't apply
                        */
                        if (countAvailableSeats[row, col] != how_many_people)
                        {
                            int peopleLeftToSeat = how_many_people - i;
                            if (!IsValidSingleSeat(row, col, peopleLeftToSeat, how_many_people))
                            {
                                Console.WriteLine("Sorry, you can't take this seat.");
                                Console.WriteLine("Make sure there is no empty seat between you and anyone else.");
                                continue;
                            }
                        }
                    }
                    else // For individuals
                    {
                        if (!IsValidSingleSeat(row, col, 1, 1))
                        {
                            Console.WriteLine("Sorry, you can't take this seat.");
                            Console.WriteLine("Make sure there is no empty seat between you and anyone else.");
                            continue;
                        }
                    }

                    seats[row, col] = 'C';
                    // Console.WriteLine($"You have selected seat ({seats.GetLength(0) - row}, {col + 1}).");

                    int chairType = pricingCategories[row, col];
                    decimal price;
                    //THIS HAS TO BE CHANGED TO DOUBLES IN THE CODE AND NUMERIC IN DATABASE
                    //SO THAT WE CAN DO 12.5O RIGHT NOW THIS WORKS WITH ONLY WHOLE NUMMERS BUT
                    //SHOULD EASILY WORK WITH DATABASE CHANGES
                    //CHECK IF IT WORKS WITH ANOUSKA ADMIN MONEY SYSTEM.

                    if (chairType == 1)
                    {
                        price = 10.00m;
                    }
                    else if (chairType == 2)
                    {
                        price = 12.50m;
                    }
                    else if (chairType == 3)
                    {
                        price = 15.00m;
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
            } while (true);
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


    // private void MakeReservation(List<SeatsModel> selectedSeats, UserModel acc, long showId)
    // {   
    //     List<VoucherModel> userVouchers = VoucherLogic.GetVouchersByUserId(acc);
    //     if (userVouchers.Count > 0) // Check if the user has a voucher, otherwise this option doesn't show up
    //     {
    //         Console.WriteLine("You have active vouchers");
    //         Console.WriteLine("Would you like to use a voucher? (yes/no)");
    //         bool useVoucher = Console.ReadLine().ToLower() == "yes";
    //         if (useVoucher)
    //         {
    //             do
    //             {
    //                 Voucher.PrintAllUserVouchers(acc);
    //                 Console.WriteLine("Enter the ID of the voucher you wish to use");

    //                 bool isValidFormat = int.TryParse(Console.ReadLine(), out int inputId);
    //                 if(!isValidFormat)
    //                 {
    //                     Console.WriteLine("Invalid format. Make sure to enter a number.");
    //                     continue;
    //                 }

    //                 VoucherModel voucher = userVouchers.FirstOrDefault(v => v.Id == inputId); // Looks up if the input ID is from an existing voucher obj
    //                 if (voucher is null)
    //                 {
    //                     Console.WriteLine("This ID doesn't exist. Try again");
    //                     continue;
    //                 }

    //                 decimal oldAmount = voucher.Amount;

    //                 for(int i = 0; i < selectedSeats.Count; i++)
    //                 {
    //                     voucher.Amount = oldAmount; // To ensure that all seats are discounted before the cost is finally deducted from the voucher.
    //                     selectedSeats[i].Price = VoucherLogic.CalculateDiscountedPrice(ref voucher, selectedSeats[i].Price); // ref is used to make a reference to voucher outside this function.
    //                 }

    //                 VoucherLogic.UpdateVoucher(voucher); // Changes to the voucher are written to the database

    //                 Console.WriteLine("Your voucher is succesfully applied!");
    //                 break;
    //             } while(true);
    //         }
    //     }

    //     long userId = acc.Id;
    //     Console.WriteLine("Do you want bar service? (yes/no):");
    //     bool barService = Console.ReadLine().ToLower() == "yes" && IsBarAvailable(selectedSeats.Count, showId);

    //     Console.WriteLine("Would you like to order snacks?");
    //     Console.WriteLine("[1] Yes");
    //     Console.WriteLine("[2] No");

    //     string snacks = "";
    //     if (Console.ReadLine() == "1")
    //     {
    //         List<MenuItem> selectedSnacks = SnackMenu.SelectSnacks();
    //         List<string> snackNames = new List<string>();
    //         foreach (MenuItem snack in selectedSnacks)
    //         {
    //             snackNames.Add(snack.Name);
    //         }
    //         snacks = string.Join(",", snackNames);
    //     }

    //     foreach (var seat in selectedSeats)
    //     {
    //         var reservation = new ReservationModel
    //         {
    //             Id = 0,
    //             Bar = barService,
    //             SeatsId = (int)seat.Id,
    //             UserId = Convert.ToInt32(userId),
    //             ShowId = (int)showId,
    //             Snacks = snacks
    //         };

    //         ReservationLogic.WriteReservation(reservation);
    //     }

    //     Console.WriteLine($"Successfully reserved seats and snacks for {acc.FirstName} {acc.LastName}.");
    //     User.Start(acc);
    // }
    private void MakeReservation(List<SeatsModel> selectedSeats, UserModel acc, long showId)
    {
        List<VoucherModel> userVouchers = VoucherLogic.GetVouchersByUserId(acc);
        if (userVouchers.Count > 0) // Check if the user has a voucher, otherwise this option doesn't show up
        {
            Console.WriteLine("You have active vouchers");
            Console.WriteLine("Would you like to use a voucher?");
            Console.WriteLine("[1] yes");
            Console.WriteLine("[2] no");
            bool useVoucher = Console.ReadLine() == "1";
            if (useVoucher)
            {
                do
                {
                    Voucher.PrintAllUserVouchers(acc);
                    Console.WriteLine("Enter the voucher you wish to use");

                    bool isValidFormat = int.TryParse(Console.ReadLine(), out int inputNum);
                    if (!isValidFormat)
                    {
                        Console.WriteLine("Invalid format. Make sure to enter a number.");
                        continue;
                    }

                    VoucherModel voucher = userVouchers.ElementAtOrDefault(inputNum-1); // the index is one smaller than the count
                    if (voucher is null)
                    {
                        Console.WriteLine("This ID doesn't exist. Try again");
                        continue;
                    }

                    string oldVoucherType = voucher.Type;

                    if (voucher.Type == "percentage" && selectedSeats.Count > 1) // CalculateDiscountedPrice() is not intended to subtract the percentage amount from more than one seat alternately. Then change it to a euro voucher.
                    {
                        decimal totalSeatsAmount = 0;

                        foreach (SeatsModel seat in selectedSeats)
                        {
                            totalSeatsAmount += seat.Price;
                        }

                        decimal voucherEuroAmount = totalSeatsAmount / 100 * voucher.Amount;

                        voucher.Type = "euro"; // euro voucher to correctly subtract the voucher from every seat until empty
                        voucher.Amount = voucherEuroAmount;
                    }

                    for (int i = 0; i < selectedSeats.Count; i++)
                    {
                        selectedSeats[i].Price = VoucherLogic.CalculateDiscountedPrice(ref voucher, selectedSeats[i].Price); // ref is used to make a reference to voucher outside this function.
                        SeatsLogic.UpdateSeat(selectedSeats[i]); // the new price of seats is written to the database
                    }

                    if (oldVoucherType == "percentage" && selectedSeats.Count > 1)
                    {
                        voucher.Type = "percentage";
                        voucher.Amount = 0;
                    }

                    VoucherLogic.UpdateVoucher(voucher); // Changes to the voucher are written to the database
                    
                    Console.WriteLine("Your voucher is succesfully applied!");
                    break;
                } while (true);
            }
        }

        long userId = acc.Id;
        Console.WriteLine("Do you want bar service? \n[1] Yes \n[2] No");
        bool barService = Console.ReadLine() == "1" && IsBarAvailable(selectedSeats.Count, showId);

        Console.WriteLine("Would you like to order snacks? \n[1] Yes \n[2] No");
        string snacks = "";

        if (Console.ReadLine() == "1")
        {
            Dictionary<MenuItem, int> selectedSnacks = SnackMenu.SelectSnacks();



            foreach (var snack in selectedSnacks)
            {
                if (snacks != string.Empty)//if the string isnt empty it knows to add a comma
                {
                    snacks += ", ";
                }
                snacks += snack.Key.Name;
            }


        }

        foreach (var seat in selectedSeats)
        {
            var reservation = new ReservationModel
            {
                Id = 0,
                Bar = barService,
                SeatsId = (int)seat.Id,
                UserId = Convert.ToInt32(userId),
                ShowId = (int)showId,
                Snacks = snacks
            };

            ReservationLogic.WriteReservation(reservation);
        }

        Console.WriteLine($"Successfully reserved seats and snacks for {acc.FirstName} {acc.LastName}.");
        User.Start(acc);
    }


    private bool IsValidSingleSeat(int row, int col, int peopleLeftToSeat, int totalAmountOfPeople)
    {
        int countSeatPlusLeftSpace = 0;
        int countEmptyLeftSpace = 0;

        // Count the total number of seats and empty array chars on the left side in the row
        for (int j = 0; j < seats.GetLength(1); j++)
        {
            if (j < seats.GetLength(1) / 2) // Cut the array length in half, so that the right side is not included, as it is not needed.
            {
                countSeatPlusLeftSpace++;
            }
            else
            {
                if (seats[row, j] == ' ')
                    countEmptyLeftSpace++;
                else
                    countSeatPlusLeftSpace++;
            }
        }

        bool leftIsValid = false;
        bool rightIsValid = false;

        /*
        Left side logic
        */
        if (col + 1 == countEmptyLeftSpace + 1) // No seats to the left
        {
            leftIsValid = true;
        }
        else if (col + 1 > 1) // Seats exist to the left
        {
            bool twoEmptySeat = false;
            bool nextToTwo = false;
            bool nextToOne = seats[row, col - 1] == 'R';
            bool seatGroup = false;
            bool groupMember = seats[row, col - 1] == 'C'; // Group member sits to the left of the selected seat

            // Ensure there's enough space from the row's far-left edge to check seat positions within array bounds.
            if ((col + 1) - (countEmptyLeftSpace + 1) >= 2)
            {
                // The conditions for a valid seat
                twoEmptySeat = seats[row, col - 2] == 'A' && seats[row, col - 1] == 'A';
                nextToTwo = seats[row, col - 2] == 'R' && seats[row, col - 1] == 'R';
                nextToOne = nextToOne && seats[row, col - 2] == 'A';
                seatGroup = peopleLeftToSeat == 1;

                // Additional condition where the furthest seat is reserved and the closer one is available
                if (seats[row, col - 2] == 'R' && seats[row, col - 1] == 'A')
                {
                    // Determine if the group needs more than one seat
                    seatGroup = peopleLeftToSeat >= 2 && peopleLeftToSeat < totalAmountOfPeople;

                    // Ensure that remaining seats to the right are filled first
                    if (seatGroup && seats[row, col + 1] == 'A' && seats[row, col + 2] == 'A')
                    {
                        return false;
                    }
                }
            }

            leftIsValid = twoEmptySeat || nextToTwo || nextToOne || groupMember || seatGroup;
        }

        /*
        Right side logic
        */
        if (col + 1 == countSeatPlusLeftSpace) // No seats to the right
        {
            rightIsValid = true;
        }
        else if (col + 1 < countSeatPlusLeftSpace) // Seats exist to the right
        {
            bool twoEmptySeat = false;
            bool nextToTwo = false;
            bool nextToOne = seats[row, col + 1] == 'R';
            bool seatGroup = false;
            bool groupMember = seats[row, col + 1] == 'C'; // Group member sits to the right of the selected seat

            // Ensure there's enough space from the row's far-right edge to check seat positions within array bounds.
            if (countSeatPlusLeftSpace - (col + 1) >= 2)
            {
                // The conditions for a valid seat
                twoEmptySeat = seats[row, col + 1] == 'A' && seats[row, col + 2] == 'A';
                nextToTwo = seats[row, col + 1] == 'R' && seats[row, col + 2] == 'R';
                nextToOne = nextToOne && seats[row, col + 2] == 'A';
                seatGroup = peopleLeftToSeat == 1;

                // Additional condition where the furthest seat is reserved and the closer one is available
                if (seats[row, col + 2] == 'R' && seats[row, col + 1] == 'A')
                {
                    // Determine if the group needs more than one seat
                    seatGroup = peopleLeftToSeat >= 2 && peopleLeftToSeat < totalAmountOfPeople;

                    // Ensure that remaining seats to the left are filled first
                    if (seatGroup && seats[row, col - 1] == 'A' && seats[row, col - 2] == 'A')
                    {
                        return false;
                    }
                }
            }

            rightIsValid = twoEmptySeat || nextToTwo || nextToOne || groupMember || seatGroup;
        }

        return leftIsValid && rightIsValid;
    }

    private bool IsValidGroupSeat(int row, int col)
    {
        /*
        Group seats must be next to each other
        */
        if ((col > 0 && seats[row, col - 1] == 'C') || (col + 1 < seats.GetLength(0) && seats[row, col + 1] == 'C'))
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
