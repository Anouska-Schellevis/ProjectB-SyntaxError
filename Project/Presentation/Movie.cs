using Microsoft.VisualBasic;

class Movie
{

    // static public void Main()
    // {
    //     Start(acc);
    // }
    static public void Start(UserModel acc)
    {
        Console.WriteLine("[1] Overview of all movies");
        Console.WriteLine("[2] Add Movie");
        Console.WriteLine("[3] Edit Movie");
        Console.WriteLine("[4] Delete Movie");
        Console.WriteLine("[5] Search Movie by title");
        Console.WriteLine("[6] See most populair movie genre");
        Console.WriteLine("[7] Go back to Start Menu");
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
                List<MoviesModel> movies = MoviesLogic.GetAllMovies();
                Console.WriteLine("Movies you can choose from: ");
                foreach (var item in movies)
                {
                    Console.WriteLine($"- {item.Title}");
                }
                Console.WriteLine("Enter the title to edit(not uppercase sensitive)");

                string Title_to_edit;
                MoviesModel movieforedit;
                do
                {
                    Title_to_edit = Console.ReadLine();
                    Console.Clear();
                    if (Title_to_edit.Contains(" "))
                    {
                        string[] words = Title_to_edit.Split(" ");
                        string newtitle = "";
                        foreach (string word in words)
                        {
                            string newword = char.ToUpper(word[0]) + word.Substring(1);
                            newtitle += newword;
                            newtitle += " ";
                        }
                        Title_to_edit = newtitle.Trim();
                    }
                    else
                    {
                        Title_to_edit = char.ToUpper(Title_to_edit[0]) + Title_to_edit.Substring(1);
                        Title_to_edit.Trim();
                    }
                    movieforedit = MoviesLogic.GetByTitle(Title_to_edit);
                    if (MoviesLogic.GetByTitle(Title_to_edit) == null)
                    {
                        Console.WriteLine("Invalid movie. Try again.");
                    }
                } while (movieforedit == null);
                
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
                string titleToDelete;
                // do
                // {
                //     Console.WriteLine("Enter the id of the movie you want to delete");
                //     idToDelete = Convert.ToInt32(Console.ReadLine());
                //     if (MoviesAccess.GetById(idToDelete) == null)
                //     {
                //         Console.WriteLine("This ID does not exist. Try again");
                //     }
                // } while (MoviesAccess.GetById(idToDelete) == null);
                Console.WriteLine("Enter the title of the movie you want to search for(not uppercase sensitive)");
                MoviesModel moviefordelete;
                do
                {
                    titleToDelete = Console.ReadLine();
                    Console.Clear();
                    if (titleToDelete.Contains(" "))
                    {
                        string[] words = titleToDelete.Split(" ");
                        string newtitle = "";
                        foreach (string word in words)
                        {
                            string newword = char.ToUpper(word[0]) + word.Substring(1);
                            newtitle += newword;
                            newtitle += " ";
                        }
                        titleToDelete = newtitle.Trim();
                    }
                    else
                    {
                        titleToDelete = char.ToUpper(titleToDelete[0]) + titleToDelete.Substring(1);
                        titleToDelete.Trim();
                    }
                    moviefordelete = MoviesLogic.GetByTitle(titleToDelete);
                    if (MoviesLogic.GetByTitle(titleToDelete) == null)
                    {
                        Console.WriteLine("Invalid movie. Try again.");
                    }
                } while (moviefordelete == null);
                MovieDelete(Convert.ToInt32(moviefordelete.Id));
                Console.WriteLine("Movie is deleted");
                break;
            case 5:
                Console.Clear();
                Console.WriteLine("Enter the title of the movie you want to search for(not uppercase sensitive)");
                string Title_to_search;
                MoviesModel movieforsearch;
                do
                {
                    Title_to_search = Console.ReadLine();
                    Console.Clear();
                    if (Title_to_search.Contains(" "))
                    {
                        string[] words = Title_to_search.Split(" ");
                        string newtitle = "";
                        foreach (string word in words)
                        {
                            string newword = char.ToUpper(word[0]) + word.Substring(1);
                            newtitle += newword;
                            newtitle += " ";
                        }
                        Title_to_search = newtitle.Trim();
                    }
                    else
                    {
                        Title_to_search = char.ToUpper(Title_to_search[0]) + Title_to_search.Substring(1);
                        Title_to_search.Trim();
                    }
                    movieforsearch = MoviesLogic.GetByTitle(Title_to_search);
                    if (MoviesLogic.GetByTitle(Title_to_search) == null)
                    {
                        Console.WriteLine("Invalid movie. Try again.");
                    }
                } while (movieforsearch == null);
                Console.WriteLine(MovieSearch(Title_to_search));
                break;
            case 6:
                Console.Clear();
                TrackPopularity(acc);
                break;
            case 7:
                Console.Clear();
                Admin.Start(acc);
                break;
            default:
                Console.WriteLine("No valid option selected. Please try again.");
                Start(acc);
                break;
        }
    }

    static public MoviesModel MovieEdit(MoviesModel movie)
    {
        int newTimeInMinutes = 0;
        string newGenre = "";
        string newDescription = "";
        string newTitle = "";
        string newDirector = "";
        string newReleaseDate = "";

        Console.WriteLine("Would you like to change the time in minutes?");
        Console.WriteLine("1. yes\n2. no");
        int question1 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        while (question1 != 1 && question1 != 2)
        {
            Console.WriteLine("Invalid input. Try again");
            Console.WriteLine("Would you like to change the time in minutes?");
            Console.WriteLine("1. yes\n2. no");
            question1 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
        }
        if (question1 == 1)
        {
            Console.WriteLine("Enter new time in minutes for this movie.");
            newTimeInMinutes = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("Would you like to change the genre?");
        Console.WriteLine("1. yes\n2. no");
        int question2 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        while (question2 != 1 && question2 != 2)
        {
            Console.WriteLine("Invalid input. Try again");
            Console.WriteLine("Would you like to change the genre?");
            Console.WriteLine("1. yes\n2. no");
            question2 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
        }
        if (question2 == 1)
        {
            Console.WriteLine("Enter new genre for this movie.");
            newGenre = Console.ReadLine();
        }

        Console.WriteLine("Would you like to change the description?");
        Console.WriteLine("1. yes\n2. no");
        int question3 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        while (question3 != 1 && question3 != 2)
        {
            Console.WriteLine("Invalid input. Try again");
            Console.WriteLine("Would you like to change the description?");
            Console.WriteLine("1. yes\n2. no");
            question3 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
        }
        if (question3 == 1)
        {
            Console.WriteLine("Enter new description for this movie.");
            newDescription = Console.ReadLine();
        }
        
        Console.WriteLine("Would you like to change the title?");
        Console.WriteLine("1. yes\n2. no");
        int question4 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        while (question4 != 1 && question4 != 2)
        {
            Console.WriteLine("Invalid input. Try again");
            Console.WriteLine("Would you like to change the title?");
            Console.WriteLine("1. yes\n2. no");
            question4 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
        }
        if (question4 == 1)
        {
            Console.WriteLine("Enter new title for this movie.");
            newTitle = Console.ReadLine();
        }
        
        Console.WriteLine("Would you like to change the director?");
        Console.WriteLine("1. yes\n2. no");
        int question5 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        while (question5 != 1 && question5 != 2)
        {
            Console.WriteLine("Invalid input. Try again");
            Console.WriteLine("Would you like to change the director?");
            Console.WriteLine("1. yes\n2. no");
            question5 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
        }
        if (question5 == 1)
        {
            Console.WriteLine("Enter new director for this movie.");
            newDirector = Console.ReadLine();
        }
        
        Console.WriteLine("Would you like to change the release date?");
        Console.WriteLine("1. yes\n2. no");
        int question6 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        while (question6 != 1 && question6 != 2)
        {
            Console.WriteLine("Invalid input. Try again");
            Console.WriteLine("Would you like to change the release date?");
            Console.WriteLine("1. yes\n2. no");
            question6 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
        }
        if (question6 == 1)
        {
            do
            {
                Console.WriteLine("Enter new release_date for this movie. '%Y-%M-%D' format. (Example: 2024-12-11, 2025-01-01)");
                newReleaseDate = Console.ReadLine();
                Console.Clear();
                if (DateTime.TryParse(newReleaseDate, out _) == false)
                {
                    Console.WriteLine("Not a valid date time format. Try again.");
                }
            } while (DateTime.TryParse(newReleaseDate, out _) != true);
        }
        
        if (question1 == 1)
        {
            movie.TimeInMinutes = newTimeInMinutes;
        }
        if (question2 == 1)
        {
            movie.Genre = newGenre;
        }
        if (question3 == 1)
        {
            movie.Description = newDescription;
        }
        if (question4 == 1)
        {
            movie.Title = newTitle;
        }
        if (question5 == 1)
        {
            movie.Director = newDirector;
        }
        if (question6 == 1)
        {
            movie.ReleaseDate = newReleaseDate;
        }

        return movie;
    }

    static public void MovieDelete(int id)
    {
        MoviesLogic.DeleteMovie(id);
    }

    static public void MovieAdd()
    {
        string newReleaseDate = "";

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
        do
        {
            Console.WriteLine("Enter new release_date for this movie. '%Y-%M-%D' format. (Example: 2024-12-11, 2025-01-01)");
            newReleaseDate = Console.ReadLine();
            Console.Clear();
            if (DateTime.TryParse(newReleaseDate, out _) == false)
            {
                Console.WriteLine("Not a valid date time format. Try again.");
            }
        } while (DateTime.TryParse(newReleaseDate, out _) != true);
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
            string info = $@"Title: {movie.Title}
Genre: {movie.Genre}
Director: {movie.Director}
Release Date: {movie.ReleaseDate}
Duration: {movie.TimeInMinutes} minutes
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

    public static void TrackPopularity(UserModel acc)
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
                Start(acc);
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