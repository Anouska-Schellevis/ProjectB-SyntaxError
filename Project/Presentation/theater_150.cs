using System;


public class Theater150
{
    public static char[,] seats; // 2D array for seat statuses (A = Available, C = Chosen)
    public static int[,] zaal150; // 2D array for pricing categories

    // Constructor to initialize arrays
    public Theater150()
    {
        // Initialize the zaal150 array with pricing categories
        zaal150 = new int[14, 12]
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

        // Initialize the seats array based on the zaal150 layout
        seats = new char[14, 12];
        for (int i = 0; i < seats.GetLength(0); i++)
        {
            for (int j = 0; j < seats.GetLength(1); j++)
            {
                seats[i, j] = zaal150[i, j] == 0 ? ' ' : 'A'; // 'A' for Available, ' ' for no seat
            }
        }
    }

    // Method to display seat layout
    public void DisplaySeats(long movieId)
    {

        List<long> reservedSeats = ReservationAccess.GetReservedSeatsByMovieId(movieId);
        int rows = seats.GetLength(0);
        int columns = seats.GetLength(1);

        // Print the column headers (1 to 12)
        Console.Write("   "); // Space for row numbers
        for (int j = 1; j <= columns; j++)
        {
            Console.Write($"{j,2}  ");
        }
        Console.WriteLine();


        for (int i = 0; i < rows; i++)
        {
            Console.ForegroundColor = ConsoleColor.Gray;


            Console.Write($"{14 - i,2}  ");

            for (int j = 0; j < columns; j++)
            {

                if (seats[i, j] == ' ')
                {
                    Console.Write("    ");
                }
                else
                {
                    int seatId = (i + 1) * columns + (j + 1);//i * columns + j + 1;
                    if (reservedSeats.Contains(seatId))  //mark as chosen if the seatId is in the reserved list
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;  //purple for already taken seat

                    }
                    else
                    {
                        switch (zaal150[i, j])
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


                    if (seats[i, j] == 'A')  //Available seat
                    {
                        Console.Write("■   ");
                    }
                    else if (seats[i, j] == 'C')  //Taken seat
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;  //Gray for taken seat
                        Console.Write("■   ");
                    }
                }
            }
            Console.WriteLine(); //Move to the next row
        }
        Console.ResetColor();
    }


    public void SelectSeats(long movieId)
    {
        List<long> reservedSeats = ReservationAccess.GetReservedSeatsByMovieId(movieId);
        DisplaySeats(movieId);

        Console.WriteLine("How many seats do you want to book?");
        int how_many_people = Convert.ToInt32(Console.ReadLine());

        List<SeatsModel> selectedSeats = new List<SeatsModel>();


        for (int i = 0; i < how_many_people; i++)
        {
            Console.WriteLine($"Booking seat {i + 1}");
            Console.WriteLine("Enter the row (1 to 14) and column (1 to 12) of the seat you want to select (e.g., 5 6):");
            string input = Console.ReadLine();


            string[] parts = input.Split(' ');

            if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
            {
                Console.WriteLine("Invalid input format. Please enter in the format: row column.");
                return;
            }


            if (row < 1 || row > 14 || col < 1 || col > 12)
            {
                Console.WriteLine("Invalid seat selection. Please choose a valid seat.");
                return;
            }

            row = 14 - row;
            col -= 1;


            if (seats[row, col] == 'A')
            {
                seats[row, col] = 'C';
                Console.WriteLine($"You have selected seat ({14 - row}, {col + 1}).");
                var seatId = (row * 12) + col;
                selectedSeats.Add(new SeatsModel
                {
                    Id = seatId,
                    RowNumber = 14 - row,
                    ColumnNumber = col + 1,
                    Price = zaal150[row, col]
                });

                DisplaySeats(movieId);
            }
            else if (seats[row, col] == 'C')
            {
                Console.WriteLine("Sorry, that seat is already taken.");
            }
            else
            {
                Console.WriteLine("Sorry, that seat is not available.");
            }
        }
        UserModel currentUser = UserSession.Instance.CurrentUser;
        if (currentUser != null)
        {
            MakeReservation(selectedSeats, currentUser, movieId); //pass the current user model, the one thats logged in, to MakeReservation method
        }
    }
    private void MakeReservation(List<SeatsModel> selectedSeats, UserModel currentUser, long movieId)
    {
        Int64 userId = currentUser.Id;

        Console.WriteLine("Do you want bar service? (yes/no):");
        bool barService = Console.ReadLine().ToLower() == "yes";

        foreach (var seat in selectedSeats)
        {
            SeatsLogic.WriteSeat(seat);
            //create a reservation for each selected seat, convert user id from int64 to int32 for seats model, 
            var reservation = new ReservationModel
            {
                Id = 0,
                Bar = barService,
                SeatsId = (int)seat.Id,
                UserId = Convert.ToInt32(userId),
                MovieId = (int)movieId
            };


            ReservationLogic.WriteReservation(reservation);
            Console.WriteLine($"Reserved seat ({seat.RowNumber}, {seat.ColumnNumber}) for User ID {currentUser.FirstName} {currentUser.LastName}.");
        }

    }
}