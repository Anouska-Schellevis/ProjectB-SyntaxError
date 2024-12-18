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
            Console.WriteLine("[1]Show the menu of the movies");
            Console.WriteLine("[2]Show the menu of the shows");
            Console.WriteLine("[3]Show the menu of the bar");
            Console.WriteLine("[4]Show the current balance");
            Console.WriteLine("[5]Show the menu of the vouchers");
            Console.WriteLine("[6]Show the movie snack menu");
            Console.WriteLine("[7]Logout");
            int input = Convert.ToInt16(Console.ReadLine());

            if (input == 1)
            {
                Console.Clear();
                Movie.Start();
                //Console.Clear();
            }
            else if (input == 2)
            {
                Console.Clear();
                Show.AdminStart();
                //Console.Clear();
            }
            else if (input == 3)
            {
                Console.Clear();
                Bar.Start();
            }
            else if (input == 4)
            {
                Console.Clear();
                TrackMoney();
            }
            else if (input == 5)
            {
                Console.Clear();
                Voucher.AdminStart(currentUser);
            }
            else if (input == 6)
            {
                SnackMenu.AdminSnackMenu(currentUser);
            }
            else if (input == 7)
            {
                Console.Clear();
                break;
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