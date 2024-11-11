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
            UserSession.Instance.CurrentUser = acc;
            UserModel admin = userlogin.GetByType(email);
            if (admin != null)
            {
                Console.Clear();
                Console.WriteLine("Admin page\n");
                Console.WriteLine("Welcome back " + admin.FirstName + " " + admin.LastName);
                // Console.WriteLine("would you like to look at the menu for movies or the menu for shows");
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
                }
               
               
            }
            else
            {
                Console.Clear();
                Console.WriteLine("User page\n");
                Console.WriteLine("Welcome back " + acc.FirstName + " " + acc.LastName);
                Console.WriteLine("Would you like to see the overview of available movies Y/N");
                string answer = Console.ReadLine().ToLower();

                if (answer == "y")
                {
                    Show.UserStart();
                }
                else
                {
                    Console.WriteLine("back to the menu...");
                    Menu.Start();
                }
                
            }

        }
        else
        {
            Console.WriteLine("No account found with that email and password");
        }
    }
}