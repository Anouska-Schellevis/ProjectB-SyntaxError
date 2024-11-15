class Admin
{
    public static void Start(UserModel currentUser)
    {
        Console.Clear();
        Console.WriteLine("Admin page\n");
        Console.WriteLine($"Welcome back {currentUser.FirstName} {currentUser.LastName}\n");
        while (true)
        {
            Console.WriteLine("[1]Show the menu of the movies\n[2]Show the menu of the shows\n[3]Show the current balance\n[4]Logout");
            int input = Convert.ToInt16(Console.ReadLine());

            if (input == 1)
            {
                Movie.Start();
                Console.Clear();
            }
            else if (input == 2)
            {
                Show.AdminStart();
                Console.Clear();
            }
            else if (input == 3)
            {
                PrintSeats();
            }
            else if (input == 4)
            {
                break;
            }
        }
    }

    static void PrintSeats()
    {
        int totalMoney = 0;

        List<SeatsModel> seats = SeatsLogic.GetAllSeats();
        foreach (SeatsModel seat in seats)
        {
            totalMoney += seat.Price;
        }

        Console.WriteLine($"The total: {totalMoney}");
    }
}