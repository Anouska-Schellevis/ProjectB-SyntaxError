using System;
using System.Runtime.CompilerServices;

public abstract class TheaterBase
{
    protected char[,] seats; // 2D array for seat statuses (A = Available, C = Chosen)
    protected int[,] pricingCategories; // 2D array for pricing categories

    // Constructor to initialize seats with dynamic layout and pricing categories
    public TheaterBase(int rows, int columns, int[,] pricingCategories)
    {
        this.pricingCategories = pricingCategories;
        InitializeSeats();
    }

    // Initializes seats based on the pricing layout
    protected void InitializeSeats()
    {
        seats = new char[pricingCategories.GetLength(0), pricingCategories.GetLength(1)];
        for (int i = 0; i < seats.GetLength(0); i++)
        {
            for (int j = 0; j < seats.GetLength(1); j++)
            {
                seats[i, j] = pricingCategories[i, j] == 0 ? ' ' : 'A'; // 'A' for Available, ' ' for no seat
            }
        }
    }

    // Method to display seat layout
    public void DisplaySeats(long movieId)
    {
        List<long> reservedSeats = ReservationAccess.GetReservedSeatsByMovieId(movieId);
        int rows = seats.GetLength(0);
        int columns = seats.GetLength(1);

        Console.Write("   ");
        // for (int j = 1; j <= columns; j++)
        // {
        //     Console.Write($"{j,2}  ");
        // }
        // Console.WriteLine();

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
                    int seatId = (i + 1) * columns + (j + 1);//i * columns + j + 1;
                    if (reservedSeats.Contains(seatId))  //mark as chosen if the seatId is in the reserved list
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;  //purple for already taken seat
                    }
                    else
                    {
                        switch (pricingCategories[i, j])
                        {
                            case 1: Console.ForegroundColor = ConsoleColor.Red; break;
                            case 2: Console.ForegroundColor = ConsoleColor.Yellow; break;
                            case 3: Console.ForegroundColor = ConsoleColor.Blue; break;
                            default: Console.ForegroundColor = ConsoleColor.Gray; break;
                        }
                    }
                    Console.Write(seats[i, j] == 'A' ? "■   " : "■   ");
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }

    // Method to select seats
    public void SelectSeats(long movieId)
    {
        List<long> reserved_seats = ReservationAccess.GetReservedSeatsByMovieId(movieId);
        DisplaySeats(movieId);
        Console.WriteLine("How many seats do you want to book?");
        int how_many_people = Convert.ToInt32(Console.ReadLine());
        List<SeatsModel> selected_seats = new List<SeatsModel>();

        for (int i = 0; i < how_many_people; i++)
        {
            Console.WriteLine($"Booking seat {i + 1}");
            Console.WriteLine("Enter the row and column of the seat (e.g., 5 6):");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
            {
                Console.WriteLine("Invalid input format.");
                return;
            }

            row = seats.GetLength(0) - row; col -= 1;

            if (row < 0 || row >= seats.GetLength(0) || col < 0 || col >= seats.GetLength(1))
            {
                Console.WriteLine("Invalid seat selection.");
                return;
            }

            if (seats[row, col] == 'A')
            {
                seats[row, col] = 'C';
                Console.WriteLine($"You have selected seat ({seats.GetLength(0) - row}, {col + 1}).");
                var seatId = (row * seats.GetLength(1)) + col;
                selected_seats.Add(new SeatsModel
                {
                    Id = seatId,
                    RowNumber = seats.GetLength(0) - row,
                    ColumnNumber = col + 1,
                    Price = pricingCategories[row, col]
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
            MakeReservation(selected_seats, currentUser, movieId);
        }
    }

    // Method to make reservation
    private void MakeReservation(List<SeatsModel> selectedSeats, UserModel currentUser, long movieId)
    {
        Int64 userId = currentUser.Id;
        Console.WriteLine("Do you want bar service? (yes/no):");
        bool barService = Console.ReadLine().ToLower() == "yes";

        foreach (var seat in selectedSeats)
        {
            SeatsLogic.WriteSeat(seat);
            var reservation = new ReservationModel
            {
                Id = 0,
                Bar = barService,
                SeatsId = (int)seat.Id,
                UserId = Convert.ToInt32(userId),
                MovieId = (int)movieId,
            };

            ReservationLogic.WriteReservation(reservation);
            Console.WriteLine("You reserved a seat/your seats.");
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
                pricingCategories = new int[20,30]
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
