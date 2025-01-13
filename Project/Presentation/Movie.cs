using Microsoft.VisualBasic;

class Movie
{

    // static public void Main()
    // {
    //     Start(acc);
    // }
    static public void Start(UserModel acc)
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine("[1]Overview of all movies");
            Console.WriteLine("[2]Add movie");
            Console.WriteLine("[3]Edit movie");
            Console.WriteLine("[4]Delete movie");
            Console.WriteLine("[5]Search movie by title");
            Console.WriteLine("[6]See most populair movie genre");
            Console.WriteLine("[7]Go back");
            Console.WriteLine("What would you like to do?");
            bool isNum = int.TryParse(Console.ReadLine(), out int choice);
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number");
                Thread.Sleep(2000);
                Start(acc);
            }

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    MoviePrint();

                    Console.WriteLine("\n[1]Go back to movie menu");
                    Console.WriteLine("[2]Exit to admin menu");

                    while (true)
                    {
                        string menuChoice = Console.ReadLine();
                        if (menuChoice == "1")
                        {
                            Console.Clear();
                            Start(acc);
                            return;
                        }
                        else if (menuChoice == "2")
                        {
                            Console.Clear();
                            Admin.Start(acc);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter 1 or 2.");
                            Thread.Sleep(2000);

                            /*
                            The cursor goes back to the position of the error message, which is replaced after 2 seconds with an empty string.
                            */
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.SetCursorPosition(0, Console.CursorTop + 1);
                        }
                    }
                case 2:
                    Console.Clear();
                    MovieAdd();
                    Console.WriteLine("Movie is added");
                    Thread.Sleep(2000);
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
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.WriteLine("Such a movie does not exist");
                        Thread.Sleep(2000);
                    }
                    break;
                case 4:
                    Console.Clear();
                    string titleToDelete;
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
                    Console.WriteLine($"Are you sure you want to delete {moviefordelete}");
                    Console.WriteLine("[1]Yes\n[2]No");
                    string question = Console.ReadLine();
                    Console.Clear();
                    while (question != "1" && question != "2")
                    {
                        Console.WriteLine("Invalid input. Try again");
                        Console.WriteLine($"Are you sure you want to delete {moviefordelete}?");
                        Console.WriteLine("1. yes\n2. no");
                        question = Console.ReadLine();
                        Console.Clear();
                    }
                    if (question == "1")
                    {
                        MovieDelete(Convert.ToInt32(moviefordelete.Id));
                        Console.WriteLine("Movie is deleted");
                        Thread.Sleep(2000);
                    }
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

                    Console.WriteLine("\n[1]Go back to movie menu");
                    Console.WriteLine("[2]Exit to admin menu");

                    while (true)
                    {
                        string menuChoice = Console.ReadLine();
                        if (menuChoice == "1")
                        {
                            Console.Clear();
                            Start(acc);
                            return;
                        }
                        else if (menuChoice == "2")
                        {
                            Console.Clear();
                            Admin.Start(acc);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter 1 or 2.");
                            Thread.Sleep(2000);

                            /*
                            The cursor goes back to the position of the error message, which is replaced after 2 seconds with an empty string.
                            */
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.SetCursorPosition(0, Console.CursorTop + 1);
                        }
                    }
                case 6:
                    Console.Clear();
                    TrackPopularity(acc);
                    
                    Console.WriteLine("\n[1]Go back to movie menu");
                    Console.WriteLine("[2]Exit to admin menu");

                    while (true)
                    {
                        string menuChoice = Console.ReadLine();
                        if (menuChoice == "1")
                        {
                            Console.Clear();
                            Start(acc);
                            return;
                        }
                        else if (menuChoice == "2")
                        {
                            Console.Clear();
                            Admin.Start(acc);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter 1 or 2.");
                            Thread.Sleep(2000);

                            /*
                            The cursor goes back to the position of the error message, which is replaced after 2 seconds with an empty string.
                            */
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.SetCursorPosition(0, Console.CursorTop + 1);
                        }
                    }
                case 7:
                    Console.Clear();
                    Admin.Start(acc);
                    break;
                default:
                    Console.WriteLine("Invalid input. This option doesn't exist");
                    Thread.Sleep(2000);
                    Start(acc);
                    break;
            }
        }
    }

    static public MoviesModel MovieEdit(MoviesModel movie)
    {
        string stringnewTimeInMinutes = "";
        int newTimeInMinutes = 0;
        string newGenre = "";
        string newDescription = "";
        string newTitle = "";
        string newDirector = "";
        string newReleaseDate = "";


        Console.Clear();

        int question4;
        do 
        {
            Console.WriteLine("Would you like to change the title?");
            Console.WriteLine("[1]Yes\n[2]No");
            bool isNum = int.TryParse(Console.ReadLine(), out question4);
            Console.Clear();
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number.");
            }
            else if (question4 != 1 && question4 != 2)
            {
                Console.WriteLine("Invalid option. Please enter 1 or 2.");
            }
        }
        while (question4 != 1 && question4 != 2);
        
        if (question4 == 1)
        {
            Console.WriteLine("Enter a new title for this movie.");
            newTitle = Console.ReadLine();
        }
      
        int question1;
        do 
        {
            Console.WriteLine("Would you like to change the duration in minutes?");
            Console.WriteLine("[1]Yes\n[2]No");
            bool isNum = int.TryParse(Console.ReadLine(), out question1);
            Console.Clear();
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number.");
            }
            else if (question1 != 1 && question1 != 2)
            {
                Console.WriteLine("Invalid option. Please enter 1 or 2.");
            }
        }
        while (question1 != 1 && question1 != 2);

        if (question1 == 1)
        {
            bool isNum;
            do
            {
                Console.WriteLine("Enter a new duration in minutes for this movie.");
                isNum = int.TryParse(Console.ReadLine(), out newTimeInMinutes);
                Console.Clear();
                if (!isNum)
                {
                    Console.WriteLine("Not a valid duration format. Try again.");
                }
                else if (newTimeInMinutes <= 0)
                {
                    Console.WriteLine("Please try again. A movie must be longer than 0 minutes");
                }
            } while (!isNum || newTimeInMinutes <= 0);
        }
        int question2;
        do
        {
            Console.WriteLine("Would you like to change the genre?");
            Console.WriteLine("[1]Yes\n[2]No");
            bool isNum = int.TryParse(Console.ReadLine(), out question2);
            Console.Clear();
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number.");
            }
            else if (question2 != 1 && question2 != 2)
            {
                Console.WriteLine("Invalid option. Please enter 1 or 2.");
            }
        }
        while (question2 != 1 && question2 != 2);

        if (question2 == 1)
        {
            Console.WriteLine("Enter a new genre for this movie.");
            newGenre = Console.ReadLine();
            if (newGenre.Contains(" "))
            {
                string[] words = newGenre.Split(" ");
                newGenre = "";
                foreach (string word in words)
                {
                    string newword = char.ToUpper(word[0]) + word.Substring(1);
                    newGenre += newword;
                    newGenre += " ";
                }
                newGenre = newGenre.Trim();
            }
            else
            {
                newGenre = char.ToUpper(newGenre[0]) + newGenre.Substring(1);
                newGenre.Trim();
            }
        }

        Console.Clear();

        int question3;
        do 
        {
            Console.WriteLine("Would you like to change the description?");
            Console.WriteLine("[1]Yes\n[2]No");
            bool isNum = int.TryParse(Console.ReadLine(), out question3);
            Console.Clear();
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number.");
            }
            else if (question3 != 1 && question3 != 2)
            {
                Console.WriteLine("Invalid option. Please enter 1 or 2.");
            }
        }
        while (question3 != 1 && question3 != 2);

        if (question3 == 1)
        {
            Console.WriteLine("Enter a new description for this movie.");
            newDescription = Console.ReadLine();
        }
        
        Console.Clear();

        int question5;
        do 
        {
            Console.WriteLine("Would you like to change the director?");

            Console.WriteLine("[1]Yes\n[2]No");
            bool isNum = int.TryParse(Console.ReadLine(), out question5);
            Console.Clear();
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number.");
            }
            else if (question5 != 1 && question5 != 2)
            {
                Console.WriteLine("Invalid option. Please enter 1 or 2.");
            }
        }
        while (question5 != 1 && question5 != 2);
        
        if (question5 == 1)
        {
            Console.WriteLine("Enter new director for this movie.");
            newDirector = Console.ReadLine();
        }

        Console.Clear();

        int question6;
        do 
        {
            Console.WriteLine("Would you like to change the release date?");
            Console.WriteLine("[1]Yes\n[2]No");
            bool isNum = int.TryParse(Console.ReadLine(), out question6);
            Console.Clear();
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number.");
            }
            else if (question6 != 1 && question6 != 2)
            {
                Console.WriteLine("Invalid option. Please enter 1 or 2.");
            }
        }
        while (question6 != 1 && question6 != 2);
        
        if (question6 == 1)
        {
            do
            {
                Console.WriteLine("Enter new release_date for this movie. 'YYYY-MM-DD' format. (Example: 2024-12-11, 2025-01-01)");
                newReleaseDate = Console.ReadLine();
                Console.Clear();
                if (DateTime.TryParse(newReleaseDate, out _) == false)
                {
                    Console.WriteLine("Not a valid date time format. Try again.");
                }
            } while (DateTime.TryParse(newReleaseDate, out _) != true);
            newReleaseDate = DateTime.Parse(newReleaseDate).ToString("yyyy-MM-dd");
        }

        if (question1 == 1)
        {
            movie.TimeInMinutes = Convert.ToInt32(newTimeInMinutes);
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


        Console.WriteLine("Enter new title for this movie.");
        string newTitle = Console.ReadLine();
        if (newTitle.Contains(" "))
        {
            string[] words = newTitle.Split(" ");
            newTitle = "";
            foreach (string word in words)
            {
                string newword = char.ToUpper(word[0]) + word.Substring(1);
                newTitle += newword;
                newTitle += " ";
            }
            newTitle = newTitle.Trim();
        }
        else
        {
            newTitle = char.ToUpper(newTitle[0]) + newTitle.Substring(1);
            newTitle.Trim();
        }
      
        Console.WriteLine("\nEnter a new description for this movie.");
        string newDescription = Console.ReadLine();
      
        bool isNum = false;
        int newTimeInMinutes;
        do
        {
            Console.WriteLine("Enter a new duration in minutes for this movie.");
            isNum = int.TryParse(Console.ReadLine(), out newTimeInMinutes);
            Console.Clear();
            if (!isNum)
            {
                Console.WriteLine("Not a valid duration format. Try again.");
            }
            else if (newTimeInMinutes <= 0)
            {
                Console.WriteLine("Please try again. A movie must be longer than 0 minutes");
            }
        } while (!isNum || newTimeInMinutes <= 0);
      
        Console.WriteLine("Enter new genre for this movie.");
        string newGenre = Console.ReadLine();
        if (newGenre.Contains(" "))
        {
            string[] words = newGenre.Split(" ");
            newGenre = "";
            foreach (string word in words)
            {
                string newword = char.ToUpper(word[0]) + word.Substring(1);
                newGenre += newword;
                newGenre += " ";
            }
            newGenre = newGenre.Trim();
        }
        else
        {
            newGenre = char.ToUpper(newGenre[0]) + newGenre.Substring(1);
            newGenre.Trim();
        }
        
        Console.WriteLine("\nEnter a new director for this movie.");
        string newDirector = Console.ReadLine();
      
        do
        {
            Console.WriteLine("\nEnter a new release_date for this movie. 'YYYY-MM-DD' format. (Example: 2024-12-11, 2025-01-01)");
            newReleaseDate = Console.ReadLine();
            Console.Clear();
            if (DateTime.TryParse(newReleaseDate, out _) == false)
            {
                Console.WriteLine("Not a valid date time format. Try again.");
            }
        } while (DateTime.TryParse(newReleaseDate, out _) != true);
        newReleaseDate = DateTime.Parse(newReleaseDate).ToString("yyyy-MM-dd");
        int TimeInMinutes = Convert.ToInt32(newTimeInMinutes);
        MoviesModel new_movie = new MoviesModel(1, TimeInMinutes, newGenre, newDescription, newTitle, newDirector, newReleaseDate);
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
    }


}