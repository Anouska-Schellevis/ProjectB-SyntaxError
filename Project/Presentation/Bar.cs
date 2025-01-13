class Bar
{

    static public void Main(UserModel acc)
    {
        Start(acc);
    }
    static public void Start(UserModel acc)
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine("[1]Overview of all reservations");
            Console.WriteLine("[2]Status");
            Console.WriteLine("[3]Overview of all users");
            Console.WriteLine("[4]Go back to the menu");

            Console.WriteLine("What would you like to do?");

            bool isNum = int.TryParse(Console.ReadLine(), out int choice);
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number");
                Thread.Sleep(2000);
                Start(acc);
            }
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    AllReservationsPrint();
                
                    Console.WriteLine("\n[1]Go back to bar menu");
                    Console.WriteLine("[2]Exit to admin menu");

                    while (true)
                    {
                        string menuChoice = Console.ReadLine();
                        if (menuChoice == "1")
                        {
                            Console.Clear();
                            Start(acc);
                            return;
                        }
                        else if (menuChoice == "2")
                        {
                            Console.Clear();
                            Admin.Start(acc);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter 1 or 2.");
                            Thread.Sleep(2000);

                            /*
                            The cursor goes back to the position of the error message, which is replaced after 2 seconds with an empty string.
                            */
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.SetCursorPosition(0, Console.CursorTop + 1);
                        }
                    }
                case 2:
                    Console.Clear();
                    StatusPrint();
                                
                    Console.WriteLine("\n[1]Go back to bar menu");
                    Console.WriteLine("[2]Exit to admin menu");

                    while (true)
                    {
                        string menuChoice = Console.ReadLine();
                        if (menuChoice == "1")
                        {
                            Console.Clear();
                            Start(acc);
                            return;
                        }
                        else if (menuChoice == "2")
                        {
                            Console.Clear();
                            Admin.Start(acc);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter 1 or 2.");
                            Thread.Sleep(2000);

                            /*
                            The cursor goes back to the position of the error message, which is replaced after 2 seconds with an empty string.
                            */
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.SetCursorPosition(0, Console.CursorTop + 1);
                        }
                    }
                case 3:
                    Console.Clear();
                    AllUsersPrint();
                
                    Console.WriteLine("\n[1]Go back to bar menu");
                    Console.WriteLine("[2]Exit to admin menu");

                    while (true)
                    {
                        string menuChoice = Console.ReadLine();
                        if (menuChoice == "1")
                        {
                            Console.Clear();
                            Start(acc);
                            return;
                        }
                        else if (menuChoice == "2")
                        {
                            Console.Clear();
                            Admin.Start(acc);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter 1 or 2.");
                            Thread.Sleep(2000);

                            /*
                            The cursor goes back to the position of the error message, which is replaced after 2 seconds with an empty string.
                            */
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.SetCursorPosition(0, Console.CursorTop + 1);
                        }
                    }
                case 4:
                    Console.Clear();
                    Admin.Start(acc);
                    break;
                default:
                    Console.WriteLine("Invalid input. This option doesn't exist");
                    Thread.Sleep(2000);
                    Start(acc);
                    break;
            }
        }
    }

    static public void AllReservationsPrint()
    {
        var reservationsByTime = MakeReservationBarDict();

        if (reservationsByTime.Count > 0)
        {
            Console.WriteLine("-----------------------------------------------");
        }
        else
        {
            Console.WriteLine("No users are currently at the bar.");
            return;
        }

        foreach (var timeSlot in reservationsByTime.Keys.OrderBy(time => time))
        {
            Console.WriteLine($"Time Slot: {timeSlot}");
            Console.WriteLine("-----------------------------------------------");

            var groupedReservations = reservationsByTime[timeSlot]
                .GroupBy(reservation => reservation.UserId)
                .ToList();

            foreach (var group in groupedReservations)
            {
                var exampleReservation = group.First();

                Console.WriteLine($"UserID: {exampleReservation.UserId}");
                Console.WriteLine($"ShowID: {exampleReservation.ShowId}");
                Console.WriteLine("Reservations:");

                foreach (var reservation in group)
                {
                    Console.WriteLine($"- Reservation ID: {reservation.Id,3}, Seats ID: {reservation.SeatsId,3}");
                }

                Console.WriteLine("-----------------------------------------------");
            }
        }
    }

    static public void StatusPrint()
    {
        List<ReservationModel> reservations = ReservationLogic.GetBarReservations();

        DateTime currentTime = DateTime.Now;

        const int numberOfSeats = 40;
        int currentNumOfBarPeople = 0;

        foreach (ReservationModel reservation in reservations)
        {
            ShowModel show = ShowLogic.GetByID(reservation.ShowId);
            MoviesModel movie = MoviesLogic.GetById((int)show.MovieId);

            DateTime movieBeginTime = DateTime.Parse(show.Date);
            DateTime barReservationTimeStart = movieBeginTime.AddMinutes(movie.TimeInMinutes);
            DateTime barReservationTimeEnd = barReservationTimeStart.AddHours(2);

            if (currentTime >= barReservationTimeStart && currentTime <= barReservationTimeEnd)
            {
                currentNumOfBarPeople++;
            }
        }

        string crowdLevel = currentNumOfBarPeople switch
        {
            < 15 => "quiet",
            < 25 => "moderately busy",
            < 35 => "busy",
            _ => "crowded"
        };

        Console.WriteLine("At the bar:");
        Console.WriteLine($"It is currently {crowdLevel}");
        Console.WriteLine($"{numberOfSeats - currentNumOfBarPeople} out of {numberOfSeats} seats are currently available");
    }

    static public void AllUsersPrint()
    {

        var usersByTime = MakeUserBarDict();

        if (usersByTime.Count > 0)
        {
            Console.WriteLine("-----------------------------------------------");
        }
        else
        {
            Console.WriteLine("No bar reservations found.");
            return;
        }

        foreach (var timeSlot in usersByTime.Keys.OrderBy(time => time))
        {
            Console.WriteLine($"Time Slot: {timeSlot}");
            Console.WriteLine("-----------------------------------------------");

            var groupedUsers = usersByTime[timeSlot]
                        .GroupBy(user => new
                        {
                            user.Email,
                            user.FirstName,
                            user.LastName,
                            user.Phone_Number
                        })
                        .ToList();

            foreach (var group in groupedUsers)
            {
                var user = group.First();
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"E-mail: {user.Email}");
                Console.WriteLine($"First name: {user.FirstName}");
                Console.WriteLine($"Last name: {user.LastName}");
                Console.WriteLine($"Phone number: {user.Phone_Number}");
                Console.WriteLine($"Group size: {group.Count()}");
                Console.WriteLine("-----------------------------------------------");
            }
        }
    }

    private static Dictionary<DateTime, List<ReservationModel>> MakeReservationBarDict()
    {
        return MakeBarDict(reservation => reservation);
    }

    private static Dictionary<DateTime, List<UserModel>> MakeUserBarDict()
    {
        UserLogic userLogic = new UserLogic();
        return MakeBarDict(reservation => userLogic.GetById(reservation.UserId));
    }

    private static Dictionary<DateTime, List<T>> MakeBarDict<T>(Func<ReservationModel, T> selector)
    {
        List<ReservationModel> reservations = ReservationLogic.GetBarReservations();

        var dataByTime = new Dictionary<DateTime, List<T>>();

        foreach (ReservationModel reservation in reservations)
        {
            ShowModel show = ShowLogic.GetByID(reservation.ShowId);
            MoviesModel movie = MoviesLogic.GetById((int)show.MovieId);

            DateTime movieBeginTime = DateTime.Parse(show.Date);
            DateTime barReservationTimeStart = movieBeginTime.AddMinutes(movie.TimeInMinutes);

            if (!dataByTime.ContainsKey(barReservationTimeStart))
            {
                dataByTime[barReservationTimeStart] = new List<T>();
            }

            dataByTime[barReservationTimeStart].Add(selector(reservation));
        }

        return dataByTime;
    }

}