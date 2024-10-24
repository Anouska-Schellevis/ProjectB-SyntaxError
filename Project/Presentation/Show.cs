class Show
{

    static public void Main()
    {
        bool admin = false;
        if (admin)
        {AdminStart();}
        else
        {UserStart();}
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
        //ShowPrint();
        PrintOverviewMovie_Time();
        Console.WriteLine("What movie would you like to watch?");
        string Movie_to_watch = Console.ReadLine();
        MoviesModel movie = MoviesLogic.GetByTitle(Movie_to_watch);
        if (movie != null)
        {
            Console.WriteLine("And at what time? Enter in 'Year, month, day - hour, minute format");
            string Date_time = Console.ReadLine();
            int Movie_Id = Convert.ToInt32(movie.Id);

            List<ShowModel> shows = new List<ShowModel>(ShowAccess.GetByMovieID(Movie_Id));

            if (shows != null)
            {
                foreach (ShowModel show in shows)
                {
        
                    if (show.Date == Date_time)
                    {
<<<<<<< HEAD
                        Console.WriteLine(show.TheatreId);
                        if (show.Date == Date_time)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("There is no movie on this date and time.");
                            UserStart();
=======
                        if (show.TheatreId == 1)
                        {
                            Theater150 theater = new Theater150();
                            theater.SelectSeats();
>>>>>>> test
                        }
                        if (show.TheatreId == 2)
                        {
                            Theater300 theater2 = new Theater300();
                            theater2.SelectSeats();
                        }
                        if (show.TheatreId == 3)
                        {
                            Theater500 theater3 = new Theater500();
                            theater3.SelectSeats();
                        }

                    }
                    else
                    {
                        Console.WriteLine("There is no movie on this date and time.");
                        UserStart();
                    }
<<<<<<< HEAD
                }
                else
                {
                    Console.WriteLine("Such a time does not exist");
                    UserStart();
=======
>>>>>>> test
                }

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

<<<<<<< HEAD
        show.TheatreId = newTheaterId;
=======
        show.TheatreId = newTheatreId;
>>>>>>> test
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
        int newTheatreId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter movie ID for this movie.");
        int newMovieId = Convert.ToInt32(Console.ReadLine());
<<<<<<< HEAD
        Console.WriteLine("Enter date for this movie in '%Y-%m-%d %H:%M' format.");
=======
        Console.WriteLine("Enter release date for this movie.");
>>>>>>> test
        string newDate = Console.ReadLine();
        ShowModel new_show = new ShowModel(1, newTheatreId, newMovieId, newDate);
        ShowLogic.WriteShow(new_show);

    }

    static public void ShowPrint()
    {
        List<ShowModel> shows = ShowLogic.GetAllShows();
        foreach (ShowModel show in shows)
        {
            Console.WriteLine($"ID: {show.Id}");
            Console.WriteLine($"TheaterID: {show.TheatreId}");
<<<<<<< HEAD
=======
            Console.WriteLine($"TheaterID: {show.TheatreId}");
>>>>>>> test
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
<<<<<<< HEAD
            Console.WriteLine(movie.Value);
=======
            Console.WriteLine("movie title: " + movie.Value);
            Console.WriteLine("available show times:");
>>>>>>> test
            foreach (var show in shows)
            {
                //Console.WriteLine(show.MovieId)

                //Console.WriteLine("available show times:");
                if (movie.Key == show.MovieId)
                {
                    Console.WriteLine(show.Date);
                    //Console.WriteLine("-----------------------------------");
                }
            }
            Console.WriteLine("-----------------------------------");
        }

    }
}