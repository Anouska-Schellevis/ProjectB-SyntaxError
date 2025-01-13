public class UserNewAccountLogic
{
    static private UserLogic userLogic = new UserLogic();//used to call the writer to sql method
    public static void Start()
    {
        string email;
        do
        {
            Console.WriteLine("Please enter your email address:");
            email = Console.ReadLine();
            Console.Clear();
        } while (!IsValidEmail(email));

        string password;
        do
        {
            Console.WriteLine("Create password:");
            password = Console.ReadLine();
            Console.Clear();
        } while (!IsValidPassword(password));

        string firstName;
        do
        {
            Console.WriteLine("Please enter your first name:");
            firstName = Console.ReadLine();
            Console.Clear();
        } while (!OnlyLetters(firstName));


        string lastName;
        do
        {
            Console.WriteLine("Please enter your last name:");
            lastName = Console.ReadLine();
            Console.Clear();
        } while (!OnlyLetters(lastName));

        string phoneInput; //to loop over to check length
        int phoneNumber;

        do
        {   //for now it takes 10 digets aka 0612345678
            Console.WriteLine("Please enter your phone number (10 digits):");
            phoneInput = Console.ReadLine();
            Console.Clear();

            if (phoneInput.Length == 10 && OnlyNumbers(phoneInput))
            {
                phoneNumber = int.Parse(phoneInput); // Convert to int if valid
                break;
            }
            else
            {
                Console.WriteLine("Invalid phone number. Please enter a 10-digit number."); // Prompt for re-entry
            }
        } while (true); //keeps running until if conition is met

        UserModel newUser = new UserModel(0, email, password, firstName, lastName, phoneNumber, 0, 0);

        userLogic.CreateAccount(newUser);
        Console.WriteLine("Account created successfully! You can now log in.");
        Thread.Sleep(2000);
        Menu.Start();
    }

    private static bool OnlyLetters(string input)//method that loops over whatever input you give it and checks if every char is a letter, used for name validation
    {
        foreach (char i in input)
        {
            if (!char.IsLetter(i))
            {
                Console.WriteLine("Invalid input! Please enter only letters.");
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
                return false;
        }
        return true;
    }

    private static bool IsValidEmail(string email)
    {
        //check if the email has s @ and ends with the valid domains
        if (email.Contains("@") &&
            (email.EndsWith(".com") || email.EndsWith(".nl") || email.EndsWith(".net")))
        {
            return true;
        }
        else
        {
            Console.WriteLine("Invalid email. try another format");
            return false;
        }
    }

    //check if user password is atleast 5 char long and has a capital letter and lower letter in it.
    private static bool IsValidPassword(string password)
    {

        if (password.Length <= 5)
        {
            Console.WriteLine("Password must be longer than 5 characters");
            return false;
        }


        bool hasUpperCaseLetter = false;
        bool hasLowerCaseLetter = false;

        foreach (char i in password)
        {
            if (char.IsUpper(i))
            {
                hasUpperCaseLetter = true;
            }
            else if (char.IsLower(i))
            {
                hasLowerCaseLetter = true;
            }
        }

        if (!hasUpperCaseLetter || !hasLowerCaseLetter)//if there is no capitals or lower letters, (or only 1) its false
        {
            Console.WriteLine("Password must contain at least one uppercase letter and one lowercase letter.");
            return false;
        }

        return true;
    }
}