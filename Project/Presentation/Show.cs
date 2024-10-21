class Show
{

    static public void Main()
    {
        Start();
    }
    static public void Start()
    {
        Console.WriteLine("[1] Overview of all show");
        Console.WriteLine("[2] Add Show");
        Console.WriteLine("[3] Edit Show");
        Console.WriteLine("[4] Delete Show");
        // Console.WriteLine("[5] Search Show by title");
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
                // Console.WriteLine("Enter the title to edit");
                // string Title_to_edit = Console.ReadLine();
                // MoviesModel movie = MoviesLogic.GetByTitle(Title_to_edit);
                // if (movie != null)
                // {
                //     movie = MovieEdit(movie);
                //     MoviesLogic.UpdateMovie(movie);
                //     Console.WriteLine("Movie is saved!");
                // }
                // else
                // {
                //     Console.WriteLine("Such a movie does not exist");
                // }
                break;
            case 4:
                Console.WriteLine("Enter the id of the show you want to delete");
                int idToDelete = Convert.ToInt32(Console.ReadLine());
                ShowDelete(idToDelete);
                Console.WriteLine("Show is deleted");
                break;
            // case 5:
            //     Console.WriteLine("Enter the title of the movie you want to search for");
            //     string Title_to_search = Console.ReadLine();
            //     MovieSearch(Title_to_search);
            //     break;
            default:
                Console.WriteLine("No valid option selected. Please try again.");
                Start();
                break;
        }
    }

    static public ShowModel ShowEdit(ShowModel show)
    {
        Console.WriteLine("Enter new theater ID for this movie.");
        int newTheaterId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter movie ID for this movie.");
        int newMovieId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter reseservation ID for this movie.");
        int newReservationId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter new start time for this movie.");
        string newStartTime = Console.ReadLine();

        show.TheaterId = newTheaterId;
        show.MovieId = newMovieId;
        show.ReservationId = newReservationId;
        show.StartTime = newStartTime;
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
        Console.WriteLine("Enter reseservation ID for this movie.");
        int newReservationId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter new start time for this movie.");
        string newStartTime = Console.ReadLine();
        ShowModel new_show = new ShowModel(1, newTheaterId, newMovieId, newReservationId, newStartTime);
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
            Console.WriteLine($"ReservationID: {show.ReservationId}");
            Console.WriteLine($"Start Time: {show.StartTime}");
            Console.WriteLine("-----------------------------------------------");
        }
    }

    // static public void ShowSearch(string Title)
    // {
    //     var movie = ShowLogic.GetByTitle(Title);
    //     Console.WriteLine($"ID: {movie.Id}");
    //     Console.WriteLine($"Title: {movie.Title}");
    //     Console.WriteLine($"Genre: {movie.Genre}");
    //     Console.WriteLine($"Director: {movie.Director}");
    //     Console.WriteLine($"Release Date: {movie.ReleaseDate}");
    //     Console.WriteLine($"Time in minutes: {movie.TimeInMinutes} minutes");
    //     Console.WriteLine($"Description: {movie.Description}");
    //     Console.WriteLine("-----------------------------------------------");
    // }
}