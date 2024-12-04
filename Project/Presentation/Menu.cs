static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the cinema. You can choose the following options.\n");
        Console.WriteLine("[1]Login");
        Console.WriteLine("[2]Create a new account");
        Console.WriteLine("[3]View our info page");

        int input = Convert.ToInt16(Console.ReadLine());
        if (input == 1)
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
            Console.WriteLine("Invalid input");
            Start();
        }

    }
}