using Microsoft.VisualBasic;

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
        Console.WriteLine("[6] See most populair movie genre");
        Console.WriteLine("What would you like to do?");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.Clear();
                MoviePrint();
                break;
            case 2:
                Console.Clear();
                MovieAdd();
                Console.WriteLine("Movie is added");
                break;
            case 3:
                Console.Clear();
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
                Console.Clear();
                int idToDelete;
                do
                {
                    Console.WriteLine("Enter the id of the movie you want to delete");
                    idToDelete = Convert.ToInt32(Console.ReadLine());
                    if (MoviesAccess.GetById(idToDelete) == null)
                    {
                        Console.WriteLine("This ID does not exist. Try again");
                    }
                } while (MoviesAccess.GetById(idToDelete) == null);
                MovieDelete(idToDelete);
                Console.WriteLine("Movie is deleted");
                break;
            case 5:
                Console.Clear();
                Console.WriteLine("Enter the title of the movie you want to search for");
                string Title_to_search = Console.ReadLine();
                Console.WriteLine(MovieSearch(Title_to_search));
                break;
            case 6:
                Console.Clear();
                TrackPopularity();
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
        List<MoviesModel> movies = MoviesLogic.GetAllMovies();
        foreach (MoviesModel movie in movies)
        {
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Genre: {movie.Genre}");
            Console.WriteLine($"Director: {movie.Director}");
            Console.WriteLine($"Release Date: {movie.ReleaseDate}");
            Console.WriteLine($"Duration: {movie.TimeInMinutes} minutes");
            Console.WriteLine($"Description: {movie.Description}");
            Console.WriteLine("-----------------------------------------------");
        }
    }

    static public string MovieSearch(string Title)
    {
        var movie = MoviesLogic.GetByTitle(Title);
        if (movie != null)
        {
            string info = $@"ID: {movie.Id}
                Title: {movie.Title}
                Genre: {movie.Genre}
                Director: {movie.Director}
                Release Date: {movie.ReleaseDate}
                Time in minutes: {movie.TimeInMinutes} minutes
                Description: {movie.Description}
                -----------------------------------------------";
            return info;
        }
        else
        {
            return "Movie does not exist.";
        }
    }

    public static Dictionary<int, string> MakeMovieDict()
    {
        List<MoviesModel> movies = MoviesLogic.GetAllMovies();
        Dictionary<int, string> movieDictionary = new Dictionary<int, string>();
        foreach (MoviesModel movie in movies)
        {
            movieDictionary.Add(Convert.ToInt32(movie.Id), movie.Title);
        }
        return movieDictionary;
    }

    public static void TrackPopularity()
    {
        List<ReservationModel> allReservations = ReservationAccess.GetAllReservations(); //make a list of reservation models of all reservations in the database

        var groupedReservations = allReservations
            .GroupBy(reservation => reservation.ShowId)
            .ToList();  //group those reservations on the show id and make that into a list
                        //group key = show id, thats the KEY that groups them all together

        var genrePopularity = new Dictionary<string, int>(); //i use a dictionary to group genre and reservation amount together

        foreach (var group in groupedReservations)
        {
            ShowModel reservedShow = ShowAccess.GetByID(group.Key); // use the group key aka show id
            MoviesModel movie = MoviesAccess.GetByLongId(reservedShow.MovieId); //get the movie model from the show model to get to the genre

            if (movie != null)
            {
                string[] genres = movie.Genre.Split(','); // Split the genres by commas

                int seatsBooked = group.Count(); //count the reservations together for each group,(one group has 4 reservation == 4)

                //this used the dictionary, if a genre is already in the dictionary it adds the amount of seats booked to that existing genre
                //if the genre doesnt exist it makes it
                foreach (string genre in genres)
                {
                    string trimmedGenre = genre.Trim(); // Remove any leading/trailing spaces from the genre

                    if (genrePopularity.ContainsKey(trimmedGenre))
                    {
                        genrePopularity[trimmedGenre] += seatsBooked;
                    }
                    else
                    {
                        genrePopularity[trimmedGenre] = seatsBooked;
                    }
                }
            }
        }

        var sortedGenres = genrePopularity
            .OrderByDescending(g => g.Value)
            .ToList();  //sort the genre by value, value = amount seats booked, refers back to the dictionary
                        //(key is genre name value is amount of reservations)

        Console.WriteLine("Most popular genres descending on amount of reservations:");
        Console.WriteLine("-----------------------------------------------------------");

        int genreNumber = 1;
        int longestGenre = 0;

        //find the longest genre name for alignment
        foreach (var genre in sortedGenres)
        {
            if (genre.Key.Length > longestGenre)
            {
                longestGenre = genre.Key.Length; //get longest length word
            }
        }
        longestGenre += 5; //+5 for alignment


        foreach (var genre in sortedGenres)
        {
            string genreName = genre.Key;
            string spaces = "";  //create empty string to hold the spaces

            //calculate how many spaces are needed
            int spacesNeeded = longestGenre - genreName.Length;
            for (int i = 0; i < spacesNeeded; i++)
            {
                spaces += " ";  //add space one by one
            }

            string seatsBooked = $"{genre.Value} seats booked";
            Console.WriteLine($"{genreName}{spaces}{seatsBooked}");
            genreNumber++;
        }

        Console.WriteLine("\n[1] Go back to movie menu");
        Console.WriteLine("[2] Exit to admin menu");

        while (true)
        {
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                Start();
                return;
            }
            else if (choice == "2")
            {
                Console.Clear();
                return;
            }
            else
            {
                Console.WriteLine("Invalid option. Please enter 1 or 2.");
            }
        }
    }


}