public class Reservation
{
    public static void SeeReservation(UserModel acc)
    {
        Console.Clear();

        Console.WriteLine($"Welcome, {acc.FirstName}!");

        List<ReservationModel> userReservations = ReservationAccess.GetReservationsByUserId(acc.Id);

        if (userReservations.Count == 0)
        {
            Console.WriteLine("You have no reservations.");
            return;
        }

        var groupedReservations = userReservations
            .GroupBy(reservation => reservation.ShowId)
            .OrderBy(group => group.Key)
            .ToList();

        Console.WriteLine("Your reservations:");
        int reservationNumber = 1;

        foreach (var group in groupedReservations)
        {
            ShowModel reservedShow = ShowAccess.GetByID(group.Key);
            MoviesModel reservedMovie = MoviesAccess.GetByLongId(reservedShow.MovieId);

            if (reservedMovie != null)
            {
                Console.WriteLine($"[{reservationNumber}]");
                Console.WriteLine($"    Movie: {reservedMovie.Title}, Show Date: {reservedShow.Date}, Bar reservation: {(group.First().Bar ? "Yes" : "No")}");

                foreach (var reservation in group)
                {
                    SeatsModel seat = SeatsAccess.GetById((int)reservation.SeatsId);
                    if (seat != null)
                    {
                        Console.WriteLine($"    Seat Row: {seat.RowNumber}, Chair: {seat.ColumnNumber}");
                    }
                    else
                    {
                        Console.WriteLine("    Seat information not available.");
                    }
                }
                //check if there are any snacks to show
                string snacks = group.First().Snacks;
                if (!string.IsNullOrEmpty(snacks)) // Check if snacks is not null or empty
                {
                    Console.WriteLine("    Ordered Snacks:");

                    //split the snacks string into a list
                    string[] snackList = snacks.Split(',');

                    //create a dictionary to count how many times a snack is in the string
                    Dictionary<string, int> snackCounts = new Dictionary<string, int>();
                    foreach (string snack in snackList.Select(s => s.Trim())) //lambda do trim extra white space because of spaces in item names
                    {
                        if (snackCounts.ContainsKey(snack))
                        {
                            snackCounts[snack]++;
                        }
                        else
                        {
                            snackCounts[snack] = 1;
                        }
                    }


                    foreach (var snack in snackCounts)
                    {
                        Console.WriteLine($"        - {snack.Value} x {snack.Key}");
                    }
                }

                reservationNumber++;
            }
            else
            {
                Console.WriteLine($"Error: No movie found for ShowId: {group.Key}");
            }
        }

        bool menuChoice = false;
        while (!menuChoice)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("[1] Cancel a reservation");
            Console.WriteLine("[2] Go back to the user menu");
            string userInput = Console.ReadLine();

            if (userInput == "1")
            {
                CancelReservation(groupedReservations, acc); // Pass the grouped reservations
                menuChoice = true;
            }
            else if (userInput == "2")
            {
                User.Start(acc);
                menuChoice = true;
            }
            else
            {
                Console.WriteLine("Invalid option, please try again.");
                Thread.Sleep(2000);

                /*
                The cursor goes back to the position of the option message, which is replaced after 2 seconds with an empty string.
                */
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }
    }

    public static void CancelReservation(List<IGrouping<int, ReservationModel>> groupedReservations, UserModel acc)
    {
        bool reservationChoice = false;

        while (!reservationChoice)
        {
            try
            {
                Console.WriteLine("Enter the number of the reservation group you want to cancel:");
                int userInput = Convert.ToInt32(Console.ReadLine());

                if (userInput < 1 || userInput > groupedReservations.Count)
                {
                    Console.WriteLine($"Invalid reservation number. Please choose a number between 1 and {groupedReservations.Count}.");
                    Thread.Sleep(2000);

                    /*
                    The cursor goes back to the position of the option message,
                    which is replaced after 2 seconds with an empty string.
                    */
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    continue;
                }

                var selectedGroup = groupedReservations[userInput - 1];
                ShowModel show = ShowAccess.GetByID(selectedGroup.Key);
                MoviesModel movie = MoviesAccess.GetByLongId(show.MovieId);

                if (movie != null)
                {
                    Console.Clear();
                    Console.WriteLine($"You have selected the movie: {movie.Title} on {show.Date} to cancel.");
                    Console.WriteLine("Are you sure you want to cancel this reservation?");
                    Console.WriteLine("[1] Yes");
                    Console.WriteLine("[2] No");

                    string confirmationInput = Console.ReadLine();

                    if (confirmationInput == "1")
                    {
                        foreach (var reservation in selectedGroup)
                        {
                            SeatsAccess.Delete((int)reservation.SeatsId);
                            ReservationAccess.Delete((int)reservation.Id);
                        }

                        Console.WriteLine($"Successfully canceled reservation for {movie.Title} on {show.Date}.");
                        Thread.Sleep(2000);
                        reservationChoice = true;

                        bool menuChoice = false;
                        while (!menuChoice)
                        {
                            Console.WriteLine("\nChoose an option:");
                            Console.WriteLine("[1] Cancel another reservation");
                            Console.WriteLine("[2] Go back to the user menu");
                            string userInput2 = Console.ReadLine();

                            if (userInput2 == "1")
                            {
                                CancelReservation(groupedReservations, acc);
                                menuChoice = true;
                            }
                            else if (userInput2 == "2")
                            {
                                User.Start(acc);
                                menuChoice = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid option, please try again.");
                                Thread.Sleep(2000);

                                /*
                                The cursor goes back to the position of the option message,
                                which is replaced after 2 seconds with an empty string.
                                */
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                            }
                        }
                    }
                    else if (confirmationInput == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("Reservation cancellation stopped.");
                        reservationChoice = true;

                        bool menuChoice = false;
                        while (!menuChoice)
                        {
                            Console.WriteLine("\nChoose an option:");
                            Console.WriteLine("[1] Cancel another reservation");
                            Console.WriteLine("[2] Go back to the user menu");
                            string userInput2 = Console.ReadLine();

                            if (userInput2 == "1")
                            {
                                CancelReservation(groupedReservations, acc);
                                menuChoice = true;
                            }
                            else if (userInput2 == "2")
                            {
                                User.Start(acc);
                                menuChoice = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid option, please try again.");
                                Thread.Sleep(2000);

                                /*
                                The cursor goes back to the position of the option message,
                                which is replaced after 2 seconds with an empty string.
                                */
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please choose [1] Yes or [2] No.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Unable to find movie for the selected reservation.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Thread.Sleep(2000);

                /*
                The cursor goes back to the position of the option message,
                which is replaced after 2 seconds with an empty string.
                */
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }
    }


}