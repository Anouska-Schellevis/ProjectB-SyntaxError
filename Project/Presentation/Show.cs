class Show
{
    // static public void Main()
    // {
    //     // bool admin = false;
    //     // if (admin)
    //     // { AdminStart(); }
    //     // else
    //     // { UserStart(); }
    // }
    public static int day;
    static public void AdminStart(UserModel acc)
    {
        Console.Clear();
        Console.WriteLine("[1] Overview of all show");
        Console.WriteLine("[2] Add Show");
        Console.WriteLine("[3] Edit Show");
        Console.WriteLine("[4] Delete Show");
        Console.WriteLine("[5] Go back to admin menu");
        Console.WriteLine("What would you like to do?");
        int choice = Convert.ToInt32(Console.ReadLine());
        int idToDelete;

        switch (choice)
        {
            case 1:
                ShowPrint();
                break;
            case 2:
                ShowAdd();
                Console.WriteLine("Show is added");
                break;
            case 3:
                Console.WriteLine("Enter the id to edit");
                int ID_to_edit = Convert.ToInt32(Console.ReadLine());
                ShowModel show = ShowLogic.GetByID(ID_to_edit);
                if (show != null)
                {
                    show = ShowEdit(show);
                    ShowLogic.UpdateShow(show);
                    Console.WriteLine("Show is saved!");
                }
                else
                {
                    Console.WriteLine("Such a show does not exist");
                }
                break;
            case 4:
                do
                {
                    Console.WriteLine("Enter the id of the show you want to delete");
                    idToDelete = Convert.ToInt32(Console.ReadLine());
                    if (ShowAccess.GetByID(idToDelete) == null)
                    {
                        Console.WriteLine("This ID does not exist. Try again");
                    }
                } while (ShowAccess.GetByID(idToDelete) == null);
                ShowDelete(idToDelete);
                Console.WriteLine("Show is deleted");
                break;
            case 5:
                Console.Clear();
                Admin.Start(acc);
                break;
            default:
                Console.WriteLine("No valid option selected. Please try again.");
                AdminStart(acc);
                break;
        }
    }

    static public void UserStart(UserModel acc)
    {
        string movie = "";
        int choice = 0;
        int chosennumber = 0;
        WeekOverviewMovies();
        Dictionary<int, string> movies;
        bool loop = true;
        bool printed = true;
        movies = PrintOverviewMovie_Time();
        while (loop)
        {
            if (printed == false)
            {
                movies = PrintOverviewMovie_Time();
            }
            if (movies.Count != 0)
            {
                Console.WriteLine("What movie would you like to watch?");
                foreach (var movie_count in movies)
                {
                    string movie_name = movie_count.Value;
                    Console.WriteLine($"{movie_count.Key}. {movie_name}");
                }
                do
                {
                    chosennumber = Convert.ToInt32(Console.ReadLine());
                    if (movies.ContainsKey(chosennumber))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not a valid choice. Try again.");
                    }
                } while (true);
                Console.Clear();
                string chosenmovie = movies[chosennumber];
                movie = Movie.MovieSearch(chosenmovie);
                Console.WriteLine("Whould you like to:");
                Console.WriteLine("1. Get movie info");
                Console.WriteLine("2. Choose time");
                Console.WriteLine("3. Go back to week overview");
                Console.WriteLine("4. Go back to day overview");
                Console.WriteLine("5. Go back to UserMenu");
                do
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1 || choice == 2 || choice == 3 || choice == 5)
                    {
                        loop = false;
                    }
                    else if (choice == 4)
                    {
                        Console.WriteLine("Returning to day overview");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                } while (choice < 1 || choice > 5);
                if (choice == 4)
                {
                    printed = false;
                    continue;
                }
                Console.Clear();
            }
            else

            {
                Console.WriteLine("There is no movie on this day");
                printed = false;
            }
        }
        switch (choice)
        {
            case 1:
                Console.WriteLine(movie);
                Console.WriteLine("\n\nWhould you like to:");
                Console.WriteLine("1. Choose time");
                Console.WriteLine("2. Go back to week overview");
                int secondchoice;
                do
                {
                    secondchoice = Convert.ToInt32(Console.ReadLine());
                    if (secondchoice != 1 && secondchoice != 2)
                    {
                        Console.WriteLine("Invalid input try again");
                    }
                } while (secondchoice != 1 && secondchoice != 2);
                Console.Clear();
                switch (secondchoice)
                {
                    case 1:
                        if (movie != null)
                        {
                            Dictionary<int, ShowModel> showtime = TimeOptions(movies[chosennumber], day);
                            Console.WriteLine("And at what time?");
                            foreach (var datetime in showtime)
                            {
                                string time = datetime.Value.Date.Split(' ')[1];
                                Console.WriteLine($"{datetime.Key}. {time}");
                            }
                            int chosentime;
                            do
                            {
                                chosentime = Convert.ToInt32(Console.ReadLine());
                                if (showtime.ContainsKey(chosentime))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Not a valid choice. Try again.");
                                }
                            }

                            while (true);
                            ShowModel ChosenShow = showtime[chosentime];
                            if (ChosenShow.TheatreId == 1)
                            // {
                            //     Theater150 theater = new Theater150();
                            //     theater.SelectSeats(ChosenShow.Id);
                            // }
                            // if (ChosenShow.TheatreId == 2)
                            // {
                            //     Theater300 theater2 = new Theater300();
                            //     theater2.SelectSeats(ChosenShow.Id);
                            // }
                            // if (ChosenShow.TheatreId == 3)
                            // {
                            //     Theater500 theater3 = new Theater500();
                            //     theater3.SelectSeats(ChosenShow.Id);
                            // }
                            {
                                ConcreteTheater theater150 = (ConcreteTheater)Theater.GetTheater(150);
                                if (theater150 != null)
                                {
                                    //Console.WriteLine($"net voor dat hij naar select seats gaat {acc.FirstName}");
                                    theater150.SelectSeats(ChosenShow.Id, acc);
                                }
                                else
                                {
                                    Console.WriteLine("Error: Unable to retrieve Theater 150.");
                                }
                            }
                            if (ChosenShow.TheatreId == 2)
                            {
                                ConcreteTheater theater300 = (ConcreteTheater)Theater.GetTheater(300);
                                if (theater300 != null)
                                {
                                    theater300.SelectSeats(ChosenShow.Id, acc);
                                }
                                else
                                {
                                    Console.WriteLine("Error: Unable to retrieve Theater 300.");
                                }
                            }
                            if (ChosenShow.TheatreId == 3)
                            {
                                ConcreteTheater theater500 = (ConcreteTheater)Theater.GetTheater(500);
                                if (theater500 != null)
                                {
                                    theater500.SelectSeats(ChosenShow.Id, acc);
                                }
                                else
                                {
                                    Console.WriteLine("Error: Unable to retrieve Theater 500.");
                                }
                            }
                        }
                        break;
                    case 2:
                        UserStart(acc);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                if (movie != null)
                {
                    Dictionary<int, ShowModel> showtime = TimeOptions(movies[chosennumber], day);
                    Console.WriteLine("And at what time?");
                    foreach (var datetime in showtime)
                    {
                        string time = datetime.Value.Date.Split(' ')[1];
                        Console.WriteLine($"{datetime.Key}. {time}");
                    }
                    int chosentime;
                    do
                    {
                        chosentime = Convert.ToInt32(Console.ReadLine());
                        if (showtime.ContainsKey(chosentime))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Not a valid choice. Try again.");
                        }
                    }

                    while (true);
                    ShowModel ChosenShow = showtime[chosentime];
                    if (ChosenShow.TheatreId == 1)
                    // {
                    //     Theater150 theater = new Theater150();
                    //     theater.SelectSeats(ChosenShow.Id);
                    // }
                    // if (ChosenShow.TheatreId == 2)
                    // {
                    //     Theater300 theater2 = new Theater300();
                    //     theater2.SelectSeats(ChosenShow.Id);
                    // }
                    // if (ChosenShow.TheatreId == 3)
                    // {
                    //     Theater500 theater3 = new Theater500();
                    //     theater3.SelectSeats(ChosenShow.Id);
                    // }
                    {
                        ConcreteTheater theater150 = (ConcreteTheater)Theater.GetTheater(150);
                        if (theater150 != null)
                        {
                            //Console.WriteLine($"net voor dat hij naar select seats gaat {acc.FirstName}");
                            theater150.SelectSeats(ChosenShow.Id, acc);
                        }
                        else
                        {
                            Console.WriteLine("Error: Unable to retrieve Theater 150.");
                        }
                    }
                    if (ChosenShow.TheatreId == 2)
                    {
                        ConcreteTheater theater300 = (ConcreteTheater)Theater.GetTheater(300);
                        if (theater300 != null)
                        {
                            theater300.SelectSeats(ChosenShow.Id, acc);
                        }
                        else
                        {
                            Console.WriteLine("Error: Unable to retrieve Theater 300.");
                        }
                    }
                    if (ChosenShow.TheatreId == 3)
                    {
                        ConcreteTheater theater500 = (ConcreteTheater)Theater.GetTheater(500);
                        if (theater500 != null)
                        {
                            theater500.SelectSeats(ChosenShow.Id, acc);
                        }
                        else
                        {
                            Console.WriteLine("Error: Unable to retrieve Theater 500.");
                        }
                    }
                }

                break;
            case 3:
                UserStart(acc);
                break;
            case 5:
                User.Start(acc);
                break;
            default:
                Console.WriteLine("Unexpected choice");
                break;

        }
    }

    static public ShowModel ShowEdit(ShowModel show)
    {
        int newMovieId = 0;
        string newDate_time;
        string Date = "";
        bool timecheck = false;
        string time = "";
        int newTheatreId = 0;
        Console.WriteLine("Would you like to change the theater ID?");
        Console.WriteLine("1. yes\n2. no");
        int question1 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        while (1 != 1 && question1 != 2)
        {
            Console.WriteLine("Invalid input. Try again");
            Console.WriteLine("Would you like to change the theater ID?");
            Console.WriteLine("1. yes\n2. no");
            question1 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
        }
        if (question1 == 1)
        {
            do
            {
                Console.WriteLine("Enter new theater ID for this movie.");
                newTheatreId = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                if (newTheatreId != 1 && newTheatreId != 2 && newTheatreId != 3)
                {
                    Console.WriteLine("Theater ID does not exist. Try again.");
                }
            } while (newTheatreId != 1 && newTheatreId != 2 && newTheatreId != 3);
        }
        Console.WriteLine("Would you like to change the movie ID?");
        Console.WriteLine("1. yes\n2. no");
        int question2 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        while (question2 != 1 && question2 != 2)
        {
            Console.WriteLine("Invalid input. Try again");
            Console.WriteLine("Would you like to change the theater ID?");
            Console.WriteLine("1. yes\n2. no");
            question2 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
        }
        if (question2 == 1)
        {
            Console.WriteLine("Enter movie name(not uppercase sensitive)");
            string title = "";
            MoviesModel movie;
            do
            {
                title = Console.ReadLine();
                Console.Clear();
                if (title.Contains(" "))
                {
                    string[] words = title.Split(" ");
                    string newtitle = "";
                    foreach (string word in words)
                    {
                        string newword = char.ToUpper(word[0]) + word.Substring(1);
                        newtitle += newword;
                        newtitle += " ";
                    }
                    title = newtitle.Trim();
                }
                else
                {
                    title = char.ToUpper(title[0]) + title.Substring(1);
                    title.Trim();
                }
                movie = MoviesLogic.GetByTitle(title);
                if (MoviesLogic.GetByTitle(title) == null)
                {
                    Console.WriteLine("Invalid movie. Try again.");
                }
            } while (movie == null);
            newMovieId = Convert.ToInt32(movie.Id);
        }
        Console.WriteLine("Would you like to change the date/time?");
        Console.WriteLine("1. yes\n2. no");
        int question = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        while (question != 1 && question != 2)
        {
            Console.WriteLine("Invalid input. Try again");
            Console.WriteLine("Would you like to change the theater ID?");
            Console.WriteLine("1. yes\n2. no");
            question = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
        }
        if (question == 1)
        {
            bool backtodate = true;
            while (backtodate == true)
            {
                do
                {
                    Console.WriteLine("Enter date for this show in '%Y-%m-%d' format.");
                    Date = Console.ReadLine();
                    Console.Clear();
                    if (DateTime.TryParse(Date, out _) == false)
                    {
                        Console.WriteLine("Not a valid date time format. Try again.");
                    }
                } while (DateTime.TryParse(Date, out _) != true);

                do
                {
                    Console.WriteLine("On this date, in this theater the following movies play:");
                    Console.WriteLine("(The times include cleaning time)");
                    if (question1 == 1)
                    {
                        PrintShowsInTheaterThisDay(Date, newTheatreId);
                    }
                    else
                    {
                        PrintShowsInTheaterThisDay(Date, Convert.ToInt32(show.TheatreId));
                    }
                    Console.WriteLine("What time would you like to choose('HH:MM' format)?");
                    time = Console.ReadLine();
                    Console.Clear();
                    int timeinminutes = 0;
                    if (question2 == 1)
                    {
                        timeinminutes = Convert.ToInt32(MoviesLogic.GetById(newMovieId).TimeInMinutes);
                    }
                    else
                    {
                        timeinminutes = Convert.ToInt32(MoviesLogic.GetById(Convert.ToInt32(show.MovieId)).TimeInMinutes);
                    }
                    if (TimeSpan.TryParse(time, out _) == true)
                    {
                        if (WithinOpeningHours(time, timeinminutes) == true)
                        {
                            string endtime = GetEndTime(time, timeinminutes);
                            if (question1 == 1)
                            {
                                if (IsDoubleBooked(time, endtime, newTheatreId, Date) == false)
                                {
                                    backtodate = false;
                                    break;
                                }
                                else
                                {
                                    List<string> avalabletimes = AnotherSpotToday(timeinminutes, Date);
                                    Console.WriteLine("There is already a movie playing at this time.");
                                    if (avalabletimes.Count == 0)
                                    {
                                        Console.WriteLine("There are no available spots for this day. Choose a different day.");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Would you like to pick one of the suggested times for this day?");
                                        Console.WriteLine("1. Yes\n 2. No");
                                        int yesno = Convert.ToInt32(Console.ReadLine());
                                        Console.Clear();
                                        if (yesno == 1)
                                        {
                                            Console.WriteLine("You can choose one of the following times");
                                            int count = 0;
                                            foreach (var availabletime in avalabletimes)
                                            {
                                                count++;
                                                Console.WriteLine($"{count}. {availabletime}");
                                            }
                                            int timechoice = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();
                                            time = Convert.ToString(avalabletimes[timechoice - 1]);
                                            backtodate = false;
                                            break;
                                        }
                                        if (yesno == 2)
                                        {
                                            Console.WriteLine("You can choose another day then");
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (IsDoubleBooked(time, endtime, Convert.ToInt32(show.TheatreId), Date) == false)
                                {
                                    backtodate = false;
                                    break;
                                }
                                else
                                {
                                    List<string> avalabletimes = AnotherSpotToday(timeinminutes, Date);
                                    Console.WriteLine("There is already a movie playing at this time.");
                                    if (avalabletimes.Count == 0)
                                    {
                                        Console.WriteLine("There are no available spots for this day. Choose a different day.");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Would you like to pick one of the suggested times for this day?");
                                        Console.WriteLine("1. Yes\n 2. No");
                                        int yesno = Convert.ToInt32(Console.ReadLine());
                                        if (yesno == 1)
                                        {
                                            Console.WriteLine("You can choose one of the following times");
                                            int count = 0;
                                            foreach (var availabletime in avalabletimes)
                                            {
                                                count++;
                                                Console.WriteLine($"{count}. {availabletime}");
                                            }
                                            int timechoice = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();
                                            time = Convert.ToString(avalabletimes[timechoice - 1]);
                                            backtodate = false;
                                            break;
                                        }
                                        if (yesno == 2)
                                        {
                                            Console.WriteLine("You can choose another day then");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("The movietheatre is not opened at this time.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not a valid time");
                    }
                } while (backtodate == false);
            }
        }
        if (question == 1)
        {
            newDate_time = $"{Date} {time}";
            show.Date = newDate_time;
        }
        if (question1 == 1)
        {
            show.TheatreId = newTheatreId;
        }
        if (question2 == 1)
        {
            show.MovieId = newMovieId;
        }
        return show;
    }

    static public void ShowDelete(int id)
    {
        ShowLogic.DeleteShow(id);
    }

    static public void ShowAdd()
    {

        int newTheaterId;
        int newMovieId;
        string newDate_time;
        string Date = "";
        bool timecheck = false;
        string time = "";
        do
        {
            Console.WriteLine("Enter new theater ID for this movie.");
            newTheaterId = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            if (newTheaterId != 1 && newTheaterId != 2 && newTheaterId != 3)
            {
                Console.WriteLine("Theater ID does not exist. Try again.");
            }
        } while (newTheaterId != 1 && newTheaterId != 2 && newTheaterId != 3);
        // do
        // {
        //     Console.WriteLine("Enter movie ID for this movie.");
        //     newMovieId = Convert.ToInt32(Console.ReadLine());
        //     if (MoviesAccess.GetById(newMovieId) == null)
        //     {
        //         Console.WriteLine("Movie ID does not exist. Try again.");
        //     }
        // } while (MoviesAccess.GetById(newMovieId) == null);
        string title = "";
        MoviesModel movie;
        List<MoviesModel> movies = MoviesLogic.GetAllMovies();
        Console.WriteLine("Movies you can choose from: ");
        foreach (var item in movies)
        {
            Console.WriteLine($"- {item.Title}");
        }
        Console.WriteLine("\nEnter movie name(not uppercase sensitive)");
        do
        {
            title = Console.ReadLine();
            Console.Clear();
            if (title.Contains(" "))
            {
                string[] words = title.Split(" ");
                string newtitle = "";
                foreach (string word in words)
                {
                    string newword = char.ToUpper(word[0]) + word.Substring(1);
                    newtitle += newword;
                    newtitle += " ";
                }
                title = newtitle.Trim();
            }
            else
            {
                title = char.ToUpper(title[0]) + title.Substring(1);
                title.Trim();
            }
            movie = MoviesLogic.GetByTitle(title);
            if (MoviesLogic.GetByTitle(title) == null)
            {
                Console.WriteLine("Invalid movie. Try again.");
            }
        } while (movie == null);
        newMovieId = Convert.ToInt32(movie.Id);
        // do
        // {
        //     Console.WriteLine("Enter date for this show in '%Y-%m-%d %H:%M' format.");
        //     newDate_time = Console.ReadLine();
        //     if (newDate_time.Contains("-") != true && newDate_time.Contains(":") != true)
        //     {
        //         Console.WriteLine("Not a valid date time format. Try again.");
        //     }
        // } while (newDate_time.Contains("-") != true && newDate_time.Contains(":") != true);
        bool backtodate = true;
        while (backtodate == true)
        {
            do
            {
                Console.WriteLine("Enter date for this show in '%Y-%m-%d' format.");
                Date = Console.ReadLine();
                Console.Clear();
                if (DateTime.TryParse(Date, out _) == false)
                {
                    Console.WriteLine("Not a valid date time format. Try again.");
                }
            } while (DateTime.TryParse(Date, out _) != true);

            do
            {
                Console.WriteLine("On this date, in this theater the following movies play:");
                Console.WriteLine("(The times include cleaning time)");
                PrintShowsInTheaterThisDay(Date, newTheaterId);
                Console.WriteLine("What time would you like to choose('HH:MM' format)?");
                time = Console.ReadLine();
                Console.Clear();
                int timeinminutes = Convert.ToInt32(MoviesLogic.GetById(newMovieId).TimeInMinutes);
                if (TimeSpan.TryParse(time, out _) == true)
                {
                    if (WithinOpeningHours(time, timeinminutes) == true)
                    {
                        string endtime = GetEndTime(time, timeinminutes);
                        if (IsDoubleBooked(time, endtime, newTheaterId, Date) == false)
                        {
                            backtodate = false;
                            break;
                        }
                        else
                        {
                            List<string> avalabletimes = AnotherSpotToday(timeinminutes, Date);
                            Console.WriteLine("There is already a movie playing at this time.");
                            if (avalabletimes.Count == 0)
                            {
                                Console.WriteLine("There are no available spots for this day. Choose a different day.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Would you like to pick one of the suggested times for this day?");
                                Console.WriteLine("1. Yes\n 2. No");
                                int yesno = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                if (yesno == 1)
                                {
                                    Console.WriteLine("You can choose one of the following times");
                                    int count = 0;
                                    foreach (var availabletime in avalabletimes)
                                    {
                                        count++;
                                        Console.WriteLine($"{count}. {availabletime}");
                                    }
                                    int timechoice = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                    time = Convert.ToString(avalabletimes[timechoice - 1]);
                                    backtodate = false;
                                    break;
                                }
                                if (yesno == 2)
                                {
                                    Console.WriteLine("You can choose another day then");
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The movietheatre is not opened at this time.");
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid time");
                }
            } while (backtodate == false);
        }
        newDate_time = $"{Date} {time}";
        ShowModel new_show = new ShowModel(1, newTheaterId, newMovieId, newDate_time);

        ShowLogic.WriteShow(new_show);

    }

    static void ShowPrint()
    {
        List<ShowModel> shows = ShowLogic.GetAllShows();
        foreach (ShowModel show in shows)
        {
            Console.WriteLine($"ID: {show.Id}");
            Console.WriteLine($"TheaterID: {show.TheatreId}");
            Console.WriteLine($"MovieID: {show.MovieId}");
            Console.WriteLine($"Date: {show.Date}");
            Console.WriteLine("-----------------------------------------------");
        }
    }
    public static Dictionary<int, string> PrintOverviewMovie_Time()
    {
        Console.WriteLine("On what day would you like to watch a movie?");
        Console.WriteLine("1. Monday\n2. Tuesday\n3. Wednesday\n4. Thursday");
        Console.WriteLine("5. Friday\n6. Saturday\n7. Sunday");
        int Day;
        do
        {
            Day = Convert.ToInt32(Console.ReadLine());
            if (Day < 0 || Day > 7)
            {
                Console.WriteLine("Invalid Input.Try again.");
            }
        } while (Day < 0 || Day > 7);
        Console.Clear();
        string DayToPrint = Convert.ToString((DayOfWeek)((Day) % 7));

        Dictionary<int, string> movies = Movie.MakeMovieDict();
        List<ShowModel> shows = ShowLogic.GetAllShows();

        List<ShowModel> ShowsOnDay = new List<ShowModel>();
        Dictionary<int, string> MovieCanWatch = new Dictionary<int, string>();

        int moviecount = 0;

        foreach (var show in shows)
        {
            string object_day = show.Date;
            DayOfWeek showDate = DateTime.Parse(object_day).DayOfWeek;
            string dayofweek = Convert.ToString(showDate);
            if (dayofweek == DayToPrint)
            {
                ShowsOnDay.Add(show);
            }
        }
        Console.WriteLine(DayToPrint);
        DateTime CurrentDate = DateTime.Now;
        DateTime oneweekfromnow = CurrentDate.AddDays(7).AddSeconds(-1);
        foreach (var movie in movies)
        {
            bool moviePrinted = false;
            string showTimes = "";
            foreach (var show in ShowsOnDay)
            {
                DateTime DateAndTime = DateTime.Parse(show.Date);
                if (movie.Key == show.MovieId)
                {
                    if (CurrentDate < DateAndTime && DateAndTime < oneweekfromnow)
                    {
                        if (moviePrinted == false)
                        {
                            Console.WriteLine("movie title: " + movie.Value);
                            Console.WriteLine("available show times:");
                            moviePrinted = true;
                            moviecount++;
                            MovieCanWatch.Add(moviecount, movie.Value);
                        }
                        if (showTimes == "")
                        {
                            showTimes += $"{show.Date.Split(" ")[1]}";
                        }
                        else
                        {
                            showTimes += $" {show.Date.Split(" ")[1]}";
                        }
                    }
                }
            }
            if (showTimes != "")
            {
                Console.WriteLine(showTimes);
                Console.WriteLine("-----------------------------------");
            }
        }
        day = Day;
        return MovieCanWatch;
    }

    public static Dictionary<int, ShowModel> TimeOptions(string movie_name, int Day)
    {
        if (Day == 7)
        {
            Day = 0;
        }
        Dictionary<int, string> movies = Movie.MakeMovieDict();
        Dictionary<int, ShowModel> ShowTime = new Dictionary<int, ShowModel>();
        int movie_id = 0;
        foreach (var movie in movies)
        {
            if (movie.Value == movie_name)
            {
                movie_id = movie.Key;
            }
        }
        List<ShowModel> shows = new List<ShowModel>(ShowAccess.GetByMovieID(movie_id));
        int count = 1;
        DateTime CurrentDate = DateTime.Now;
        DateTime oneweekfromnow = CurrentDate.AddDays(7).AddSeconds(-1);
        DayOfWeek dayneeded = (DayOfWeek)Day;
        int daystilldayneeded = ((int)dayneeded - (int)CurrentDate.DayOfWeek + 7) % 7;
        DateTime DDay = CurrentDate.AddDays(daystilldayneeded);
        foreach (var show in shows)
        {
            string show_date = show.Date.Split(" ")[0];
            string ddate = DDay.ToString("yyyy-MM-dd");
            DateTime showdate = DateTime.Parse(show.Date);
            if (show_date == ddate)
            {
                if (showdate > CurrentDate && showdate < oneweekfromnow)
                {
                    ShowTime.Add(count, show);
                    count++;
                }
            }
        }
        return ShowTime;
    }

    // public static void PrintMovie()
    // {
    //     ShowModel movie = Movie.
    //     List<ShowModel> shows = ShowLogic.GetAllShows();
    //     foreach (var movie in movies)
    //     {
    //         MoviesModel MovieInfo = MoviesLogic.GetById(movie.Key);
    //         bool moviePrinted = false;
    //         string days = "";
    //         foreach (var show in shows)
    //         {
    //             if (movie.Key == show.MovieId)
    //             {
    //                 if (moviePrinted == false)
    //                 {
    //                     Console.WriteLine($"Movie: {movie.Value}");
    //                     Console.WriteLine($"Genre: {MovieInfo.Genre}");
    //                     Console.WriteLine($"Time in minutes: {MovieInfo.TimeInMinutes}");
    //                     Console.WriteLine($"Release Date: {MovieInfo.ReleaseDate}");
    //                     Console.WriteLine($"Director: {MovieInfo.Director}");
    //                     Console.WriteLine($"Descrition: {MovieInfo.Description}");
    //                     moviePrinted = true;
    //                 }
    //                 DayOfWeek showDate = DateTime.Parse(show.Date).DayOfWeek;
    //                 string StringshowDate = Convert.ToString(showDate);
    //                 days = days + " " + StringshowDate;
    //             }
    //         }
    //         Console.WriteLine($"Plays on:{days}");
    //     }
    // }

    public static void WeekOverviewMovies()
    {
        Dictionary<int, string> movies = Movie.MakeMovieDict();
        List<ShowModel> shows = ShowLogic.GetAllShows();
        for (int i = 0; i < 7; i++)
        {
            bool printed = false;
            DateTime CurrentDate = DateTime.Now;
            DateTime Datetoprint = CurrentDate.AddDays(i);
            DayOfWeek CurrentDay = CurrentDate.DayOfWeek;
            DayOfWeek DayToPrint = (DayOfWeek)(((int)CurrentDay + i) % 7);
            string StringCurrentDate = Convert.ToString(Datetoprint).Split(" ")[0];
            Console.WriteLine($"{DayToPrint} {StringCurrentDate}");
            Console.WriteLine("________________________________________");
            Dictionary<string, string> movieTimes = new Dictionary<string, string>();
            DateTime oneweekfromnow = CurrentDate.AddDays(7).AddSeconds(-1);
            foreach (var show in shows)
            {
                DateTime DateAndTime = DateTime.Parse(show.Date);

                string StringDate = Convert.ToString(DateAndTime).Split(" ")[0];

                string StringTime = show.Date.Split(" ")[1]; ;
                string StringCurrentTime = Convert.ToString(CurrentDate).Split(" ")[1];

                if (CurrentDate < DateAndTime && DateAndTime < oneweekfromnow)
                {
                    if (DateAndTime.DayOfWeek == DayToPrint)
                    {
                        foreach (var movie in movies)
                        {
                            if (movie.Key == show.MovieId)
                            {
                                if (movieTimes.ContainsKey(movie.Value))
                                {
                                    movieTimes[movie.Value] += $" {StringTime}";
                                }
                                else
                                {
                                    movieTimes[movie.Value] = StringTime;
                                }
                            }
                        }
                    }
                }
            }
            foreach (var movietime in movieTimes)
            {
                Console.WriteLine($"{movietime.Key} {movietime.Value}");
                printed = true;
            }
            if (printed == false)
            {
                Console.WriteLine("No movies play on this day.");
            }
            Console.WriteLine("----------------------------------------\n\n");
        }
    }

    public static string GetEndTime(string time, int TimeInMinutes)
    {
        DateTime datetime = DateTime.Parse(time);
        datetime = datetime.AddMinutes(TimeInMinutes); // movie time
        datetime = datetime.AddMinutes(20); // cleaning time
        string newTime = datetime.ToString("HH:mm");
        return newTime;
    }

    public static void PrintShowsInTheaterThisDay(string date, int theater)
    {
        List<ShowModel> shows = ShowLogic.GetAllShows();
        foreach (var show in shows)
        {
            string showdate = show.Date.Split(" ")[0];
            if (showdate == date)
            {
                if (show.TheatreId == theater)
                {
                    MoviesModel movie = MoviesLogic.GetById(Convert.ToInt32(show.MovieId));
                    string time = show.Date.Split(" ")[1];
                    int minutes = Convert.ToInt32(movie.TimeInMinutes);
                    string endtime = GetEndTime(time, minutes);
                    Console.WriteLine(movie.Title);
                    Console.WriteLine($"{time} - {endtime}");
                }
            }
        }
    }

    public static bool WithinOpeningHours(string starttime, int TimeInMinutes)
    {
        TimeSpan Starttime = TimeSpan.Parse(starttime);
        TimeSpan Endtime = TimeSpan.Parse(GetEndTime(starttime, TimeInMinutes));
        TimeSpan openingTime = new(10, 0, 0);
        TimeSpan closingTime = new(23, 59, 59);
        if (Starttime >= openingTime && Endtime < closingTime && Endtime > openingTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsDoubleBooked(string newstarttime, string newendtime, int theaterid, string date)
    {
        TimeSpan NewStartTime = TimeSpan.Parse(newstarttime);
        TimeSpan NewEndTime = TimeSpan.Parse(newendtime);
        List<ShowModel> shows = ShowLogic.AllOrderedByDate(date);
        bool doublebooked = false;
        foreach (var show in shows)
        {
            MoviesModel movie = MoviesLogic.GetById(Convert.ToInt32(show.MovieId));
            string starttime = show.Date.Split(" ")[1];
            TimeSpan StartTime = TimeSpan.Parse(starttime); // starttime is the time of the already planned show
            int minutes = Convert.ToInt32(movie.TimeInMinutes);
            string endtime = GetEndTime(starttime, minutes);
            TimeSpan EndTime = TimeSpan.Parse(endtime);
            if (theaterid == show.TheatreId)
            {
                if ((StartTime <= NewStartTime && NewStartTime < EndTime) || (StartTime < NewEndTime && EndTime >= NewEndTime))
                {
                    doublebooked = true;
                }
            }
        }
        if (doublebooked == false)
        {
            return doublebooked;
        }
        else
        {
            return doublebooked;
        }
    }

    public static List<string> AnotherSpotToday(int minutesNewShow, string date)
    {
        // TimeSpan NewStartTime = TimeSpan.Parse(newstarttime);
        // TimeSpan NewEndTime = TimeSpan.Parse(newendtime);
        List<ShowModel> shows = ShowLogic.AllOrderedByDate(date);
        TimeSpan openingtime = new(10, 0, 0);
        TimeSpan StartOpenSpot = new(10, 0, 0); // opening time
        TimeSpan ClosingTime = new(23, 59, 59); // closing time
        List<string> AvailableStartTimes = new List<string>();
        foreach (var show in shows)
        {
            MoviesModel movie = MoviesLogic.GetById(Convert.ToInt32(show.MovieId));

            string starttime = show.Date.Split(" ")[1];
            TimeSpan StartTime = TimeSpan.Parse(starttime); // start time planned show

            int minutes = Convert.ToInt32(movie.TimeInMinutes); // playtime movie

            string endtime = GetEndTime(starttime, minutes);
            TimeSpan EndTime = TimeSpan.Parse(endtime); // endtime planned show

            int totalMinutes = (int)Math.Ceiling(EndTime.TotalMinutes / 5.0) * 5; // rounds the endtime up so the next available starttime is a round nuumber
            EndTime = TimeSpan.FromMinutes(totalMinutes);

            TimeSpan TimeAvailable = StartTime.Subtract(StartOpenSpot);
            int MinutesAvailable = Convert.ToInt32(TimeAvailable.TotalMinutes);

            TimeSpan PotentialEndtime = StartOpenSpot.Add(TimeSpan.FromMinutes(minutesNewShow + 20));

            if (StartOpenSpot < StartTime && MinutesAvailable >= minutesNewShow && PotentialEndtime < ClosingTime && StartOpenSpot >= openingtime)
            {
                string add = StartOpenSpot.ToString(@"hh\:mm");
                AvailableStartTimes.Add(add);
            }


            StartOpenSpot = EndTime;
        }
        return AvailableStartTimes;
    }
}