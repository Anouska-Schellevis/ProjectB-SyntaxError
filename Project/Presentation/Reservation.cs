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
            .ToList(); //group all user reservation by show id, show id is group KEY

        Console.WriteLine("Your reservations:");
        int reservationNumber = 1; //number to incriment for amount of reservations

        foreach (var group in groupedReservations)
        {
            ShowModel reservedShow = ShowAccess.GetByID(group.Key); //get the show model with the group key aka the show id that groups that group
            MoviesModel reservedMovie = MoviesAccess.GetByLongId(reservedShow.MovieId); // get the movie model from that show for the name 

            if (reservedMovie != null)
            {
                Console.WriteLine($"Reservation [{reservationNumber}]");
                Console.WriteLine($"    Movie: {reservedMovie.Title}, Show Date: {reservedShow.Date}, Bar reservation: {(group.First().Bar ? "Yes" : "No")}");


                foreach (var reservation in group)
                {
                    SeatsModel seat = SeatsAccess.GetById((int)reservation.SeatsId);
                    //get the seats model for each reservation

                    if (seat != null)
                    {
                        //print each seats row and collum aka chair
                        Console.WriteLine($"    Seat Row: {seat.RowNumber}, Chair: {seat.ColumnNumber}");
                    }
                    else
                    {
                        Console.WriteLine("    Seat information not available.");
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
            }
        }
    }

    public static void CancelReservation(List<ReservationModel> userReservations, UserModel acc)
    {
        //this gets called with that flat reservations from see reservations, which is one list of all that users reservations
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
                    continue;
                }

                var selectedReservation = userReservations[userInput - 1];

                if (selectedReservation == null)
                {
                    Console.WriteLine("Reservation doesn't exist.");
                    return;
                }

                // Get the show and movie information
                ShowModel reservedShow = ShowAccess.GetByID((int)selectedReservation.ShowId);
                MoviesModel reservedMovie = MoviesAccess.GetByLongId(reservedShow.MovieId);

                Console.WriteLine($"Cancelling reservation for Movie: {reservedMovie.Title}, Show Date: {reservedShow.Date}");

                //because earlier we had to make user reservations one list, destroying the grouping on show id, we group these reservations on show id again,
                // to group all reservations for each seperate show
                var groupedReservations = userReservations
                    .Where(r => r.ShowId == selectedReservation.ShowId)
                    .ToList();

                var reservedSeatIds = groupedReservations.Select(r => r.SeatsId).ToList(); //make a list of all seats of that show

                if (reservedSeatIds.Count == 0)
                {
                    Console.WriteLine($"No reserved seats found for {reservedMovie.Title}, Show Date: {reservedShow.Date}");
                    return;
                }

                foreach (var seatId in reservedSeatIds)
                {
                    //for each seat in that list remove that
                    int seatIdInt = (int)seatId;
                    SeatsAccess.Delete(seatIdInt);
                    //Console.WriteLine($"Deleted seat with ID: {seatIdInt}");
                }

                foreach (var reservation in groupedReservations)
                {
                    ReservationAccess.Delete((int)reservation.Id);
                }

                Console.WriteLine($"Successfully canceled reservation for Movie: {reservedMovie.Title}, Show Date: {reservedShow.Date}");
                reservationChoice = true;
                bool menuChoice = false;
                while (!menuChoice)
                {
                    Console.WriteLine("\nChoose an option:");
                    Console.WriteLine("[1] Return to menu");
                    Console.WriteLine("[2] See your reservations");
                    string userInput2 = Console.ReadLine();

                    if (userInput2 == "1")
                    {
                        User.Start(acc);
                        menuChoice = true;
                    }
                    else if (userInput2 == "2")
                    {
                        SeeReservation(acc);
                        menuChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option, please try again.");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}
