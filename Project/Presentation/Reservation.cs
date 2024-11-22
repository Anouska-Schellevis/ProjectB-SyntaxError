public class Reservation
{
    public static void SeeReservation(UserModel acc)
    {

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
                Console.WriteLine($"Reservation [{reservationNumber}]");
                Console.WriteLine($"    Movie: {reservedMovie.Title}, Show Date: {reservedShow.Date}, Bar reservation: {(group.First().Bar ? "Yes" : "No")}");


                foreach (var reservation in group)
                {
                    SeatsModel seat = SeatsAccess.GetById((int)reservation.SeatsId);
                    //Console.WriteLine($"Trying to fetch seat for SeatId: {reservation.SeatsId}");

                    if (seat != null)
                    {
                        //Console.WriteLine($"    Seat: ");
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
                CancelReservation(flatReservations);
                menuChoice = true;
            }
            else if (userInput == "2")
            {
                UserLogin.Start();
                menuChoice = true;
            }
            else
            {
                Console.WriteLine("Invalid option, please try again.");
            }
        }
    }

    public static void CancelReservation(List<ReservationModel> userReservations)
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
                    continue;
                }

                var selectedReservation = userReservations[userInput - 1];

                if (selectedReservation == null)
                {
                    Console.WriteLine("Reservation is null. Cannot cancel.");
                    return;
                }

                Console.WriteLine($"Cancelling reservation for Show Reservation: {selectedReservation.ShowId}");

                var groupedReservations = userReservations
                    .Where(r => r.ShowId == selectedReservation.ShowId)
                    .ToList();

                var reservedSeatIds = groupedReservations.Select(r => r.SeatsId).ToList();

                if (reservedSeatIds.Count == 0)
                {
                    Console.WriteLine($"No reserved seats found for show {selectedReservation.ShowId}.");
                    return;
                }

                Console.WriteLine($"Reserved seats for Show Reservation {selectedReservation.ShowId}: {string.Join(", ", reservedSeatIds)}");

                foreach (var seatId in reservedSeatIds)
                {
                    int seatIdInt = (int)seatId;
                    SeatsAccess.Delete(seatIdInt);
                    Console.WriteLine($"Deleted seat with ID: {seatIdInt}");
                }

                foreach (var reservation in groupedReservations)
                {
                    ReservationAccess.Delete((int)reservation.Id);
                    Console.WriteLine($"Deleted reservation with ID: {reservation.Id}");
                }

                Console.WriteLine($"Successfully canceled reservations and deleted associated seats for ShowId: {selectedReservation.ShowId}");
                reservationChoice = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}
