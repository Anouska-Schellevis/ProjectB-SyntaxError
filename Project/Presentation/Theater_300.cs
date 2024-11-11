// // using System;

// // class Theater300
// // {
// //     static char[,] seats; // 2D array for seat statuses (A = Available, C = Chosen)
// //     static int[,] zaal300; // 2D array for pricing categories

// //     // Constructor to initialize arrays
// //     public Theater300()
// //     {
// //         // Initialize the zaal300 array with pricing categories
// //         zaal300 = new int[19, 18]
// //         {
//             // { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
//             // { 0, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 0 },
//             // { 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0 },
//             // { 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0 },
//             // { 0, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 0 },
//             // { 0, 3, 3, 3, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 3, 3, 3, 0 },
//             // { 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 },
//             // { 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3 },
//             // { 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
//             // { 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
//             // { 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
//             // { 0, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 0 },
//             // { 0, 3, 3, 3, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 3, 3, 3, 0 },
//             // { 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0 },
//             // { 0, 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0, 0 },
//             // { 0, 0, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0, 0 },
//             // { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 },
//             // { 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0 },
//             // { 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0 }
// //         };

// //         // Initialize the seats array based on the zaal300 layout
// //         seats = new char[19, 18];
// //         for (int i = 0; i < seats.GetLength(0); i++)
// //         {
// //             for (int j = 0; j < seats.GetLength(1); j++)
// //             {
// //                 seats[i, j] = zaal300[i, j] == 0 ? ' ' : 'A'; // 'A' for Available, ' ' for no seat
// //             }
// //         }
// //     }

// //     // Method to display seat layout
// //     static void DisplaySeats()
// //     {
// //         int rows = seats.GetLength(0);
// //         int columns = seats.GetLength(1);

// //         // Print the column headers (1 to 18)
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
// //             Console.Write($"{19 - i,2}  "); // Use 2 spaces for all row numbers

// //             for (int j = 0; j < columns; j++)
// //             {
// //                 // Check if there's a seat in this position
// //                 if (seats[i, j] == ' ')
// //                 {
// //                     Console.Write("    "); // Space for no seat
// //                 }
// //                 else
// //                 {
// //                     // Set color based on zaal300 category
// //                     switch (zaal300[i, j])
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
// //                         Console.Write("■   "); // Use '■' to represent a seat
// //                     }
// //                     else if (seats[i, j] == 'C')  // Chosen seat
// //                     {
// //                         Console.ForegroundColor = ConsoleColor.DarkGray;  // Gray for chosen seat
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

// //             // Input validation
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

// //             // Adjust indices for seat array
// //             row = 14 - row; // Convert user row input to the correct index
// //             col -= 1; // Convert user column input to 0-based index

// //             // Check if the selected seat is available
// //             if (seats[row, col] == 'A')
// //             {
// //                 seats[row, col] = 'C'; // Mark seat as chosen
// //                 Console.WriteLine($"You have selected seat ({14 - row}, {col + 1}).");

// //                 // Add to the selected seats list
// //                 selectedSeats.Add(new SeatsModel
// //                 {
// //                     RowNumber = 14 - row, // Converting back to user-facing row number
// //                     ColumnNumber = col + 1, // Converting to user-facing column number
// //                     Price = zaal300[row, col] // Assuming price can be accessed from zaal150
// //                 });
// //             }
// //             else if (seats[row, col] == 'C')
// //             {
// //                 Console.WriteLine("Sorry, that seat is already taken.");
// //                 i--; // Decrement i to repeat this iteration for a new seat selection
// //             }
// //             else
// //             {
// //                 Console.WriteLine("Sorry, that seat is not available.");
// //                 i--; // Decrement i to repeat this iteration for a new seat selection
// //             }
// //         }

// //         // Proceed with reservation after seat selection
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
// //                 Id = 0, // Assuming ID will be generated in the database
// //                 Bar = barService,
// //                 SeatsId = (seat.RowNumber - 1) * 12 + (seat.ColumnNumber - 1), // Calculate seat ID
// //                 UserId = Convert.ToInt32(userId),
// //                 MovieId = 1 // Assuming MovieId is fixed for demonstration
// //             };

// //             // Write reservation to the database
// //             ReservationLogic.WriteReservation(reservation);
// //             Console.WriteLine($"Reserved seat ({seat.RowNumber}, {seat.ColumnNumber}) for User ID {currentUser.FirstName} {currentUser.LastName}.");
// //         }
// //     }
// // }



// public class Theater300 : TheaterBase
// {
//     protected override void InitializeLayout()
//     {
//         pricingCategories = new int[15, 20] // Example layout for Theater300
//         {
//             { 3, 3, 3, 3, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3 },
//             { 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3 },
//             { 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 },
//             { 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3 },
//             { 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3 },
//             { 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
//             { 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3 },
//             { 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3 },
//             { 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3 },
//             { 3, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3 },
//             { 3, 3, 3, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3 },
//             { 3, 3, 3, 3, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3 },
//             { 3, 3, 3, 3, 3, 2, 2, 2, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3 },
//             { 3, 3, 3, 3, 3, 3, 2, 2, 2, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3 },
//             { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }
//         };
//     }
// }