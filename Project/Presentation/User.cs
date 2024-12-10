class User
{
    public static void Start(UserModel acc)
    {
        Console.Clear();
        Console.WriteLine("===== User Page =====");

        List<ReservationModel> reservationsByUser = ReservationLogic.GetReservationsByUserId(acc.Id);

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

        foreach (ReservationModel reservation in mergedReservationsByUser)
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
        bool exitMenu = false;
        do
        {
            //Console.Clear();
            //Console.WriteLine("User page\n");
            Console.WriteLine("Welcome back " + acc.FirstName + " " + acc.LastName);
            Console.WriteLine("[1] See week overview of available movies");
            Console.WriteLine("[2] See your reservations");
            Console.WriteLine("[3] See your vouchers");
            Console.WriteLine("[4] Go back to login screen");
            string user_answer = Console.ReadLine().ToLower();

            if (user_answer == "1")
            {
                Show.UserStart(acc);
                exitMenu = true;
            }
            else if (user_answer == "2")
            {
                Reservation.SeeReservation(acc);
                exitMenu = true;
            }
            else if (user_answer == "3")
            {
                Voucher.UserStart(acc);
                exitMenu = true;
            }
            else if (user_answer == "4")
            {
                Console.WriteLine("Logging out");
                return;
            }
            else
            {
                Console.WriteLine("Invalid option, please try again.");
            }
        } while (!exitMenu);

        // Console.WriteLine("Would you like to see the overview of available movies Y/N");
        // string answer = Console.ReadLine().ToLower();

        // if (answer == "y")
        // {
        //     //Console.WriteLine($"voordat hij naar show user start gaat {acc.FirstName}");
        //     Show.UserStart(acc);
        // }
        // else
        // {
        //     Console.WriteLine("back to the menu...");
        //     Menu.Start();
        // }
    }
}
