// // using System;

// // public class Theater150
// // {
// //     public static char[,] seats; // 2D array for seat statuses (A = Available, C = Chosen)
// //     public static int[,] zaal150; // 2D array for pricing categories

// //     // Constructor to initialize arrays
// //     public Theater150()
// //     {
// //         // Initialize the zaal150 array with pricing categories
// //         zaal150 = new int[14, 12]
// //         {
//             // { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 },
//             // { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
//             // { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
//             // { 3, 3, 3, 3, 3, 2, 2, 3, 3, 3, 3, 3 },
//             // { 3, 3, 3, 3, 2, 2, 2, 2, 3, 3, 3, 3 },
//             // { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
//             // { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
//             // { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
//             // { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
//             // { 3, 3, 3, 3, 2, 2, 2, 2, 3, 3, 3, 3 },
//             // { 3, 3, 3, 3, 3, 2, 2, 3, 3, 3, 3, 3 },
//             // { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
//             // { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 },
//             // { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 }
// //         };

// //         // Initialize the seats array based on the zaal150 layout
// //         seats = new char[14, 12];
// //         for (int i = 0; i < seats.GetLength(0); i++)
// //         {
// //             for (int j = 0; j < seats.GetLength(1); j++)
// //             {
// //                 seats[i, j] = zaal150[i, j] == 0 ? ' ' : 'A'; // 'A' for Available, ' ' for no seat
// //             }
// //         }
// //     }

// //     // Method to display seat layout
// //     public void DisplaySeats()
// //     {
// //         int rows = seats.GetLength(0);
// //         int columns = seats.GetLength(1);

// //         // Print the column headers (1 to 12)
// //         Console.Write("   "); // Space for row numbers
// //         for (int j = 1; j <= columns; j++)
// //         {
// //             Console.Write($"{j,2}  "); // Print column numbers with spaces
// //         }
// //         Console.WriteLine(); // Move to the next line

// //         // Print each row with seat availability
// //         for (int i = 0; i < rows; i++)
// //         {
// //             Console.ForegroundColor = ConsoleColor.Gray;

// //             // Print the row number, ensuring proper alignment
// //             Console.Write($"{14 - i,2}  "); // Use 2 spaces for all row numbers

// //             for (int j = 0; j < columns; j++)
// //             {
// //                 // Check if there's a seat in this position
// //                 if (seats[i, j] == ' ')
// //                 {
// //                     Console.Write("    "); // Space for no seat
// //                 }
// //                 else
// //                 {
// //                     // Set color based on zaal150 category
// //                     switch (zaal150[i, j])
// //                     {
// //                         case 1: // Category 1 (Red)
// //                             Console.ForegroundColor = ConsoleColor.Red;
// //                             break;
// //                         case 2: // Category 2 (Yellow)
// //                             Console.ForegroundColor = ConsoleColor.Yellow;
// //                             break;
// //                         case 3: // Category 3 (Blue)
// //                             Console.ForegroundColor = ConsoleColor.Blue;
// //                             break;
// //                         default:
// //                             Console.ForegroundColor = ConsoleColor.Gray;
// //                             break;
// //                     }

// //                     // Print the seat status with aligned spacing
// //                     if (seats[i, j] == 'A')  // Available seat
// //                     {
// //                         Console.Write("■   "); // Use 'A' to avoid character rendering issues
// //                     }
// //                     else if (seats[i, j] == 'C')  // Taken seat
// //                     {
// //                         Console.ForegroundColor = ConsoleColor.DarkGray;  // Gray for taken seat
// //                         Console.Write("■   ");
// //                     }
// //                 }
// //             }
// //             Console.WriteLine(); // Move to the next row
// //         }
// //         Console.ResetColor();
// //     }

// //     // Method to select seats
// //     public void SelectSeats()
// //     {
// //         DisplaySeats();
// //         Console.WriteLine("How many seats do you want to book?");
// //         int how_many_people = Convert.ToInt32(Console.ReadLine());

// //         List<SeatsModel> selectedSeats = new List<SeatsModel>();


// //         for (int i = 0; i < how_many_people; i++)
// //         {
// //             Console.WriteLine($"Booking seat {i + 1}");
// //             Console.WriteLine("Enter the row (1 to 14) and column (1 to 12) of the seat you want to select (e.g., 5 6):");
// //             string input = Console.ReadLine(); // Read user input


// //             string[] parts = input.Split(' '); // Split input into row and column

// //             if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
// //             {
// //                 Console.WriteLine("Invalid input format. Please enter in the format: row column.");
// //                 return;
// //             }

// //             // Validate row and column input range (1 to 14 for rows, 1 to 12 for columns)
// //             if (row < 1 || row > 14 || col < 1 || col > 12)
// //             {
// //                 Console.WriteLine("Invalid seat selection. Please choose a valid seat.");
// //                 return;
// //             }

// //             row = 14 - row; // Convert user row input to the correct index
// //             col -= 1; // Convert user column input to 0-based index

// //             // Check if the selected seat is available
// //             if (seats[row, col] == 'A')
// //             {
// //                 seats[row, col] = 'C'; // Mark seat as chosen
// //                 Console.WriteLine($"You have selected seat ({14 - row}, {col + 1}).");
// //                 var seatId = (row * 12) + col;
// //                 selectedSeats.Add(new SeatsModel
// //                 {
// //                     Id = seatId, // Ensure your SeatsModel has an Id assigned
// //                     RowNumber = 14 - row, // Converting back to user-facing row number
// //                     ColumnNumber = col + 1, // Converting to user-facing column number
// //                     Price = zaal150[row, col] // Assuming price can be directly accessed from zaal150
// //                 });

// //                 DisplaySeats();
// //             }
// //             else if (seats[row, col] == 'C')
// //             {
// //                 Console.WriteLine("Sorry, that seat is already taken.");
// //             }
// //             else
// //             {
// //                 Console.WriteLine("Sorry, that seat is not available.");
// //             }
// //         }
// //         UserModel currentUser = UserSession.Instance.CurrentUser;
// //         if (currentUser != null)
// //         {
// //             MakeReservation(selectedSeats, currentUser); // Pass the current user model to MakeReservation
// //         }
// //     }
// //     private void MakeReservation(List<SeatsModel> selectedSeats, UserModel currentUser)
// //     {
// //         Int64 userId = currentUser.Id;

// //         Console.WriteLine("Do you want bar service? (yes/no):");
// //         bool barService = Console.ReadLine().ToLower() == "yes";

// //         foreach (var seat in selectedSeats)
// //         {
// //             // Create a reservation for each selected seat
// //             var reservation = new ReservationModel
// //             {
// //                 Id = 0, 
// //                 Bar = barService, 
// //                 SeatsId = (int)seat.Id,
// //                 UserId = Convert.ToInt32(userId),
// //                 MovieId = 1 
// //             };

// //             // Write reservation to the database
// //             ReservationLogic.WriteReservation(reservation);
// //             Console.WriteLine($"Reserved seat ({seat.RowNumber}, {seat.ColumnNumber}) for User ID {currentUser.FirstName} {currentUser.LastName}.");
// //         }

// //     }
// // }


// public class Theater150 : TheaterBase
// {
//     protected override void InitializeLayout()
//     {
//         pricingCategories = new int[10, 15] // Example layout for Theater150
//         {
//             { 3, 3, 3, 3, 2, 2, 1, 1, 1, 2, 2, 3, 3, 3, 3 },
//             { 3, 3, 3, 2, 2, 2, 1, 1, 1, 2, 2, 2, 3, 3, 3 },
//             { 3, 3, 3, 2, 2, 2, 1, 1, 1, 2, 2, 2, 3, 3, 3 },
//             { 3, 3, 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
//             { 3, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3 },
//             { 3, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3 },
//             { 3, 3, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3 },
//             { 3, 3, 3, 2, 2, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3 },
//             { 3, 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3, 3, 3 },
//             { 3, 3, 3, 3, 3, 3, 2, 2, 3, 3, 3, 3, 3, 3, 3 }
//         };
//     }
// }
