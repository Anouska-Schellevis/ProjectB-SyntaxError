public class Show
{
    public static string dateofshow;
    static public void AdminStart(UserModel acc)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Show menu =====\n");
            Console.WriteLine("[1]Overview of all show");
            Console.WriteLine("[2]Add show");
            Console.WriteLine("[3]Edit show");
            Console.WriteLine("[4]Delete show");
            Console.WriteLine("[5]Go back to the menu");
            Console.WriteLine("What would you like to do?");
            bool isNum = int.TryParse(Console.ReadLine(), out int choice);
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number.");
                Thread.Sleep(2000);
                AdminStart(acc);
            }

            int idToDelete;

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    ShowPrint();

                    Console.WriteLine("\n[1]Go back to show menu");
                    Console.WriteLine("[2]Exit to admin menu");

                    while (true)
                    {
                        string menuChoice = Console.ReadLine();
                        if (menuChoice == "1")
                        {
                            Console.Clear();
                            AdminStart(acc);
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
                    ShowAdd();
                    Console.WriteLine("Show is added!");
                    Thread.Sleep(2000);
                    break;
                case 3:
                    int ID_to_edit;
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter the id to edit:");
                        bool idIsNum = int.TryParse(Console.ReadLine(), out ID_to_edit);
                        if (!idIsNum)
                        {
                            Console.WriteLine("Invalid input. Must be a number.");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            break;
                        }
                    }

                    ShowModel show = ShowLogic.GetByID(ID_to_edit);
                    if (show != null)
                    {
                        show = ShowEdit(show);
                        ShowLogic.UpdateShow(show);
                        Console.WriteLine("Show is saved!");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.WriteLine("This show does not exist.");
                    }
                    break;
                case 4:
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Enter the id of the show you want to delete:");
                        bool idIsNum = int.TryParse(Console.ReadLine(), out idToDelete);
                        if (!idIsNum)
                        {
                            Console.WriteLine("Invalid input. Must be a number.");
                            Thread.Sleep(2000);
                        }
                        else if (ShowAccess.GetByID(idToDelete) == null)
                        {
                            Console.WriteLine("This ID does not exist. Try again.");
                            Thread.Sleep(2000);
                        }
                    } while (ShowAccess.GetByID(idToDelete) == null);

                    Console.WriteLine($"Are you sure you want to delete this show?");
                    Console.WriteLine("[1]Yes\n[2]No");
                    string question = Console.ReadLine();
                    Console.Clear();
                    while (question != "1" && question != "2")
                    {
                        Console.WriteLine("Invalid input. Try again.");
                        Console.WriteLine($"Are you sure you want to delete this show?");
                        Console.WriteLine("[1]Yes\n[2]No");
                        question = Console.ReadLine();
                        Console.Clear();
                    }
                    if (question == "1")
                    {
                        ShowDelete(idToDelete);
                        Console.WriteLine("Show is deleted!");
                        Thread.Sleep(2000);
                    }
                    break;
                case 5:
                    Console.Clear();
                    Admin.Start(acc);
                    break;
                default:
                    Console.WriteLine("Invalid input. This option doesn't exist.");
                    Thread.Sleep(2000);
                    AdminStart(acc);
                    break;
            }
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
                    Console.WriteLine($"[{movie_count.Key}]{movie_name}");
                }
                do
                {
                    bool isNum = int.TryParse(Console.ReadLine(), out chosennumber);
                    if (!isNum)
                    {
                        Console.WriteLine("Invalid input. Must be a number.");
                        Thread.Sleep(2000);

                    }
                    else if (movies.ContainsKey(chosennumber))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not a valid choice. Try again.");
                        Thread.Sleep(2000);
                    }

                    /*
                    The cursor goes back to the position of the error message, which is replaced after 2 seconds with an empty string.
                    */
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                    
                } while (true);
                Console.Clear();
                string chosenmovie = movies[chosennumber];
                movie = Movie.MovieSearch(chosenmovie);
                do
                {
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("[1]Get movie info");
                    Console.WriteLine("[2]Choose time");
                    Console.WriteLine("[3]Go back to week overview");
                    Console.WriteLine("[4]Go back to day overview");
                    Console.WriteLine("[5]Go back to the menu");

                    bool isNum = int.TryParse(Console.ReadLine(), out choice);
                    if (!isNum)
                    {
                        Console.WriteLine("Invalid input. Must be a number.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else if (choice == 1 || choice == 2 || choice == 3 || choice == 5)
                    {
                        loop = false;
                    }
                    else if (choice == 4)
                    {
                        Console.WriteLine("Returning to day overview...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                        Thread.Sleep(2000);
                        Console.Clear();
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
                Console.WriteLine("There is no movie on this day.");
                printed = false;
            }
        }
        switch (choice)
        {
            case 1:

                int secondchoice;
                do
                {
                    Console.WriteLine(movie);
                    Console.WriteLine("\nWhat would you like to do?");
                    Console.WriteLine("[1]Choose time");
                    Console.WriteLine("[2]Go back to week overview");
                    bool isNum = int.TryParse(Console.ReadLine(), out secondchoice);
                    if (!isNum)
                    {
                        Console.WriteLine("Invalid input. Must be a number.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else if (secondchoice != 1 && secondchoice != 2)
                    {
                        Console.WriteLine("Invalid input try again.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                } while (secondchoice != 1 && secondchoice != 2);
                Console.Clear();
                switch (secondchoice)
                {
                    case 1:
                        if (movie != null)
                        {
                            Dictionary<int, ShowModel> showtime = TimeOptions(movies[chosennumber], dateofshow);
                            int chosentime;
                            do
                            {
                                Console.WriteLine("And at what time?");
                                foreach (var datetime in showtime)
                                {
                                    string time = datetime.Value.Date.Split(' ')[1];
                                    Console.WriteLine($"[{datetime.Key}]{time}");
                                }
                                bool isNum = int.TryParse(Console.ReadLine(), out chosentime);
                                Console.Clear();
                                if (!isNum)
                                {
                                    Console.WriteLine("Invalid input. Must be a number.");
                                    Thread.Sleep(2000);
                                }
                                else if (showtime.ContainsKey(chosentime))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Not a valid choice. Try again.");
                                    Thread.Sleep(2000);
                                }
                            } while (true);

                            ShowModel ChosenShow = showtime[chosentime];
                            if (ChosenShow.TheatreId == 1)
                            {
                                ConcreteTheater theater150 = (ConcreteTheater)Theater.GetTheater(150);
                                if (theater150 != null)
                                {
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
                    Dictionary<int, ShowModel> showtime = TimeOptions(movies[chosennumber], dateofshow);
                    int chosentime;
                    do
                    {
                        Console.WriteLine("And at what time?");
                        foreach (var datetime in showtime)
                        {
                            string time = datetime.Value.Date.Split(' ')[1];
                            Console.WriteLine($"[{datetime.Key}]{time}");
                        }
                        bool isNum = int.TryParse(Console.ReadLine(), out chosentime);
                        Console.Clear();
                        if (!isNum)
                        {
                            Console.WriteLine("Invalid input. Must be a number.");
                            Thread.Sleep(2000);
                        }
                        else if (showtime.ContainsKey(chosentime))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Not a valid choice. Try again.");
                            Thread.Sleep(2000);
                        }
                    }

                    while (true);
                    ShowModel ChosenShow = showtime[chosentime];
                    if (ChosenShow.TheatreId == 1)
                    {
                        ConcreteTheater theater150 = (ConcreteTheater)Theater.GetTheater(150);
                        if (theater150 != null)
                        {
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
                Console.Clear();
                UserStart(acc);
                break;
            case 5:
                Console.Clear();
                User.Start(acc);
                break;
            default:
                Console.WriteLine("Unexpected choice.");
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

        int question1;
        do
        {
            Console.WriteLine("Would you like to change the theater number?");
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
            do
            {
                Console.WriteLine("Theatres you can choose: \n1 [Imax, 150 seats]\n2 [2D, 300 seats]\n3 [2D, 500 seats]");
                Console.WriteLine("\nEnter new theater number for this movie:");
                bool isNum = int.TryParse(Console.ReadLine(), out newTheatreId);
                Console.Clear();
                if (!isNum)
                {
                    Console.WriteLine("Invalid input. Must be a number.");
                }
                else if (newTheatreId != 1 && newTheatreId != 2 && newTheatreId != 3)
                {
                    Console.WriteLine("Theater number does not exist. Try again.");
                }
            } while (newTheatreId != 1 && newTheatreId != 2 && newTheatreId != 3);
        }

        int question2;
        do
        {
            Console.WriteLine("Would you like to change the movie title?");
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
            List<MoviesModel> movies = MoviesLogic.GetAllMovies();
            Console.WriteLine("Movies you can choose from: ");
            foreach (var item in movies)
            {
                Console.WriteLine($"- {item.Title}");
            }
            Console.WriteLine("Enter movie title (not uppercase sensitive):");
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
                    movies = MoviesLogic.GetAllMovies();
                    Console.WriteLine("\nMovies you can choose from: ");
                    foreach (var item in movies)
                    {
                        Console.WriteLine($"- {item.Title}");
                    }
                    Console.WriteLine("\nEnter movie title (not uppercase sensitive):");
                }
            } while (movie == null);
            newMovieId = Convert.ToInt32(movie.Id);
        }

        int question;
        do
        {
            Console.WriteLine("Would you like to change the date/time?");
            Console.WriteLine("[1]Yes\n[2]No");
            bool isNum = int.TryParse(Console.ReadLine(), out question);
            Console.Clear();
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number.");
            }
            else if (question != 1 && question != 2)
            {
                Console.WriteLine("Invalid option. Please enter 1 or 2.");
            }
        }
        while (question != 1 && question != 2);
        if (question == 1)
        {
            bool backtodate = true;
            while (backtodate == true)
            {
                do
                {
                    Console.WriteLine("Enter date for this show in 'YYYY-MM-DD' format (Example: 2025-01-24, 2025-02-01):");
                    Date = Console.ReadLine();

                    Console.Clear();
                    if (!DateTime.TryParseExact(Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                    {
                        Console.WriteLine("Not a valid date time format. Try again.");
                        Console.WriteLine("Enter date for this show in 'YYYY-MM-DD' format (Example: 2025-01-24, 2025-02-01):");
                        Date = Console.ReadLine();
                    }
                    else if (parsedDate < DateTime.Now.Date)
                    {
                        Console.WriteLine("This date is in the past. Try again.");
                        Console.WriteLine("Enter date for this show in 'YYYY-MM-DD' format (Example: 2025-01-24, 2025-02-01):");
                        Date = Console.ReadLine();
                    }
                } while (DateOnly.TryParse(Date, out _) != true || DateTime.TryParse(Date, out _) != true);

                do
                {
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
                                        Console.WriteLine("[1]Yes\n [2]No");
                                        int yesno = Convert.ToInt32(Console.ReadLine());
                                        Console.Clear();
                                        if (yesno == 1)
                                        {
                                            Console.WriteLine("You can choose one of the following times: ");
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
                                            Console.WriteLine("You can choose another day then.");
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
                                        Console.WriteLine("[1]Yes\n [2]No");
                                        int yesno = Convert.ToInt32(Console.ReadLine());
                                        if (yesno == 1)
                                        {
                                            Console.WriteLine("You can choose one of the following times: ");
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
                                            Console.WriteLine("You can choose another day then.");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("The cinema is not opened at this time.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not a valid time.");
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
        bool isNum = false;
        do
        {
            Console.WriteLine("Theatres you can choose: \n1 [Imax, 150 seats]\n2 [2D, 300 seats]\n3 [2D, 500 seats]");
            Console.WriteLine("\nEnter the theater number for this movie: ");

            isNum = int.TryParse(Console.ReadLine(), out newTheaterId);
            Console.Clear();
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number.");
                continue;
            }
            else if (newTheaterId != 1 && newTheaterId != 2 && newTheaterId != 3)
            {
                Console.WriteLine("Theater number does not exist. Try again.");
            }
        } while (newTheaterId != 1 && newTheaterId != 2 && newTheaterId != 3);

        string title = "";
        MoviesModel movie;
        List<MoviesModel> movies = MoviesLogic.GetAllMovies();
        Console.WriteLine("Movies you can choose from: ");
        foreach (var item in movies)
        {
            Console.WriteLine($"- {item.Title}");
        }
        Console.WriteLine("\nEnter movie name(not uppercase sensitive):");
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
                Console.WriteLine("\nMovies you can choose from: ");
                foreach (var item in movies)
                {
                    Console.WriteLine($"- {item.Title}");
                }
                Console.WriteLine("\nEnter the title to edit(not uppercase sensitive)");
            }
        } while (movie == null);
        newMovieId = Convert.ToInt32(movie.Id);

        bool backtodate = true;
        while (backtodate == true)
        {
            do
            {
                Console.WriteLine("Enter date for this show in 'YYYY-MM-DD' format (Example: 2025-01-24, 2025-02-01):");
                Date = Console.ReadLine();

                Console.Clear();
                if (!DateTime.TryParseExact(Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    Console.WriteLine("Not a valid date time format. Try again.");
                    Console.WriteLine("Enter date for this show in 'YYYY-MM-DD' format (Example: 2025-01-24, 2025-02-01):");
                    Date = Console.ReadLine();
                }
                else if (parsedDate < DateTime.Now.Date)
                {
                    Console.WriteLine("This date is in the past. Try again.");
                    Console.WriteLine("Enter date for this show in 'YYYY-MM-DD' format (Example: 2025-01-24, 2025-02-01):");
                    Date = Console.ReadLine();
                }
            } while (DateOnly.TryParse(Date, out _) != true || DateTime.TryParse(Date, out _) != true);

            Console.Clear();
            do
            {
                PrintShowsInTheaterThisDay(Date, newTheaterId);
                Console.WriteLine("What time would you like to choose ('HH:MM' format)?");
                time = Console.ReadLine();
                int timeinminutes = Convert.ToInt32(MoviesLogic.GetById(newMovieId).TimeInMinutes);
                if (TimeOnly.TryParse(time, out _) == true && TimeSpan.TryParse(time, out _) == true)
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
                                int yesno;
                                bool isNumYesNo;
                                do
                                {
                                    Console.WriteLine("Would you like to pick one of the suggested times for this day?");
                                    Console.WriteLine("[1]Yes\n[2]No");
                                    isNumYesNo = int.TryParse(Console.ReadLine(), out yesno);
                                    Console.Clear();
                                    if (!isNumYesNo)
                                    {
                                        Console.WriteLine("Invalid input. Must be a number.");
                                    }
                                    else if (yesno != 1 && yesno != 2)
                                    {
                                        Console.WriteLine("Invalid option. Please enter 1 or 2.");
                                    }
                                }
                                while (yesno != 1 && yesno != 2);

                                if (yesno == 1)
                                {
                                    Console.WriteLine("You can choose one of the following times:");
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
                                    Console.WriteLine("You can choose another day then.");
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The cinema is not opened at this time.");
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid time.");
                }
            } while (backtodate == false);
        }
        newDate_time = $"{Date} {time}";
        ShowModel new_show = new ShowModel(1, newTheaterId, newMovieId, newDate_time);
        ShowLogic.WriteShow(new_show);
        int answer = 0;
        do
        {
            Console.WriteLine("Would you also like to plan this show at the same time for the next 6 days?");
            Console.WriteLine("[1]Yes\n[2]No");
            bool isNumYesNo = int.TryParse(Console.ReadLine(), out answer);
            Console.Clear();
            if (!isNumYesNo)
            {
                Console.WriteLine("Invalid input. Must be a number.");
            }
            else if (answer != 1 && answer != 2)
            {
                Console.WriteLine("Invalid input try again.");
            }
        } while (answer != 1 && answer != 2);
        if (answer == 1)
        {
            PlanForAWholeWeek(new_show);
        }
    }

    static void ShowPrint()
    {
        List<ShowModel> shows = ShowLogic.GetAllShows();
        foreach (ShowModel show in shows)
        {
            Console.WriteLine($"Theater: {show.TheatreId}");
            string movietitle = MoviesLogic.GetById(Convert.ToInt32(show.MovieId)).Title;
            Console.WriteLine($"Movie title: {movietitle}");
            Console.WriteLine($"Date: {show.Date}");
            Console.WriteLine("-----------------------------------------------");
        }
    }
    public static Dictionary<int, string> PrintOverviewMovie_Time()
    {
        Console.WriteLine("On what day would you like to watch a movie?");
        DayOfWeek Currentday = DateTime.Now.DayOfWeek;
        int currentDay = (int)Currentday;
        int thursDay = (int)DayOfWeek.Thursday;
        int daysTilNextThursday = (thursDay - currentDay + 7) % 7;

        if (daysTilNextThursday == 0)
        {
            daysTilNextThursday = 7;
        }
        if (currentDay == 2)
        {
            daysTilNextThursday += 7;
        }
        if (currentDay == 3)
        {
            daysTilNextThursday += 6;
        }
        int numbernottoprint = 0;
        int lastnumber = 0;
        int minus = 0;
        int count = 0;
        List<string> dateThatDoesWork = new List<string>();
        List<ShowModel> listshowdate;

        for (int i = 0; i <= daysTilNextThursday; i++)
        {
            DayOfWeek printedday = DateTime.Now.AddDays(i).DayOfWeek;
            string printeddate = DateTime.Now.AddDays(i).Date.ToString("yyyy-MM-dd");
            listshowdate = ShowLogic.AllOrderedByDate(printeddate);
            foreach (var item in listshowdate)
            {
                DateTime timeofday = DateTime.Now;
                DateTime timeofshow = DateTime.Parse(item.Date);
                if (timeofday > timeofshow)
                {
                    numbernottoprint++;
                }
            }
            if (listshowdate.Count == 0 || numbernottoprint == listshowdate.Count)
            {
                minus++;
            }
            else
            {
                Console.WriteLine($"[{i + 1 - minus}]{printedday} {printeddate}");
                count++;
                dateThatDoesWork.Add(printeddate);
            }
            lastnumber = i + 1 - minus;
            numbernottoprint = 0;
        }

        int Day;
        do
        {
            bool isNum = int.TryParse(Console.ReadLine(), out Day);
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number.");
                Thread.Sleep(2000);
            }
            else if (Day <= 0 || Day > count)
            {
                Console.WriteLine("Invalid Input. Try again.");
                Thread.Sleep(2000);
            }
            /*
            The cursor goes back to the position of the error message, which is replaced after 2 seconds with an empty string.
            */
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.SetCursorPosition(0, Console.CursorTop + 1);
        } while (Day <= 0 || Day > count);
        Console.Clear();
        string DayToPrint = Convert.ToString(DateTime.Parse(dateThatDoesWork[Day - 1]).DayOfWeek);
        string date = dateThatDoesWork[Day - 1];

        Dictionary<int, string> movies = Movie.MakeMovieDict();
        List<ShowModel> shows = ShowLogic.GetAllShows();

        List<ShowModel> ShowsOnDay = new List<ShowModel>();
        Dictionary<int, string> MovieCanWatch = new Dictionary<int, string>();

        int moviecount = 0;

        foreach (var show in shows)
        {
            string showdate = show.Date;
            if (showdate.Contains(date))
            {
                ShowsOnDay.Add(show);
            }
        }
        Console.WriteLine($"{DayToPrint} {date}");
        foreach (var movie in movies)
        {
            bool moviePrinted = false;
            string showTimes = "";
            foreach (var show in ShowsOnDay)
            {
                DateTime DateAndTime = DateTime.Parse(show.Date);
                if (movie.Key == show.MovieId)
                {
                    if (moviePrinted == false)
                    {
                        Console.WriteLine("Movie title: " + movie.Value);
                        Console.WriteLine("Available show times:");
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
            if (showTimes != "")
            {
                Console.WriteLine(showTimes);
                Console.WriteLine("-----------------------------------");
            }
        }
        dateofshow = date;
        return MovieCanWatch;
    }

    public static Dictionary<int, ShowModel> TimeOptions(string movie_name, string Datefortime)
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
        DateTime CurrentDate = DateTime.Now;
        DateTime DDay = DateTime.Parse(Datefortime); ;
        foreach (var show in shows)
        {
            string show_date = show.Date.Split(" ")[0];
            DateTime showdate = DateTime.Parse(show.Date);
            if (show_date == Datefortime)
            {
                if (showdate > CurrentDate)
                {
                    ShowTime.Add(count, show);
                    count++;
                }
            }
        }
        return ShowTime;
    }

    public static void WeekOverviewMovies()
    {
        Dictionary<int, string> movies = Movie.MakeMovieDict();
        List<ShowModel> shows = ShowLogic.GetAllShows();

        int currentDay = (int)DateTime.Now.DayOfWeek;
        int thursDay = (int)DayOfWeek.Thursday;

        int daysTilNextThursday = (thursDay - currentDay + 7) % 7;

        if (daysTilNextThursday == 0)
        {
            daysTilNextThursday = 7;
        }

        if (currentDay == 2)
        {
            daysTilNextThursday += 7;
        }

        if (currentDay == 3)
        {
            daysTilNextThursday += 6;
        }

        for (int i = 0; i <= daysTilNextThursday; i++)
        {
            bool printed = false;

            DateTime CurrentDate = DateTime.Now; // datetime at this date and time
            DateTime Datetoprint = CurrentDate.AddDays(i);
            DayOfWeek CurrentDay = CurrentDate.DayOfWeek;
            DayOfWeek DayToPrint = (DayOfWeek)(((int)CurrentDay + i) % 7);
            string StringCurrentDate = Convert.ToString(Datetoprint).Split(" ")[0];
            Console.WriteLine($"{DayToPrint} {StringCurrentDate}");
            Console.WriteLine("________________________________________");
            Dictionary<string, string> movieTimes = new Dictionary<string, string>();
            foreach (var show in shows)
            {
                DateTime DateAndTime = DateTime.Parse(show.Date); //datetime of show.date

                string StringDate = Convert.ToString(DateAndTime).Split(" ")[0]; // just the date of show.date in string form

                string StringTime = show.Date.Split(" ")[1]; ; // just the time of show.date in string form
                string StringCurrentTime = Convert.ToString(CurrentDate).Split(" ")[1]; // just the time of the current date in string form 

                if (CurrentDate < DateAndTime)
                {
                    if (StringCurrentDate == StringDate)
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
        bool printed = false;
        Dictionary<string, string> movieandtimes = new Dictionary<string, string>();
        foreach (var show in shows)
        {
            string showdate = show.Date.Split(" ")[0];
            if (showdate == date)
            {
                if (show.TheatreId == theater)
                {
                    if (printed == false)
                    {
                        Console.WriteLine("On this date, in this theater the following movies play:");
                        Console.WriteLine("(The times include cleaning time)");
                        printed = true;
                    }
                    MoviesModel movie = MoviesLogic.GetById(Convert.ToInt32(show.MovieId));
                    string time = show.Date.Split(" ")[1];
                    int minutes = Convert.ToInt32(movie.TimeInMinutes);
                    string endtime = GetEndTime(time, minutes);
                    if (movieandtimes.ContainsKey(movie.Title))
                    {
                        movieandtimes[movie.Title] += $"\n{time} - {endtime}";
                    }
                    else
                    {
                        movieandtimes[movie.Title] = $"{time} - {endtime}";
                    }
                }
            }
        }
        if (movieandtimes.Count != 0)
        {
            foreach (var item in movieandtimes)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
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
        return doublebooked;
    }

    public static List<string> AnotherSpotToday(int minutesNewShow, string date)
    {
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

    public static void PlanForAWholeWeek(ShowModel showtostartwith)
    {
        for (int i = 1; i < 7; i++)
        {
            DateTime newshowdate = DateTime.Parse(showtostartwith.Date).AddDays(i);
            string newstringshowdate = newshowdate.ToString("yyyy-MM-dd HH:mm");
            MoviesModel movie = MoviesLogic.GetById(Convert.ToInt32(showtostartwith.MovieId));
            string time = showtostartwith.Date.Split(" ")[1];
            string endtime = GetEndTime(time, Convert.ToInt32(movie.TimeInMinutes));
            if (WithinOpeningHours(time, Convert.ToInt32(movie.TimeInMinutes)) && IsDoubleBooked(time, endtime, Convert.ToInt32(showtostartwith.TheatreId), newstringshowdate) == false)
            {
                ShowModel new_show = new ShowModel(1, showtostartwith.TheatreId, showtostartwith.MovieId, newstringshowdate);
                ShowLogic.WriteShow(new_show);
            }
        }
    }
}