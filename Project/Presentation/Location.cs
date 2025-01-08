using System.Text.RegularExpressions;

class Location
{
    public static void Start()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("[1]Overview of all locations");
            Console.WriteLine("[2]Add Location");
            Console.WriteLine("[3]Go back to the menu");
            Console.WriteLine("What would you like to do?");

            int input = Convert.ToInt16(Console.ReadLine());

            if (input == 1)
            {
                Console.Clear();
                PrintAllLocations();
            }
            else if (input == 2)
            {
                Console.Clear();
                AddLocation();
            }
            else if (input == 3)
            {
                Console.Clear();
                break;
            }


            // bool isCorrectFormat = int.TryParse(Console.ReadLine(), out int choice);
            // if (!isCorrectFormat)
            // {
            //     Console.WriteLine("Invalid format. Make sure to enter a number.");
            //     Start();
            // }

            // switch (choice)
            // {
            //     case 1:
            //         PrintAllLocations();
            //         Start();
            //         break;
            //     case 2:
            //         AddLocation();
            //         Start();
            //         break;
            //     case 3:
            //         break;
            //     default:
            //         Console.WriteLine("No valid option selected. Please try again.");
            //         Start();
            //         break;
            // }
        }
    }

    public static void PrintAllLocations()
    {
        List<LocationModel> locations = LocationLogic.GetAllLocations();

        foreach (LocationModel location in locations)
        {
            Console.WriteLine($"City: {location.City}");
            Console.WriteLine($"Address: {location.Address}");
            Console.WriteLine($"Postal Code: {location.PostalCode}");
            Console.WriteLine("----------------------------------------");
        }
    }

    public static void AddLocation()
    {
        Console.Clear();

        string inputCity;
        do
        {
            Console.WriteLine("Enter the city name: ");
            inputCity = Console.ReadLine();
        } while (!OnlyLetters(inputCity));

        string inputAddress;
        do
        {
            Console.WriteLine("Enter the address like this 'Street 1': ");
            inputAddress = Console.ReadLine();
        } while (!CheckAddress(inputAddress));

        string inputPostalCode;
        do
        {
            Console.WriteLine("Enter the postal code like this '1111 AA': ");
            inputPostalCode = Console.ReadLine();
        } while (!CheckPostalCode(inputPostalCode));

        LocationModel newLocation = new LocationModel(0, inputCity, inputAddress, inputPostalCode);
        LocationLogic.WriteLocation(newLocation);

        Console.WriteLine("The location has been added!");
    }

    //method that loops over whatever input you give it and checks if every char is a letter, used for name validation
    private static bool OnlyLetters(string input)
    {
        string removeSpaces = input.Replace(" ", "");

        foreach (char i in removeSpaces)
        {
            if (!char.IsLetter(i))
            {
                Console.WriteLine("Invalid input!");
                return false;
            }
        }
        return true;
    }

    private static bool OnlyNumbers(string input)//same as only letters just for numbers
    {
        foreach (char i in input)
        {
            if (!char.IsDigit(i))
            {
                Console.WriteLine("Invalid input!");
                return false;
            }
        }
        return true;
    }

    private static bool CheckAddress(string input)
    {
        string pattern = @"^([A-Za-z\s]+)\s(\d+)$";
        Match match = Regex.Match(input, pattern);

        if (match.Success)
        {
            string letters = match.Groups[1].Value;
            string numbers = match.Groups[2].Value;

            if (!OnlyLetters(letters))
            {
                return false;
            }

            if (!OnlyNumbers(numbers))
            {
                return false;
            }
        }
        else
        {
            Console.WriteLine("Invalid input!");
            return false;
        }
        return true;
    }

    private static bool CheckPostalCode(string input)
    {
        string[] splitSentence = input.Split(" ");

        string numbers = splitSentence[0];
        string letters = splitSentence[1];

        if (!OnlyNumbers(numbers))
        {
            return false;
        }

        if (!OnlyLetters(letters))
        {
            return false;
        }
        return true;
    }
}