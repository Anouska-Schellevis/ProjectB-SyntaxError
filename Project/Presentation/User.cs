class User
{
    public static void Start(UserModel currentUser)
    {
        Console.Clear();
        Console.WriteLine("User page\n");
        Console.WriteLine($"Welcome back {currentUser.FirstName} {currentUser.LastName}");

        List<ReservationModel> reservationsByUser = ReservationLogic.GetReservationsByUserId(currentUser.Id);
        
        // Merging reservations by ShowId to ensure that each show counts only once for a user,
        // regardless of how many friends joined. This prevents duplicate warnings messages.
        List<ReservationModel> mergedReservationsByUser = reservationsByUser
        .GroupBy(b => b.ShowId)
        .Select(group => new ReservationModel
        {
            ShowId = group.Key,
            Id = group.First().Id,
            Bar = group.First().Bar,  
            UserId = group.First().UserId,
            SeatsId = group.First().SeatsId
        })
        .ToList();

        DateTime currentDateTime = DateTime.Now;
        
        foreach(ReservationModel reservation in mergedReservationsByUser)
        {
            ShowModel show = ShowLogic.GetByID(reservation.ShowId);
            MoviesModel movie = MoviesLogic.GetById((int)show.MovieId);

            DateTime movieBeginTime = DateTime.Parse(show.Date);
            DateTime barReservationTimeStart = movieBeginTime.AddMinutes(movie.TimeInMinutes);
            DateTime barReservationTimeEnd = barReservationTimeStart.AddHours(2);

            if (barReservationTimeEnd < currentDateTime && currentDateTime < barReservationTimeEnd.AddMinutes(15)) // warning until 15 minutes after the end of the bar reservation
            {
                Console.WriteLine("Your bar reservation time is up. We kindly ask you to leave the bar");
            }

        }


        Console.WriteLine("Would you like to see the overview of available movies Y/N");
        string answer = Console.ReadLine().ToLower();

        if (answer == "y")
        {
            Show.UserStart();
        }
        else
        {
            Console.WriteLine("back to the menu...");
            Menu.Start();
        }
    }
}
