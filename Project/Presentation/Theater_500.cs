class Theater500
{
    static char[,] seats; 
    static int[,] zaal500; 

    
    public Theater500()
    {
       
        zaal500 = new int[20, 30]
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

        
        seats = new char[20, 30];
        for (int i = 0; i < seats.GetLength(0); i++)
        {
            for (int j = 0; j < seats.GetLength(1); j++)
            {
                seats[i, j] = zaal500[i, j] == 0 ? ' ' : 'A'; 
            }
        }
    }

   
    static void DisplaySeats(long movieId)
    {
        List<long> reservedSeats = ReservationAccess.GetReservedSeatsByShowId(movieId);
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

            
            Console.Write($"{20 - i,2}  "); 

            for (int j = 0; j < columns; j++)
            {
                if (seats[i, j] == ' ')
                {
                    Console.Write("    "); 
                }
                else
                {
                    int seatId = (i * columns) + j + 1;//i * columns + j + 1;
                    if (reservedSeats.Contains(seatId))  //mark as chosen if the seatId is in the reserved list
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;  //purple for already taken seat

                    }
                    else
                    {
                    
                        switch (zaal500[i, j])
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
                        Console.ForegroundColor = ConsoleColor.DarkGray;  
                        Console.Write("■   ");
                    }
                }
            }
            Console.WriteLine(); 
        }
        Console.ResetColor();
    }

    
    public void SelectSeats(long movieId)
    {
        List<long> reservedSeats = ReservationAccess.GetReservedSeatsByShowId(movieId);
        DisplaySeats(movieId);

        Console.WriteLine("How many seats do you want to book?");
        int how_many_people = Convert.ToInt32(Console.ReadLine());
        List<SeatsModel> selectedSeats = new List<SeatsModel>();

        for (int i = 0; i < how_many_people; i++)
        {
            Console.WriteLine($"Booking seat {i + 1}");
            Console.WriteLine("Enter the row (1 to 20) and column (1 to 30) of the seat you want to select (e.g., 5 6):");
            string input = Console.ReadLine(); 

            string[] parts = input.Split(' '); 

           
            if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
            {
                Console.WriteLine("Invalid input format. Please enter in the format: row column.");
                return;
            }

            
            if (row < 1 || row > 20 || col < 1 || col > 30)
            {
                Console.WriteLine("Invalid seat selection. Please choose a valid seat.");
                return;
            }

            
            row = 20 - row; 
            col -= 1; 

            
            if (seats[row, col] == 'A')
            {
                seats[row, col] = 'C'; 
                Console.WriteLine($"You have selected seat ({20 - row}, {col + 1}).");
                var seatId = (row * 30) + col;
               
                selectedSeats.Add(new SeatsModel
                {
                    Id = seatId,
                    RowNumber = 20 - row, 
                    ColumnNumber = col + 1, 
                    Price = zaal500[row, col] 
                });

                DisplaySeats(movieId); 
            }
            else if (seats[row, col] == 'C')
            {
                Console.WriteLine("Sorry, that seat is already taken.");
                i--; 
            }
            else
            {
                Console.WriteLine("Sorry, that seat is not available.");
                i--; 
            }
        }

        
        UserModel currentUser = UserSession.Instance.CurrentUser;
        if (currentUser != null)
        {
            MakeReservation(selectedSeats, currentUser, movieId); 
        }
    }

    private void MakeReservation(List<SeatsModel> selectedSeats, UserModel currentUser, long showId)
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
                SeatsId = (seat.RowNumber - 1) * 30 + (seat.ColumnNumber - 1), 
                UserId = Convert.ToInt32(userId),
                ShowId = (int)showId 
            };

            
            ReservationLogic.WriteReservation(reservation);
            //Console.WriteLine($"Successfully reserved seats for {currentUser.FirstName} {currentUser.LastName}.");
        }
        Console.WriteLine($"Successfully reserved seats for {currentUser.FirstName} {currentUser.LastName}.");
    }
    
}