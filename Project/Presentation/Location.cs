using System.Text.RegularExpressions;

class Location
{
    public static void Start(UserModel acc)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Location menu =====\n");
            Console.WriteLine("[1]Overview of all locations");
            Console.WriteLine("[2]Add Location");
            Console.WriteLine("[3]Go back to the menu");
            Console.WriteLine("What would you like to do?");

            bool isNum = int.TryParse(Console.ReadLine()!, out int input);
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number");
                Thread.Sleep(2000);
            }
            else if (input == 1)
            {
                Console.Clear();
                PrintAllLocations();

                Console.WriteLine("\n[1]Go back to location menu");
                Console.WriteLine("[2]Exit to admin menu");

                while (true)
                {
                    string menuChoice = Console.ReadLine()!;
                    if (menuChoice == "1")
                    {
                        Console.Clear();
                        Start(acc);
                        return;
                    }
                    else if (menuChoice == "2")
                    {
                        Console.Clear();
                        Admin.Start(acc);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please enter 1 or 2.");
                        Thread.Sleep(2000);

                        /*
                        The cursor goes back to the position of the error message, which is replaced after 2 seconds with an empty string.
                        */
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.SetCursorPosition(0, Console.CursorTop + 1);
                    }
                }
            }
            else if (input == 2)
            {
                Console.Clear();
                AddLocation();
                Thread.Sleep(2000);
            }
            else if (input == 3)
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. This option doesn't exist");
                Thread.Sleep(2000);
                Console.Clear();
            }
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
            inputCity = Console.ReadLine()!;
            Console.Clear();
        } while (inputCity.Length == 0 || !OnlyLetters(inputCity));

        string inputAddress;
        do
        {
            Console.WriteLine("Enter the address like this 'Street 1': ");
            inputAddress = Console.ReadLine()!;
            Console.Clear();
        } while (!CheckAddress(inputAddress));

        string inputPostalCode;
        do
        {
            Console.WriteLine("Enter the postal code like this '1111 AA': ");
            inputPostalCode = Console.ReadLine()!;
            Console.Clear();
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

        if (numbers.Length != 4 || letters.Length != 2)
        {
            return false;
        }
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