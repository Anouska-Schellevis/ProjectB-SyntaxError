using System;
using System.Runtime.CompilerServices;

public abstract class TheaterBase
{
    public char[,] seats; // The seat grid of the theater
    protected int[,] pricingCategories;

    // Constructor to initialize pricing categories and seats
    public TheaterBase(int rows, int columns, int[,] pricingCategories)
    {
        this.pricingCategories = pricingCategories;
        InitializeSeats(); // Initialize the seat grid based on the pricing categories
    }

    // Initializes the seat grid based on the pricing categories
    protected void InitializeSeats()
    {
        seats = new char[pricingCategories.GetLength(0), pricingCategories.GetLength(1)];
        for (int i = 0; i < seats.GetLength(0); i++)
        {
            for (int j = 0; j < seats.GetLength(1); j++)
            {
                // Mark unavailable seats with 'X' and available seats with 'A'
                seats[i, j] = pricingCategories[i, j] == 0 ? 'X' : 'A';
            }
        }
    }

    // Displays the seating arrangement in the console, marking reserved and available seats
    public void DisplaySeats(long showId, int selectedRow = 0, int selectedCol = 0)
    {
        Console.Clear();
        // Retrieve list of reserved seats for the given showId
        List<long> reservedSeats = ReservationLogic.GetReservedSeatsByShowId(showId);

        // Mark the reserved seats on the seat grid
        foreach (long seatId in reservedSeats)
        {
            SeatsModel seat = SeatsLogic.GetById((int)seatId);
            int row = seats.GetLength(0) - seat.RowNumber;
            int col = seat.ColumnNumber - 1;
            if (row >= 0 && row < seats.GetLength(0) && col >= 0 && col < seats.GetLength(1))
            {
                seats[row, col] = 'R'; // Mark reserved seats as 'R'
            }
        }

        int rows = seats.GetLength(0);
        int columns = seats.GetLength(1);

        // Display seat numbers on the top row
        Console.Write("   ");
        for (int j = 1; j <= columns; j++)
        {
            Console.Write($"{j,2}  ");
        }
        Console.WriteLine();

        // Display each row of seats
        for (int i = 0; i < rows; i++)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{rows - i,2}  "); // Display row number

            for (int j = 0; j < columns; j++)
            {
                // Check if the current seat is the selected seat
                bool isSelectedSeat = (i == selectedRow && j == selectedCol);

                if (seats[i, j] == 'X') // Check for unavailable seats
                {
                    Console.ForegroundColor = isSelectedSeat ? ConsoleColor.Green : ConsoleColor.Black;
                    Console.Write("■   "); // Display as a black block or green if selected when navigating through the seatoverview
                }
                else if (seats[i, j] == 'R') // Makes reserved seats purple
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("■   ");
                }
                else
                {
                    // Display available seats based on their pricing category
                    switch (pricingCategories[i, j])
                    {
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Red; // Premium seats are red
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Yellow; // Standard seats are yellow
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Blue; // Basic seats are blue
                            break;
                        case 0:
                            Console.ForegroundColor = isSelectedSeat ? ConsoleColor.Green : ConsoleColor.Black; // Green if selected
                            Console.Write("■   "); // Display as a black block or green if selected
                            continue; // Skip the rest of the loop for this seat
                        default:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                    }

                    if (isSelectedSeat) // Highlight selected seat
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    if (seats[i, j] == 'A') // Available seat
                    {
                        Console.Write("■   ");
                    }
                    else if (seats[i, j] == 'C') // Currently chosen seat(s)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("■   ");
                    }
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }

        // Display screen separator and the word "SCREEN"
        string screenLine = new string('=', columns * 4);
        Console.WriteLine(new string(' ', 3) + screenLine);
        Console.WriteLine(new string(' ', 3) + "SCREEN".PadLeft((screenLine.Length + "SCREEN".Length) / 2));
        Console.WriteLine(new string(' ', 3) + screenLine);

        // Display price legend for different seat types
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
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Use arrow keys to navigate and Enter to select one or more seat(s). Use backspace to delete your selected seat. Press Esc to finish selection.");
        Console.ResetColor();
        Console.WriteLine();
    }

    // Allows the user to select seats for a show
    public void SelectSeats(long showId, UserModel acc)
    {
        List<long> reservedSeats = ReservationLogic.GetReservedSeatsByShowId(showId);
        int selectedRow = 0;
        int selectedCol = 0;
        DisplaySeats(showId, selectedRow, selectedCol);

        List<SeatsModel> selectedSeats = new List<SeatsModel>();

        int seatscount = 1; // To track the number of selected/chosen seats
        int countSeatPlusLeftSpace = 0;
        int countEmptyLeftSpace = 0;

        // Count the total number of seats and empty array chars on the left side in the row
        for (int j = 0; j < seats.GetLength(1); j++)
        {
            if (j < seats.GetLength(1))
            {
                countSeatPlusLeftSpace++;
            }
            else
            {
                if (seats[selectedRow, j] == 'X')
                    countEmptyLeftSpace++;
                else
                    countSeatPlusLeftSpace++;
            }
        }

        // Begin the selection process
        while (true)
        {
            DisplaySeats(showId, selectedRow, selectedCol);

            ConsoleKeyInfo key = Console.ReadKey(true); // Read user input for navigation
            if (key.Key == ConsoleKey.UpArrow && selectedRow > 0)
            {
                selectedRow--; // Move up
            }
            else if (key.Key == ConsoleKey.DownArrow && selectedRow < seats.GetLength(0) - 1)
            {
                selectedRow++; // Move down
            }
            else if (key.Key == ConsoleKey.LeftArrow && selectedCol > 0)
            {
                selectedCol--; // Move left
            }
            else if (key.Key == ConsoleKey.RightArrow && selectedCol < seats.GetLength(1) - 1)
            {
                selectedCol++; // Move right
            }
            else if (key.Key == ConsoleKey.Enter) // Enter to choose the seat
            {
                // Check if the selected seat is available
                if (seats[selectedRow, selectedCol] == 'A')
                {
                    bool isSingleSeatValid = IsValidSingleSeat(selectedRow, selectedCol, 1, 1); // Assuming 1 person for single seat check
                    bool isGroupSeatValid = true;

                    if (seatscount >= 2)
                    {
                        isGroupSeatValid = IsValidGroupSeat(selectedRow, selectedCol); // Check if it's part of a valid group
                    }

                    if (isSingleSeatValid && isGroupSeatValid)
                    {
                        seats[selectedRow, selectedCol] = 'C'; // Mark selected seat

                        decimal price = GetSeatPrice(selectedRow, selectedCol); // Get price based on seat category
                        var selectedSeat = new SeatsModel
                        {
                            RowNumber = seats.GetLength(0) - selectedRow,
                            ColumnNumber = selectedCol + 1,
                            Price = price
                        };

                        long seatId = SeatsLogic.InsertSeatAndGetId(selectedSeat); // Insert the seat in the database and get the seat ID
                        selectedSeats.Add(new SeatsModel
                        {
                            Id = seatId,
                            RowNumber = selectedSeat.RowNumber,
                            ColumnNumber = selectedSeat.ColumnNumber,
                            Price = selectedSeat.Price
                        });
                        seatscount++; // Increase the count of selected seats
                    }
                    else
                    {
                        Console.WriteLine("This seat is not valid for selection.");
                    }
                }
                else
                {
                    Console.WriteLine("This seat is already taken or invalid.");
                }
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                // Logic to delete a selected seat
                if (seats[selectedRow, selectedCol] == 'C')
                {
                    int seatIdToDelete = (int)(selectedSeats.FirstOrDefault(s => s.RowNumber == (seats.GetLength(0) - selectedRow) && s.ColumnNumber == (selectedCol + 1))?.Id ?? 0);

                    bool canDelete = true;
                    if (countEmptyLeftSpace == selectedCol)
                    {
                        if (seats[selectedRow, selectedCol + 1] == 'C')
                        {
                            canDelete = false;
                        }
                    }
                    else if (countSeatPlusLeftSpace - 1 == selectedCol)
                    {
                        if (seats[selectedRow, selectedCol - 1] == 'C')
                        {
                            canDelete = false;
                        }
                    }
                    else
                    {
                        if (seats[selectedRow, selectedCol + 1] == 'C' && seats[selectedRow, selectedCol - 1] == 'C')
                        {
                            canDelete = false;
                        }
                    }

                    if (seatIdToDelete > 0 && canDelete)
                    {
                        SeatsLogic.DeleteSeat(seatIdToDelete); // Delete the seat from the database
                        seats[selectedRow, selectedCol] = 'A'; // Mark the seat as available
                        Console.WriteLine($"You have deleted the seat.");
                        seatscount--; // Decrease the count of selected seats
                    }
                    else
                    {
                        Console.WriteLine($"This seat can't be deleted.");
                    }
                }
                else
                {
                    Console.WriteLine($"This seat is not selected.");
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                // Logic to exit the seat selection process
                Console.WriteLine("Are you done choosing seats and do you want to leave the choosing area.\n[1] Yes\n[2] No");
                string esc_anwser = Console.ReadLine();
                if (esc_anwser == "1")
                {
                    break;
                }
                else if (esc_anwser == "2")
                {
                    Console.WriteLine("You are continue to choose seats.");
                    continue;
                }
                else
                {
                    Console.WriteLine("Wrong input!");
                    continue;
                }
            }
        }

        // After seat selection, make the reservation if the user is logged in
        if (selectedSeats.Count > 0)
        {
            if (acc != null)
            {
                Console.WriteLine("\nMaking reservation...");
                MakeReservation(selectedSeats, acc, showId); // Make reservation for selected seats
            }
            else
            {
                Console.WriteLine("No user logged in, cannot make a reservation.");
            }
        }
        else
        {
            Console.WriteLine("No seats selected.");
        }
    }

    // Get the price of a seat based on its category
    private decimal GetSeatPrice(int row, int col)
    {
        int chairType = pricingCategories[row, col];
        switch (chairType)
        {
            case 1:
                return 10.00m; // Premium price
            case 2:
                return 12.50m; // Standard price
            case 3:
                return 15.00m; // Basic price
            default:
                return 0.00m; // No price for unavailable seats
        }
    }

    protected void MakeReservation(List<SeatsModel> selectedSeats, UserModel acc, long showId)
    {
        List<VoucherModel> userVouchers = VoucherLogic.GetVouchersByUserId(acc);
        if (userVouchers.Count > 0)
        {
            Console.WriteLine("\nYou have active vouchers");
            Console.WriteLine("Would you like to use a voucher?");
            Console.WriteLine("[1] Yes");
            Console.WriteLine("[2] No");
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

                    VoucherModel voucher = userVouchers.ElementAtOrDefault(inputNum - 1); // the index is one smaller than the count
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
        Console.WriteLine("\nDo you want bar service? \n[1] Yes \n[2] No");
        bool barService = Console.ReadLine() == "1" && IsBarAvailable(selectedSeats.Count, showId);

        Console.WriteLine("\nWould you like to order snacks? \n[1] Yes \n[2] No");
        string snacks = "";

        if (Console.ReadLine() == "1")
        {
            Dictionary<MenuItem, int> selectedSnacks = SnackMenu.SelectSnacks();
            //string snack = "";

            foreach (var snack in selectedSnacks)
            {
                for (int i = 0; i < snack.Value; i++)
                {
                    if (snacks != string.Empty)
                    {
                        snacks += ", ";
                    }
                    snacks += snack.Key.Name;
                }
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


        Console.WriteLine($"Successfully reserved ticket(s) for {acc.FirstName} {acc.LastName}.");

        Thread.Sleep(2000);
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
                if (seats[row, j] == 'X')
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
            bool twoEmptySeats = false;
            bool nextToTwo = false;
            bool nextToOne = seats[row, col - 1] == 'R';
            bool PeopleLeftToSeat = false;
            bool nextToGroupMember = seats[row, col - 1] == 'C'; // Group member sits to the left of the selected seat

            // Ensure there's enough space from the row's far-left edge to check seat positions within array bounds.
            if ((col + 1) - (countEmptyLeftSpace + 1) >= 2)
            {
                // The conditions for a valid seat
                twoEmptySeats = seats[row, col - 2] == 'A' && seats[row, col - 1] == 'A';
                nextToTwo = seats[row, col - 2] == 'R' && seats[row, col - 1] == 'R';
                nextToOne = nextToOne && seats[row, col - 2] == 'A';
                PeopleLeftToSeat = peopleLeftToSeat == 1;

                // Additional condition where the furthest seat is reserved and the closer one is available
                if (seats[row, col - 2] == 'R' && seats[row, col - 1] == 'A')
                {
                    // Determine if the group needs more than one seat
                    PeopleLeftToSeat = peopleLeftToSeat >= 2 && peopleLeftToSeat < totalAmountOfPeople;

                    // Ensure that remaining seats to the right are filled first
                    if (PeopleLeftToSeat && seats[row, col + 1] == 'A' && seats[row, col + 2] == 'A')
                    {
                        return false;
                    }
                }
            }

            leftIsValid = twoEmptySeats || nextToTwo || nextToOne || nextToGroupMember || PeopleLeftToSeat;
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
            bool twoEmptySeats = false;
            bool nextToTwo = false;
            bool nextToOne = seats[row, col + 1] == 'R';
            bool PeopleLeftToSeat = false;
            bool nextToGroupMember = seats[row, col + 1] == 'C'; // Group member sits to the right of the selected seat

            // Ensure there's enough space from the row's far-right edge to check seat positions within array bounds.
            if (countSeatPlusLeftSpace - (col + 1) >= 2)
            {
                // The conditions for a valid seat
                twoEmptySeats = seats[row, col + 1] == 'A' && seats[row, col + 2] == 'A';
                nextToTwo = seats[row, col + 1] == 'R' && seats[row, col + 2] == 'R';
                nextToOne = nextToOne && seats[row, col + 2] == 'A';
                PeopleLeftToSeat = peopleLeftToSeat == 1;

                // Additional condition where the furthest seat is reserved and the closer one is available
                if (seats[row, col + 2] == 'R' && seats[row, col + 1] == 'A')
                {
                    // Determine if the group needs more than one seat
                    PeopleLeftToSeat = peopleLeftToSeat >= 2 && peopleLeftToSeat < totalAmountOfPeople;

                    // Ensure that remaining seats to the left are filled first
                    if (PeopleLeftToSeat && seats[row, col - 1] == 'A' && seats[row, col - 2] == 'A')
                    {
                        return false;
                    }
                }
            }

            rightIsValid = twoEmptySeats || nextToTwo || nextToOne || nextToGroupMember || PeopleLeftToSeat;
        }

        return leftIsValid && rightIsValid;
    }

    private bool IsValidGroupSeat(int row, int col)
    {
        /*
        Group seats must be next to each other
        */
        if ((col > 0 && seats[row, col - 1] == 'C') || (col + 1 < seats.GetLength(1) && seats[row, col + 1] == 'C'))
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
            Console.WriteLine("We're unable to take any more bar reservations. It is fully booked");
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