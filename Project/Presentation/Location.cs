class Location
{
    public static void Start()
    {
        Console.Clear();
        Console.WriteLine("[1] Overview of all locations");
        Console.WriteLine("[2] Add Location");
        Console.WriteLine("[3] Go back");
        Console.WriteLine("What would you like to do?");

        bool isCorrectFormat = int.TryParse(Console.ReadLine(), out int choice);
        if (!isCorrectFormat)
        {
            Console.WriteLine("Invalid format. Make sure to enter a number.");
            Start();
        }

        switch (choice)
        {
            case 1:
                PrintAllLocations();
                Start();
                break;
            case 2:
                AddLocation();
                Start();
                break;
            case 3:
                break;
            default:
                Console.WriteLine("No valid option selected. Please try again.");
                Start();
                break;
        }
    }

    public static void PrintAllLocations()
    {
        
    }

    public static void AddLocation()
    {
        Console.WriteLine("Enter the city name: ");
        string inputCity = Console.ReadLine();
        // checks are placed in the logic layer

        Console.WriteLine("Enter the address: ");
        string inputAddress = Console.ReadLine();
        // checks are placed in the logic layer

        Console.WriteLine("Enter the postal code: ");
        string inputPostalCode = Console.ReadLine();
        // checks are placed in the logic layer
    }
}