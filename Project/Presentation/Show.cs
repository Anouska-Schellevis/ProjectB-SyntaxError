class Show
{
    static public void Main()
    {
        bool admin = false;
        if (admin)
        { AdminStart(); }
        else
        { UserStart(); }
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

    static public void UserStart()
    {
        //ShowPrint();
        Dictionary<int, string> movies = PrintOverviewMovie_Time();

        Console.WriteLine("What movie would you like to watch?");
        foreach (var movie_count in movies)
        {
            string movie_name = movie_count.Value;
            Console.WriteLine($"{movie_count.Key}. {movie_name}");
        }
        int chosenmovie;
        do
        {
            chosenmovie = Convert.ToInt32(Console.ReadLine());
            if (movies.ContainsKey(chosenmovie))
            {
                break;
            }
            else
            {
                Console.WriteLine("Not a valid choice. Try again.");
            }
        }
        while (true);
        MoviesModel movie = MoviesLogic.GetByTitle(movies[chosenmovie]);
        if (movie != null)
        {
            Dictionary<int, ShowModel> showtime = TimeOptions(movies[chosenmovie]);
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
            Console.WriteLine(ChosenShow.Date);
            if (ChosenShow.TheatreId == 1)
            {
                Theater150 theater = new Theater150();
                theater.SelectSeats(ChosenShow.MovieId);
            }
            if (ChosenShow.TheatreId == 2)
            {
                Theater300 theater2 = new Theater300();
                theater2.SelectSeats();
            }
            if (ChosenShow.TheatreId == 3)
            {
                Theater500 theater3 = new Theater500();
                theater3.SelectSeats();
            }
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
            if (newTheatreId != 1 || newTheatreId != 2 || newTheatreId != 3)
            {
                Console.WriteLine("Theater ID does not exist. Try again.");
            }
        } while (newTheatreId != 1 || newTheatreId != 2 || newTheatreId != 3);
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
            if (newTheaterId != 1 || newTheaterId != 2 || newTheaterId != 3)
            {
                Console.WriteLine("Theater ID does not exist. Try again.");
            }
        } while (newTheaterId != 1 || newTheaterId != 2 || newTheaterId != 3);
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
        string Day = Console.ReadLine();
        Day = char.ToUpper(Day[0]) + Day.Substring(1);
        Dictionary<int, string> movies = Movie.MakeMovieDict();
        List<ShowModel> shows = ShowLogic.GetAllShows();
        List<ShowModel> ShowsOnDay = new List<ShowModel>();
        int moviecount = 1;
        Dictionary<int, string> MovieCanWatch = new Dictionary<int, string>();
        foreach (var show in shows)
        {
            string object_day = show.Date;
            DayOfWeek showDate = DateTime.Parse(object_day).DayOfWeek;
            string dayofweek = Convert.ToString(showDate);
            if (dayofweek == Day)
            {
                ShowsOnDay.Add(show);
            }
        }
        Console.WriteLine(Day);
        foreach (var movie in movies)
        {
            // Console.WriteLine("movie title: " + movie.Value);
            // Console.WriteLine("available show times:");
            // foreach (var show in ShowsOnDay)
            // {
            //     //Console.WriteLine(show.MovieId)

            //     //Console.WriteLine("available show times:");
            //     if (movie.Key == show.MovieId)
            //     {
            //         Console.WriteLine(show.Date);
            //     }
            // }
            bool moviePrinted = false;
            foreach (var show in ShowsOnDay)
            {
                if (movie.Key == show.MovieId)
                {
                    if (moviePrinted == false)
                    {
                        Console.WriteLine("movie title: " + movie.Value);
                        Console.WriteLine("available show times:");
                        moviePrinted = true;
                        MovieCanWatch.Add(moviecount, movie.Value);
                        moviecount++;
                    }
                    Console.WriteLine(show.Date);
                }
            }
            Console.WriteLine("-----------------------------------");
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
            // string time = show.Date.Split(' ')[1];
            ShowTime.Add(count, show);
            count++;
        }
        return ShowTime;
    }
}