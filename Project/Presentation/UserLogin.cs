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
            Console.WriteLine("Welcome back " + acc.FirstName + " " + acc.LastName);
            Console.WriteLine(acc.Email);
            Console.WriteLine(acc.Phone_Number);
            Console.WriteLine("Your email number is " + acc.Email);

            Menu.Start();
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
        }
    }
}