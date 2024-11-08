class Bar
{

    static public void Main()
    {
        Start();
    }
    static public void Start()
    {
        Console.WriteLine("[1] Overview of all reservations");
        Console.WriteLine("[2] Status");
        Console.WriteLine("[3] Overview of all users");

        Console.WriteLine("What would you like to do?");
        int choice = Convert.ToInt32(Console.ReadLine());
        
        switch (choice)
        {
            case 1:
                AllReservationsPrint();
                break;
            case 2:
                StatusPrint();
                break;
            case 3:
                AllUsersPrint();
                break;
            default:
                Console.WriteLine("No valid option selected. Please try again.");
                Start();
                break;
        }
        
    }

    static public void AllReservationsPrint()
    {
        List<ReservationModel> reservations = ReservationLogic.GetBarReservations();

        if (reservations.Count > 0)
        {
            Console.WriteLine("Bar Reservations Overview by Time Slot:");
            Console.WriteLine("-----------------------------------------------");
        }
        else
        {
            Console.WriteLine("No bar reservations found.");
            return;
        }

        var reservationsByTime = new Dictionary<DateTime, List<ReservationModel>>();

        foreach (ReservationModel reservation in reservations)
        {
            ShowModel show = ShowLogic.GetByID(reservation.MovieId);
            MoviesModel movie = MoviesLogic.GetById((int)show.MovieId);

            DateTime movieBeginTime = DateTime.Parse(show.Date);
            DateTime barBeginReservationTime = movieBeginTime.AddMinutes(movie.TimeInMinutes);

            if (!reservationsByTime.ContainsKey(barBeginReservationTime))
            {
                reservationsByTime[barBeginReservationTime] = new List<ReservationModel>();
            }

            reservationsByTime[barBeginReservationTime].Add(reservation);
        }

        // Sort the keys (time slots) in ascending order for a time-based overview
        foreach (var timeSlot in reservationsByTime.Keys.OrderBy(time => time))
        {
            Console.WriteLine($"Time Slot: {timeSlot}");
            Console.WriteLine("-----------------------------------------------");

            foreach (var reservation in reservationsByTime[timeSlot])
            {
                Console.WriteLine($"ID: {reservation.Id}");
                Console.WriteLine($"SeatsID: {reservation.SeatsId}");
                Console.WriteLine($"UserID: {reservation.UserId}");
                Console.WriteLine($"MovieID: {reservation.MovieId}");
                Console.WriteLine("-----------------------------------------------");
            }
        }
    }
    static public void StatusPrint()
    {
        List<ReservationModel> reservations = ReservationLogic.GetBarReservations();
        
        // Get current time
        DateTime currentTime = DateTime.Now;

        // Calculate the current number of people using the bar based on reservations
        int currentNumOfBarPeople = 0;

        foreach (ReservationModel reservation in reservations)
        {
            // Calculate the bar reservation time based on the movie end time
            ShowModel show = ShowLogic.GetByID(reservation.MovieId);
            MoviesModel movie = MoviesLogic.GetById((int)show.MovieId);

            DateTime movieBeginTime = DateTime.Parse(show.Date);
            DateTime barReservationTimeStart = movieBeginTime.AddMinutes(movie.TimeInMinutes);
            DateTime barReservationTimeEnd = barReservationTimeStart.AddHours(2); // Each bar reservation lasts for 2 hours after the movie

            // Check if the current time is within the reservation window
            if (currentTime >= barReservationTimeStart && currentTime <= barReservationTimeEnd)
            {
                currentNumOfBarPeople++;
            }
        }

        // Determine crowd level based on the number of people currently in the bar
        string crowdLevel = currentNumOfBarPeople switch
        {
            < 15 => "quiet",
            < 25 => "moderately busy",
            < 35 => "busy",
            _ => "crowded"
        };

        // Print the status of the bar
        Console.WriteLine("At the bar:");
        Console.WriteLine($"It is currently {crowdLevel}");
        Console.WriteLine($"{currentNumOfBarPeople} out of a maximum of 40 people are present");
    }

    static public void AllUsersPrint()
    {
        List<ReservationModel> reservations = ReservationLogic.GetBarReservations();

        if (reservations.Count > 0)
        {
            Console.WriteLine("-----------------------------------------------");
        }
        else
        {
            Console.WriteLine("No users are currently at the bar.");
            return;
        }

        UserLogic getUsers = new UserLogic();
        // Dictionary to group users by the bar reservation time slot
        var usersByTime = new Dictionary<DateTime, List<UserModel>>();

        foreach (ReservationModel reservation in reservations)
        {
            ShowModel show = ShowLogic.GetByID(reservation.MovieId);
            MoviesModel movie = MoviesLogic.GetById((int)show.MovieId);

            DateTime movieBeginTime = DateTime.Parse(show.Date);
            DateTime barReservationTimeStart = movieBeginTime.AddMinutes(movie.TimeInMinutes);

            if (!usersByTime.ContainsKey(barReservationTimeStart))
            {
                usersByTime[barReservationTimeStart] = new List<UserModel>();
            }

            UserModel user = getUsers.GetById(reservation.UserId);
            usersByTime[barReservationTimeStart].Add(user);
        }

        // Sort the time slots in ascending order and print user info by each time slot
        foreach (var timeSlot in usersByTime.Keys.OrderBy(time => time))
        {
            Console.WriteLine($"Time Slot: {timeSlot}");
            Console.WriteLine("-----------------------------------------------");

            foreach (var user in usersByTime[timeSlot])
            {
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"E-mail: {user.Email}");
                Console.WriteLine($"First name: {user.FirstName}");
                Console.WriteLine($"Last name: {user.LastName}");
                Console.WriteLine($"Phone number: {user.Phone_Number}");
                Console.WriteLine("-----------------------------------------------");
            }
        }
    }
}