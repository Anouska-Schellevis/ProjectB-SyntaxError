static class UserLogin
{
    static private UserLogic userlogin = new UserLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        UserModel acc = userlogin.CheckLogin(email, password);
        if (acc != null)
        {
            if (acc.Type == 1)
            {
<<<<<<< HEAD
                Console.Clear();
                Console.WriteLine("Admin page\n");
                Console.WriteLine("Welcome back " + admin.FirstName + " " + admin.LastName);
                Console.WriteLine("Which menu would you like to look at?: movies, shows or bar");
                string input = Console.ReadLine().ToLower();

                while (input != null)
                {
                    if (input == "movies")
                    {
                        Movie.Start();
                        Console.Clear();
                    }
                    if (input == "shows")
                    {
                        Show.AdminStart();
                        Console.Clear();
                    }
                    if (input == "bar")
                    {
                        Bar.Start();
                        Console.Clear();
                    }
                }
               
               
=======
                Admin.Start(acc);
>>>>>>> ca7cddb (Split user and admin to seperate files)
            }
            else
            {
                User.Start(acc);
            }

        }
        else
        {
            Console.WriteLine("No account found with that email and password");
        }
    }
}