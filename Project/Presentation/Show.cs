class Show
{
    static public void Main()
    {
        // bool admin = false;
        // if (admin)
        // { AdminStart(); }
        // else
        // { UserStart(); }
    }
    static public void AdminStart()
    {
        Console.WriteLine("[1] Overview of all show");
        Console.WriteLine("[2] Add Show");
        Console.WriteLine("[3] Edit Show");
        Console.WriteLine("[4] Delete Show");
        Console.WriteLine("What would you like to do?");
        int choice = Convert.ToInt32(Console.ReadLine());

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
                int idToDelete;
                do
                {
                    Console.WriteLine("Enter the id of the show you want to delete");
                    idToDelete = Convert.ToInt32(Console.ReadLine());
                    if (MoviesAccess.GetById(idToDelete) == null)
                    {
                        Console.WriteLine("This ID does not exist. Try again");
                    }
                } while (MoviesAccess.GetById(idToDelete) == null);
                ShowDelete(idToDelete);
                Console.WriteLine("Show is deleted");
                break;
            default:
                Console.WriteLine("No valid option selected. Please try again.");
                AdminStart();
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
        Console.Clear();
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
                do
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1 || choice == 2 || choice == 3)
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
                } while (choice < 1 || choice > 4);
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
                }while (secondchoice != 1 && secondchoice != 2);
                Console.Clear();
                switch (secondchoice)
                {
                    case 1:
                        if (movie != null)
                        {
                            Dictionary<int, ShowModel> showtime = TimeOptions(movies[chosennumber]);
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
                    Dictionary<int, ShowModel> showtime = TimeOptions(movies[chosennumber]);
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
            default:
                Console.WriteLine("Unexpected choice");
                break;

        }
    }

    static public ShowModel ShowEdit(ShowModel show)
    {

        int newTheatreId;
        int newMovieId;
        string newDate_time;
        do
        {
            Console.WriteLine("Enter new theater ID for this movie.");
            newTheatreId = Convert.ToInt32(Console.ReadLine());
            if (newTheatreId != 1 && newTheatreId != 2 && newTheatreId != 3)
            {
                Console.WriteLine("Theater ID does not exist. Try again.");
            }
        } while (newTheatreId != 1 && newTheatreId != 2 && newTheatreId != 3);
        do
        {
            Console.WriteLine("Enter movie ID for this movie.");
            newMovieId = Convert.ToInt32(Console.ReadLine());
            if (MoviesAccess.GetById(newMovieId) == null)
            {
                Console.WriteLine("Movie ID does not exist. Try again.");
            }
        } while (MoviesAccess.GetById(newMovieId) == null);
        do
        {
            Console.WriteLine("And at what time? Enter in '%Y-%m-%d %H:%M' format");
            newDate_time = Console.ReadLine();
            if (newDate_time.Contains("-") != true && newDate_time.Contains(":") != true)
            {
                Console.WriteLine("Not a valid date time format. Try again.");
            }
        } while (newDate_time.Contains("-") != true && newDate_time.Contains(":") != true);


        show.TheatreId = newTheatreId;
        show.MovieId = newMovieId;
        show.Date = newDate_time;
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
        do
        {
            Console.WriteLine("Enter new theater ID for this movie.");
            newTheaterId = Convert.ToInt32(Console.ReadLine());
            if (newTheaterId != 1 && newTheaterId != 2 && newTheaterId != 3)
            {
                Console.WriteLine("Theater ID does not exist. Try again.");
            }
        } while (newTheaterId != 1 && newTheaterId != 2 && newTheaterId != 3);
        do
        {
            Console.WriteLine("Enter movie ID for this movie.");
            newMovieId = Convert.ToInt32(Console.ReadLine());
            if (MoviesAccess.GetById(newMovieId) == null)
            {
                Console.WriteLine("Movie ID does not exist. Try again.");
            }
        } while (MoviesAccess.GetById(newMovieId) == null);
        do
        {
            Console.WriteLine("Enter date for this show in '%Y-%m-%d %H:%M' format.");
            newDate_time = Console.ReadLine();
            if (newDate_time.Contains("-") != true && newDate_time.Contains(":") != true)
            {
                Console.WriteLine("Not a valid date time format. Try again.");
            }
        } while (newDate_time.Contains("-") != true && newDate_time.Contains(":") != true);

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
        }while (Day < 0 || Day > 7);
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
        foreach (var movie in movies)
        {
            bool moviePrinted = false;
            string showTimes = "";
            foreach (var show in ShowsOnDay)
            {
                DateTime DateAndTime = DateTime.Parse(show.Date);
                if (movie.Key == show.MovieId)
                {
                    if (CurrentDate < DateAndTime)
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
        return MovieCanWatch;
    }

    public static Dictionary<int, ShowModel> TimeOptions(string movie_name)
    {
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
        foreach (var show in shows)
        {
            DateTime showdate = DateTime.Parse(show.Date);
            if (showdate > DateTime.Now)
            {
                ShowTime.Add(count, show);
                count++;
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
            DayOfWeek CurrentDay = CurrentDate.DayOfWeek;
            DayOfWeek DayToPrint = (DayOfWeek)(((int)CurrentDay + i) % 7);
            string StringCurrentDate = Convert.ToString(CurrentDate).Split(" ")[0];
            Console.WriteLine($"{DayToPrint} {StringCurrentDate}");
            Console.WriteLine("________________________________________");
            Dictionary<string, string> movieTimes = new Dictionary<string, string>();
            foreach (var show in shows)
            {
                DateTime DateAndTime = DateTime.Parse(show.Date);

                string StringDate = Convert.ToString(DateAndTime).Split(" ")[0];

                string StringTime = show.Date.Split(" ")[1]; ;
                string StringCurrentTime = Convert.ToString(CurrentDate).Split(" ")[1];

                if (CurrentDate < DateAndTime)
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
}