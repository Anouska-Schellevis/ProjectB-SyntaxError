class Show
{

    static public void Main()
    {
        AdminStart();
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
                int Title_to_edit = Convert.ToInt32(Console.ReadLine());
                ShowModel show = ShowLogic.GetByID(Title_to_edit);
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
        PrintOverviewMovie_Time();
        Console.WriteLine("What movie would you like to watch?");
        string Movie_to_watch = Console.ReadLine();
        MoviesModel movie = MoviesLogic.GetByTitle(Movie_to_watch);
        if (movie != null)
        {
            string Date_time;
            do
            {
                Console.WriteLine("And at what date and time? Enter in '%Y-%m-%d %H:%M' format");
                Date_time = Console.ReadLine();
                if (Date_time.Contains("-") != true && Date_time.Contains(":") != true)
                {
                    Console.WriteLine("Not a valid date time format. Try again.");
                }
            } while (Date_time.Contains("-") != true && Date_time.Contains(":") != true);

            int Movie_Id = Convert.ToInt32(movie.Id);
            List<ShowModel> shows = new List<ShowModel>(ShowAccess.GetByMovieID(Movie_Id));

            if (shows != null)
            {
                foreach (ShowModel show in shows)
                {
                    if (show.Date == Date_time)
                    {
                        //Call reservering hier
                        break;
                    }
                    else
                    {
                        Console.WriteLine("There is no movie on this date and time.");
                        UserStart();
                    }
                }

            }
            else
            {
                Console.WriteLine("A show of that movie doesn't exist yet.");
                UserStart();
            }
        }
        else
        {
            Console.WriteLine("Such a movie does not exist");
            UserStart();
        }
    }

    static public ShowModel ShowEdit(ShowModel show)
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
            Console.WriteLine("And at what time? Enter in '%Y-%m-%d %H:%M' format");
            newDate_time = Console.ReadLine();
            if (newDate_time.Contains("-") != true && newDate_time.Contains(":") != true)
            {
                Console.WriteLine("Not a valid date time format. Try again.");
            }
        } while (newDate_time.Contains("-") != true && newDate_time.Contains(":") != true);

        show.TheaterId = newTheaterId;
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
            Console.WriteLine("And at what time? Enter in '%Y-%m-%d %H:%M' format");
            newDate_time = Console.ReadLine();
            if (newDate_time.Contains("-") != true && newDate_time.Contains(":") != true)
            {
                Console.WriteLine("Not a valid date time format. Try again.");
            }
        } while (newDate_time.Contains("-") != true && newDate_time.Contains(":") != true);

        ShowModel new_show = new ShowModel(1, newTheaterId, newMovieId, newDate_time);
        ShowLogic.WriteShow(new_show);

    }

    static public void ShowPrint()
    {
        List<ShowModel> shows = ShowLogic.GetAllShows();
        foreach (ShowModel show in shows)
        {
            Console.WriteLine($"ID: {show.Id}");
            Console.WriteLine($"TheaterID: {show.TheaterId}");
            Console.WriteLine($"MovieID: {show.MovieId}");
            Console.WriteLine($"Date: {show.Date}");
            Console.WriteLine("-----------------------------------------------");
        }
    }

    public static void PrintOverviewMovie_Time()
    {
        Dictionary<int, string> movies = Movie.MakeMovieDict();
        List<ShowModel> shows = ShowLogic.GetAllShows();
        foreach (var movie in movies)
        {
            Console.WriteLine(movie);
            foreach (var show in shows)
            {
                if (movie.Key == show.MovieId)
                {
                    Console.WriteLine(show.Date);
                }
            }
        }

    }
}