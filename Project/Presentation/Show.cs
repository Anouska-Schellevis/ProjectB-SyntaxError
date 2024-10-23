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
                Console.WriteLine("Enter the id of the show you want to delete");
                int idToDelete = Convert.ToInt32(Console.ReadLine());
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

            Console.WriteLine("And at what time? Enter in '%Y-%m-%d %H:%M' format");
            string Date_time = Console.ReadLine();
            int Movie_Id = Convert.ToInt32(movie.Id);

            List<ShowModel> shows = new List<ShowModel>(ShowAccess.GetByMovieID(Movie_Id));




            if (shows != null)
            {
                foreach (ShowModel show in shows)
                {
                    if (show.TheatreId == 1)
                    {
                        Theater150 theater = new Theater150();
                        theater.SelectSeats();
                    }
                }
                Console.WriteLine("There is no movie on this date and time.");
                UserStart();
            }
            else
            {
                Console.WriteLine("Such a time does not exist");
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
        Console.WriteLine("Enter new theater ID for this movie.");
        int newTheatreId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter movie ID for this movie.");
        int newMovieId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter date for this movie in '%Y-%m-%d %H:%M' format.");
        string newDate = Console.ReadLine();

        show.TheatreId = newTheaterId;
        show.MovieId = newMovieId;
        show.Date = newDate;
        return show;
    }

    static public void ShowDelete(int id)
    {
        ShowLogic.DeleteShow(id);
    }

    static public void ShowAdd()
    {
        Console.WriteLine("Enter new theater ID for this movie.");
        int newTheaterId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter movie ID for this movie.");
        int newMovieId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter release date for this movie.");
        string newDate = Console.ReadLine();
        ShowModel new_show = new ShowModel(1, newTheaterId, newMovieId, newDate);
        ShowLogic.WriteShow(new_show);

    }

    static public void ShowPrint()
    {
        List<ShowModel> shows = ShowLogic.GetAllShows();
        foreach (ShowModel show in shows)
        {
            Console.WriteLine($"ID: {show.Id}");
            Console.WriteLine($"TheaterID: {show.TheatreId}");
            Console.WriteLine($"TheaterID: {show.TheatreId}");
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
            Console.WriteLine(movie.Value);
            foreach (var show in shows)
            {
                //Console.WriteLine(show.MovieId)
                if (movie.Key == show.MovieId)
                {
                    Console.WriteLine(show.Date);
                }
            }
        }

    }
}