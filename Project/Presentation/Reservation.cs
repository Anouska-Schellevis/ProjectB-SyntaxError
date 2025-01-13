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
            .ToList(); //group all user reservations by show id, show id is group KEY

        Console.WriteLine("Your reservations:");
        int reservationNumber = 1; //number to increment for amount of reservations

        foreach (var group in groupedReservations)
        {
            ShowModel reservedShow = ShowAccess.GetByID(group.Key); //get the show model with the group key aka the show id that groups that group
            MoviesModel reservedMovie = MoviesAccess.GetByLongId(reservedShow.MovieId); //get the movie model from that show for the name 

            if (reservedMovie != null)
            {
                Console.WriteLine($"Reservation [{reservationNumber}]");
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
                var flatReservations = groupedReservations.SelectMany(group => group).ToList();
                //grouped reservations holds multiple lists, this flat reservations makes it all into one list
                CancelReservation(flatReservations, acc);
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

    public static void CancelReservation(List<ReservationModel> userReservations, UserModel acc)
    {
        bool reservationChoice = false;

        while (!reservationChoice)
        {
            int userInput;

            try
            {
                Console.WriteLine("Enter the number of the reservation you want to cancel:");
                userInput = Convert.ToInt32(Console.ReadLine());

                if (userInput < 1 || userInput > userReservations.Count)
                {
                    Console.WriteLine($"Invalid reservation number. Please choose a number between 1 and {userReservations.Count}.");
                    Thread.Sleep(2000);

                    /*
                    The cursor goes back to the position of the option message, which is replaced after 2 seconds with an empty string.
                    */
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    continue;
                }

                var selectedReservation = userReservations[userInput - 1];

                if (selectedReservation == null)
                {
                    Console.WriteLine("Reservation doesn't exist.");
                    return;
                }

                ShowModel reservedShow = ShowAccess.GetByID((int)selectedReservation.ShowId);
                MoviesModel reservedMovie = MoviesAccess.GetByLongId(reservedShow.MovieId);

                Console.Clear();
                Console.WriteLine($"You have selected this Movie: {reservedMovie.Title} on this Show Date: {reservedShow.Date} to cancel");

                Console.WriteLine("Are you sure you want to cancel this reservation?");
                Console.WriteLine("[1] Yes");
                Console.WriteLine("[2] No");

                string confirmationInput = Console.ReadLine();

                if (confirmationInput == "1")
                {
                    Console.Clear();
                    var groupedReservations = userReservations
                        .Where(r => r.ShowId == selectedReservation.ShowId)
                        .ToList();

                    var reservedSeatIds = groupedReservations.Select(r => r.SeatsId).ToList();

                    if (reservedSeatIds.Count == 0)
                    {
                        Console.WriteLine($"No reserved seats found for {reservedMovie.Title}, Show Date: {reservedShow.Date}");
                        return;
                    }

                    foreach (var seatId in reservedSeatIds)
                    {
                        int seatIdInt = (int)seatId;
                        SeatsAccess.Delete(seatIdInt);
                    }

                    foreach (var reservation in groupedReservations)
                    {
                        ReservationAccess.Delete((int)reservation.Id);
                    }

                    Console.WriteLine($"Successfully canceled reservation for Movie: {reservedMovie.Title}, Show Date: {reservedShow.Date}");
                    Thread.Sleep(2000);
                    reservationChoice = true;

                    bool menuChoice = false;
                    while (!menuChoice)
                    {
                        Console.WriteLine("\nChoose an option:");
                        Console.WriteLine("[1] Cancel a reservation");
                        Console.WriteLine("[2] Go back to the user menu");
                        string userInput2 = Console.ReadLine();

                        if (userInput2 == "1")
                        {
                            CancelReservation(userReservations, acc);
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
                else if (confirmationInput == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Reservation Cancellation stopped.");
                    reservationChoice = true;

                    bool menuChoice = false;
                    while (!menuChoice)
                    {
                        Console.WriteLine("\nChoose an option:");
                        Console.WriteLine("[1] Cancel a reservation");
                        Console.WriteLine("[2] Go back to the user menu");
                        string userInput2 = Console.ReadLine();

                        if (userInput2 == "1")
                        {
                            CancelReservation(userReservations, acc);
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
                else
                {
                    Console.WriteLine("Invalid option. Please choose [1] Yes or [2] No.");
                    // Thread.Sleep(2000);

                    // /*
                    // The cursor goes back to the position of the option message, which is replaced after 2 seconds with an empty string.
                    // */
                    // Console.SetCursorPosition(0, Console.CursorTop - 1);
                    // Console.Write(new string(' ', Console.WindowWidth));
                    // Console.SetCursorPosition(0, Console.CursorTop - 1);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Thread.Sleep(2000);

                /*
                The cursor goes back to the position of the option message, which is replaced after 2 seconds with an empty string.
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