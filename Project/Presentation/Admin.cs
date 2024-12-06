class Admin
{
    public static void Start(UserModel currentUser)
    {
        Console.Clear();
        Console.WriteLine("===== Admin Page =====");
        Console.WriteLine($"Welcome back {currentUser.FirstName} {currentUser.LastName}\n");
        while (true)
        {
            Console.WriteLine("[1]Show the menu of the movies");
            Console.WriteLine("[2]Show the menu of the shows");
            Console.WriteLine("[3]Show the menu of the bar");
            Console.WriteLine("[4]Show the current balance");
            Console.WriteLine("[5]Show the menu of the vouchers");
            Console.WriteLine("[6]Show the movie snack menu");
            Console.WriteLine("[7]Logout");
            int input = Convert.ToInt16(Console.ReadLine());

            if (input == 1)
            {
                Console.Clear();
                Movie.Start();
                //Console.Clear();
            }
            else if (input == 2)
            {
                Console.Clear();
                Show.AdminStart();
                //Console.Clear();
            }
            else if (input == 3)
            {
                Console.Clear();
                Bar.Start();
            }
            else if (input == 4)
            {
                Console.Clear();
                PrintSeats();
            }
            else if (input == 5)
            {
                Console.Clear();
                Voucher.start();
            }
            else if (input == 6)
            {
                SnackMenu.AdminSnackMenu(currentUser);
            }
            else if (input == 7)
            {
                Console.Clear();
                break;
            }
        }
    }

    static void PrintSeats()
    {
        Console.Clear();
        decimal totalMoney = 0m;

        List<SeatsModel> seats = SeatsLogic.GetAllSeats();
        foreach (SeatsModel seat in seats)
        {
            totalMoney += seat.Price;
        }

        Console.WriteLine($"The total: {totalMoney:C2}");
    }





}