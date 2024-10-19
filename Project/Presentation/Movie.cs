class Movie
{

    static public void Main()
    {
        Start();
    }
    static public void Start()
    {
        Console.WriteLine("[1] Overview of all movies");
        Console.WriteLine("[2] Add Movie");
        Console.WriteLine("[3] Edit Movie");
        Console.WriteLine("[4] Delete Movie");
        Console.WriteLine("[5] Search Movie by title");
        Console.WriteLine("What would you like to do?");
        int choice = Convert.ToInt32(Console.ReadLine());
        
        switch (choice)
        {
            case 1:
                MoviePrint();
                break;
            case 2:

                MovieAdd();
                Console.WriteLine("Movie is added");
                break;
            case 3:
                Console.WriteLine("Enter the title to edit");
                string Title_to_edit = Console.ReadLine();
                MoviesModel movie = MoviesLogic.GetByTitle(Title_to_edit);
                if (movie != null)
                {
                    movie = MovieEdit(movie);
                    MoviesLogic.UpdateMovie(movie);
                    Console.WriteLine("Movie is saved!");
                }
                else
                {
                    Console.WriteLine("Such a movie does not exist");
                }
                break;
            case 4:
                Console.WriteLine("Enter the id of the movie you want to delete");
                int idToDelete = Convert.ToInt32(Console.ReadLine());
                MovieDelete(idToDelete);
                Console.WriteLine("Movie is deleted");
                break;
            case 5:
                Console.WriteLine("Enter the title of the movie you want to search for");
                string Title_to_search = Console.ReadLine();
                MovieSearch(Title_to_search);
                break;
            default:
                Console.WriteLine("No valid option selected. Please try again.");
                Start();
                break;
        }
    }

    static public MoviesModel MovieEdit(MoviesModel movie)
    {
        Console.WriteLine("Enter new time in minutes for this movie.");
        int newTimeInMinutes = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter new genre for this movie.");
        string newGenre = Console.ReadLine();
        Console.WriteLine("Enter new description for this movie.");
        string newDescription = Console.ReadLine();
        Console.WriteLine("Enter new title for this movie.");
        string newTitle = Console.ReadLine();
        Console.WriteLine("Enter new director for this movie.");
        string newDirector = Console.ReadLine();
        Console.WriteLine("Enter new release_date for this movie.");
        string newReleaseDate = Console.ReadLine();

        movie.TimeInMinutes = newTimeInMinutes;
        movie.Genre = newGenre;
        movie.Description = newDescription;
        movie.Title = newTitle;
        movie.Director = newDirector;
        movie.ReleaseDate = newReleaseDate;
        return movie;
    }

    static public void MovieDelete(int id)
    {
        MoviesLogic.DeleteMovie(id);
    }

    static public void MovieAdd()
    {
        Console.WriteLine("Enter new time in minutes for this movie.");
        int newTimeInMinutes = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter new genre for this movie.");
        string newGenre = Console.ReadLine();
        Console.WriteLine("Enter new description for this movie.");
        string newDescription = Console.ReadLine();
        Console.WriteLine("Enter new title for this movie.");
        string newTitle = Console.ReadLine();
        Console.WriteLine("Enter new director for this movie.");
        string newDirector = Console.ReadLine();
        Console.WriteLine("Enter new release_date for this movie.");
        string newReleaseDate = Console.ReadLine();
        MoviesModel new_movie = new MoviesModel(1, newTimeInMinutes, newGenre, newDescription, newTitle, newDirector, newReleaseDate);
        MoviesLogic.WriteMovie(new_movie);

    }

    static public void MoviePrint()
    {
        // List<MoviesModel> movies = MoviesLogic.GetAllMovies();
        // foreach (MoviesModel movie in movies)
        // {
        //     Console.WriteLine($"ID: {movie.Id}");
        //     Console.WriteLine($"Title: {movie.Title}");
        //     Console.WriteLine($"Genre: {movie.Genre}");
        //     Console.WriteLine($"Director: {movie.Director}");
        //     Console.WriteLine($"Release Date: {movie.ReleaseDate}");
        //     Console.WriteLine($"Time in minutes: {movie.TimeInMinutes} minutes");
        //     Console.WriteLine($"Description: {movie.Description}");
        //     Console.WriteLine("-----------------------------------------------");
        // }
        int Count = 1;
        MoviesModel movie = MoviesLogic.GetById(Count);
        while (movie != null)
        {
            Count += 1;
            movie = MoviesLogic.GetById(Count);
        }
    }

    static public void MovieSearch(string Title)
    {
        var movie = MoviesLogic.GetByTitle(Title);
        Console.WriteLine($"ID: {movie.Id}");
        Console.WriteLine($"Title: {movie.Title}");
        Console.WriteLine($"Genre: {movie.Genre}");
        Console.WriteLine($"Director: {movie.Director}");
        Console.WriteLine($"Release Date: {movie.ReleaseDate}");
        Console.WriteLine($"Time in minutes: {movie.TimeInMinutes} minutes");
        Console.WriteLine($"Description: {movie.Description}");
        Console.WriteLine("-----------------------------------------------");
    }
}