using System;

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
    public virtual void DisplaySeats()
    {
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
                    switch (pricingCategories[i, j])
                    {
                        case 1: Console.ForegroundColor = ConsoleColor.Red; break;
                        case 2: Console.ForegroundColor = ConsoleColor.Yellow; break;
                        case 3: Console.ForegroundColor = ConsoleColor.Blue; break;
                        default: Console.ForegroundColor = ConsoleColor.Gray; break;
                    }

                    Console.Write(seats[i, j] == 'A' ? "■   " : "■   ");
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }

    // Method to select seats
    public void SelectSeats()
    {
        DisplaySeats();
        Console.WriteLine("How many seats do you want to book?");
        int howManyPeople = Convert.ToInt32(Console.ReadLine());
        List<SeatsModel> selectedSeats = new List<SeatsModel>();

        for (int i = 0; i < howManyPeople; i++)
        {
            Console.WriteLine($"Booking seat {i + 1}");
            Console.WriteLine("Enter the row and column of the seat (e.g., 5 6):");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
            {
                Console.WriteLine("Invalid input format.");
                i--; continue;
            }

            row = seats.GetLength(0) - row; col -= 1;

            if (row < 0 || row >= seats.GetLength(0) || col < 0 || col >= seats.GetLength(1))
            {
                Console.WriteLine("Invalid seat selection.");
                i--; continue;
            }

            if (seats[row, col] == 'A')
            {
                seats[row, col] = 'C';
                Console.WriteLine($"You have selected seat ({seats.GetLength(0) - row}, {col + 1}).");
                selectedSeats.Add(new SeatsModel
                {
                    RowNumber = seats.GetLength(0) - row,
                    ColumnNumber = col + 1,
                    Price = pricingCategories[row, col]
                });
                DisplaySeats();
            }
            else
            {
                Console.WriteLine("Sorry, that seat is already taken.");
                i--;
            }
        }

        UserModel currentUser = UserSession.Instance.CurrentUser;
        if (currentUser != null)
        {
            MakeReservation(selectedSeats, currentUser);
        }
    }

    // Method to make reservation
    private void MakeReservation(List<SeatsModel> selectedSeats, UserModel currentUser)
    {
        Int64 userId = currentUser.Id;
        Console.WriteLine("Do you want bar service? (yes/no):");
        bool barService = Console.ReadLine().ToLower() == "yes";

        foreach (var seat in selectedSeats)
        {
            var reservation = new ReservationModel
            {
                Id = 0,
                Bar = barService,
                SeatsId = (seat.RowNumber - 1) * seats.GetLength(1) + (seat.ColumnNumber - 1),
                UserId = Convert.ToInt32(userId),
                MovieId = 1
            };

            ReservationLogic.WriteReservation(reservation);
            Console.WriteLine($"Reserved seat ({seat.RowNumber}, {seat.ColumnNumber}) for User ID {currentUser.FirstName} {currentUser.LastName}.");
        }
    }
}


public class ConcreteTheater : TheaterBase
{
    public ConcreteTheater(int[,] pricingCategories) : base(pricingCategories.GetLength(0), pricingCategories.GetLength(1), pricingCategories) { }
}


public class Theater
{
    public static TheaterBase GetTheater(int theaterType)
    {
        int[,] pricingCategories = null;

        switch (theaterType)
        {
            case 150:
                pricingCategories = new int[10, 15]
                {
                    { 3, 3, 3, 3, 2, 2, 1, 1, 1, 2, 2, 3, 3, 3, 3 },
                    { 3, 3, 3, 2, 2, 2, 1, 1, 1, 2, 2, 2, 3, 3, 3 },
                    { 3, 3, 3, 2, 2, 2, 1, 1, 1, 2, 2, 2, 3, 3, 3 },
                    { 3, 3, 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
                    { 3, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3 },
                    { 3, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3 },
                    { 3, 3, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3 },
                    { 3, 3, 3, 2, 2, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 3, 3, 2, 2, 3, 3, 3, 3, 3, 3, 3 }
                };
                return new ConcreteTheater(pricingCategories);

            case 300:
                pricingCategories = new int[15, 20]
                {
                    { 3, 3, 3, 3, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3 },
                    { 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 },
                    { 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3 },
                    { 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3 },
                    { 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
                    { 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
                    { 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3 },
                    { 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 3, 2, 2, 2, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 3, 3, 2, 2, 2, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3 },
                    { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }
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
