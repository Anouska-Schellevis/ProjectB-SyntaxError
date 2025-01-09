static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the cinema. You can choose the following options.\n");
            Console.WriteLine("[1]Login");
            Console.WriteLine("[2]Create a new account");
            Console.WriteLine("[3]View our info page");
            Console.WriteLine("[4]Stop the program");

        bool isNum = int.TryParse(Console.ReadLine(), out int input);
        if (!isNum)
        {
            Console.WriteLine("Invalid input. Must be a number");
            Thread.Sleep(2000);
            Start();
        }
        else if (input == 1)
        {
            UserLogin.Start();
        }
        else if (input == 2)
        {
            UserNewAccountLogic.Start();
        }
        else if (input == 3)
        {
            InfoPage.Start();
        }
        else
        {
            Console.WriteLine("Invalid input. This option doesn't exist");
            Thread.Sleep(2000);
            Start();
        }
    }
}