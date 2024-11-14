class User
{
    public static void Start(UserModel currentUser)
    {
        Console.Clear();
        Console.WriteLine("User page\n");
        Console.WriteLine($"Welcome back {currentUser.FirstName} {currentUser.LastName}");
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