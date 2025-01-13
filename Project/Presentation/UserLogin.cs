static class UserLogin
{
    static private UserLogic userlogin = new UserLogic();


    public static void Start()
    {
        bool loginSuccessful = false;

        while (!loginSuccessful)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the login page");
            Console.WriteLine("Please enter your email address");
            string email = Console.ReadLine()!;
            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine()!;

            UserModel acc = userlogin.CheckLogin(email, password);
            if (acc != null)
            {
                loginSuccessful = true;
                if (acc.Type == 1)
                {
                    Admin.Start(acc);
                }
                else
                {
                    User.Start(acc);
                }
            }
            else
            {
                Console.WriteLine("No account found with that email and password");
                Console.WriteLine("Please try again");
                Console.Clear();
            }
        }
    }
}