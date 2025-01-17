using System.Text.RegularExpressions;

class Admin
{
    public static void Start(UserModel currentUser)
    {
        Console.Clear();
        Console.WriteLine("===== Admin Page =====");
        Console.WriteLine($"Welcome {currentUser.FirstName} {currentUser.LastName}\n");
        while (true)
        {
            Console.WriteLine("[1]Movie menu");
            Console.WriteLine("[2]Show menu");
            Console.WriteLine("[3]Bar menu");
            Console.WriteLine("[4]Show the current balance");
            Console.WriteLine("[5]Voucher menu");
            Console.WriteLine("[6]Snack menu");
            Console.WriteLine("[7]Location menu");
            Console.WriteLine("[8]Logout");
            bool isNum = int.TryParse(Console.ReadLine(), out int input);
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number");
                Thread.Sleep(2000);
                Console.Clear();
            }
            else if (input == 1)
            {
                Movie.Start(currentUser);
                //Console.Clear();
            }
            else if (input == 2)
            {
                Show.AdminStart(currentUser);
                //Console.Clear();
            }
            else if (input == 3)
            {
                Bar.Start(currentUser);
            }
            else if (input == 4)
            {
                TrackMoney();
                Thread.Sleep(2000);
            }
            else if (input == 5)
            {
                Voucher.AdminStart(currentUser);
            }
            else if (input == 6)
            {
                SnackMenu.AdminSnackMenu(currentUser);
            }
            else if (input == 7)
            {
                Location.Start(currentUser);
            }
            else if (input == 8)
            {
                Console.Clear();
                Menu.Start();
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. This option doesn't exist");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }
    }

    static void TrackMoney()
    {
        Console.Clear();

        // This dictionary will hold the shows with the total income
        var incomePerShow = new Dictionary<string, decimal>();

        DateTime currentDate = DateTime.Now;
        DateTime newDate = currentDate.AddDays(-7);

        // All the reservations are being retrieved from the database
        List<ReservationModel> allReservations = ReservationAccess.GetAllReservations();
        // The reservations with the same show id will be placed in a group 
        var lastWeekReservations = allReservations
            .GroupBy(r => r.ShowId)
            .ToList();

        foreach (var group in lastWeekReservations)
        {
            List<int> lastWeekShows = [];
            string dateFormat = "yyyy-MM-dd HH:mm";

            ShowModel show = ShowAccess.GetByID(group.Key);

            // Show date is a string so it has to be converted to a datetime object
            DateTime showDate;
            if (DateTime.TryParse(show.Date, out showDate) && showDate >= newDate)
            {
                decimal totalIncome = 0;

                foreach (var reservation in group)
                {
                    SeatsModel seat = SeatsAccess.GetById(reservation.SeatsId);
                    if (seat != null)
                    {
                        totalIncome += seat.Price;
                    }
                }

                MoviesModel movie = MoviesAccess.GetByLongId(show.MovieId);
                string movieTitle = "";
                if (movie != null)
                {
                    // Movie title is the key
                    movieTitle = movie.Title;
                }
                // Total income is the value
                incomePerShow[movieTitle] = totalIncome;
            }
        }

        Console.WriteLine("The shows with the most income from the last week:\n");

        // The values in the dictionary will be printed from high to low
        var sorted = incomePerShow.OrderByDescending(show => show.Value);
        foreach (var show in sorted)
        {
            Console.WriteLine($"{show.Key} has a total of €{show.Value}");
        }
        Console.WriteLine("\n");
    }
}