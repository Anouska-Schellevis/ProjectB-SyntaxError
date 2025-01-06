// using System;

// class Theater150
// {
//     static char[,] seats;
//     static int[,] zaal150;

//     public Theater150()
//     {
//         zaal150 = new int[14, 12]
//         {
//             { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 },
//             { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
//             { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
//             { 3, 3, 3, 3, 3, 2, 2, 3, 3, 3, 3, 3 },
//             { 3, 3, 3, 3, 2, 2, 2, 2, 3, 3, 3, 3 },
//             { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
//             { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
//             { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
//             { 3, 3, 3, 2, 2, 1, 1, 2, 2, 3, 3, 3 },
//             { 3, 3, 3, 3, 2, 2, 2, 2, 3, 3, 3, 3 },
//             { 3, 3, 3, 3, 3, 2, 2, 3, 3, 3, 3, 3 },
//             { 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
//             { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 },
//             { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 }
//         };

//         seats = new char[14, 12];
//         for (int i = 0; i < seats.GetLength(0); i++)
//         {
//             for (int j = 0; j < seats.GetLength(1); j++)
//             {
//                 seats[i, j] = zaal150[i, j] == 0 ? 'X' : 'A';
//             }
//         }
//     }

//     static void DisplaySeats(int selectedRow, int selectedCol)
//     {
//         int rows = seats.GetLength(0);
//         int columns = seats.GetLength(1);

//         Console.Clear();
//         Console.Write("   ");
//         for (int j = 1; j <= columns; j++)
//         {
//             Console.Write($"{j,2}  ");
//         }
//         Console.WriteLine();

//         for (int i = 0; i < rows; i++)
//         {
//             Console.ForegroundColor = ConsoleColor.Gray;

//             Console.Write($"{14 - i,2}  ");

//             for (int j = 0; j < columns; j++)
//             {
//                 switch (zaal150[i, j])
//                 {
//                     case 1:
//                         Console.ForegroundColor = ConsoleColor.Red;
//                         break;
//                     case 2:
//                         Console.ForegroundColor = ConsoleColor.Yellow;
//                         break;
//                     case 3:
//                         Console.ForegroundColor = ConsoleColor.Blue;
//                         break;
//                     default:
//                         Console.ForegroundColor = ConsoleColor.Gray;
//                         break;
//                 }
                

//                 // Check if the seat is selected
//                 if (i == selectedRow && j == selectedCol)
//                 {
//                     Console.ForegroundColor = ConsoleColor.Green; // Seat turns green if selected
//                 }

//                 if (seats[i, j] == 'A')
//                 {
//                     Console.Write("■   ");
//                 }
//                 else if (seats[i, j] == 'C')
//                 {
//                     Console.ForegroundColor = ConsoleColor.Magenta;
//                     Console.Write("■   ");
//                 }
//                 else if (seats[i, j] == 'X')
//                 {
//                     Console.ForegroundColor = ConsoleColor.Black;
//                     Console.Write("■   ");
//                 }

//                 Console.ResetColor();
//             }
//             Console.WriteLine();
//         }
//     }
    


//     static bool AreSeatsConnected(int row, int col, int count)
//     {
//         int connectedCount = 0;

//         for (int j = col; j < col + count; j++)
//         {
//             if (j < seats.GetLength(1) && seats[row, j] == 'A')
//             {
//                 connectedCount++;
//             }
//             else
//             {
//                 break;
//             }
//         }

//         if (connectedCount == count)
//             return true;

//         connectedCount = 0;

//         for (int i = row; i < row + count; i++)
//         {
//             if (i < seats.GetLength(0) && seats[i, col] == 'A')
//             {
//                 connectedCount++;
//             }
//             else
//             {
//                 break;
//             }
//         }

//         return connectedCount == count;
//     }

//     static void SelectSeats()
//     {
//         int selectedRow = 0, selectedCol = 0;

//         Console.WriteLine("How many seats do you want to book?");
//         int how_many_people = Convert.ToInt32(Console.ReadLine());

//         while (true)
//         {
//             DisplaySeats(selectedRow, selectedCol);

//             ConsoleKeyInfo key = Console.ReadKey(true);
            // if (key.Key == ConsoleKey.UpArrow && selectedRow > 0) 
            // {
            //     selectedRow--; // Move up
            // }
            // else if (key.Key == ConsoleKey.DownArrow && selectedRow < seats.GetLength(0) - 1)
            // {
            //     selectedRow++; // Move down
            // }
            // else if (key.Key == ConsoleKey.LeftArrow && selectedCol > 0)
            // {
            //     selectedCol--; // Move left
            // }
            // else if (key.Key == ConsoleKey.RightArrow && selectedCol < seats.GetLength(1) - 1)
            // {
            //     selectedCol++; // Move right
            // }
            // else if (key.Key == ConsoleKey.Enter)
            // {
            //     // Allow booking only if the seat is 'A' (available)
            //     if (seats[selectedRow, selectedCol] == 'A')
            //     {
            //         seats[selectedRow, selectedCol] = 'C';
            //         Console.WriteLine($"You have selected seat ({14 - selectedRow}, {selectedCol + 1}).");
            //         break;
            //     }
            //     else
            //     {
            //         Console.WriteLine("This seat is already taken or invalid.");
            //     }
            // }
            // else if (key.Key == ConsoleKey.Backspace)
            // {
            //     if (seats[selectedRow, selectedCol] == 'C')
            //     {
            //         seats[selectedRow, selectedCol] = 'A';
            //         Console.WriteLine($"You have deleted seat ({14 - selectedRow}, { selectedCol + 1}).");
            //         break;
            //     }
            //     else
            //     {
            //         Console.WriteLine($"this seat cant be deleted.");
            //     }
            // }
//         }
//     }

//     static void Main(string[] args)
//     {
//         Theater150 theater = new Theater150();

//         while (true)
//         {
//             SelectSeats();
//             Console.WriteLine("Do you want to select another seat? (y/n)");
//             string choice = Console.ReadLine();
//             if (choice.ToLower() != "y")
//             {
//                 break;
//             }
//         }
//     }
// }



