class Voucher
{
    public static void AdminStart(UserModel acc)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("[1]Add a new voucher");
            Console.WriteLine("[2]Show all the vouchers");
            Console.WriteLine("[3]Go back to the menu");
            bool isNum = int.TryParse(Console.ReadLine(), out int input);
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number");
                Thread.Sleep(2000);
            }
            else if (input == 1)
            {
                string type = "";
                int typeAnswer = 0;
                string description = "";
                int answer = 0;
                string enterDate = "";
                DateTime dateTime;
                DateTime currentDate = DateTime.Now;

                do
                {
                    // Ask the admin what kind of voucher he would like to make
                    Console.WriteLine("What kind of voucher would you like to add?\n[1]Percentage\n[2]Amount of money");
                    bool isNumType = int.TryParse(Console.ReadLine(), out typeAnswer);
                    Console.Clear();
                    if (isNumType)
                    {
                        Console.WriteLine("Invalid input. Must be a number");
                    }
                    else if (typeAnswer != 1 && typeAnswer != 2)
                    {
                        Console.WriteLine("Invalid input. Enter 1 or 2");
                    }
                } while (typeAnswer != 1 && typeAnswer != 2);

                if (typeAnswer == 1)
                {
                    type = "percentage";
                }
                else if (typeAnswer == 2)
                {
                    type = "euro";
                }

                Console.Clear();

                decimal amount;
                while (true)
                {
                    // Ask the admin what the amount of the voucher will be
                    Console.WriteLine("What is the amount?");
                    bool isDecimal = decimal.TryParse(Console.ReadLine(), out amount);
                    Console.Clear();
                    if (!isDecimal)
                    {
                        Console.WriteLine("Invalid input. Must be a number");
                    }
                    else if (typeAnswer == 1 && amount < 0)
                    {
                        Console.WriteLine("Please try again. A percentage amount cannot be a negative number");
                    }
                    else if (typeAnswer == 1 && amount > 100)
                    {
                        Console.WriteLine("Please try again. A percentage amount cannot be higher than 100");
                    }
                    else if (typeAnswer == 2 && amount < 0)
                    {
                        Console.WriteLine("Please try again. A euro amount cannot be a negative number");
                    }
                    else if (typeAnswer == 1 && amount == 0)
                    {
                        Console.WriteLine("Please try again. A percentage amount must be higher than 0%");
                    }
                    else if (typeAnswer == 2 && amount == 0)
                    {
                        Console.WriteLine("Please try again. A euro amount must be higher than €0");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.Clear();

                do
                {
                    // Ask the admin if the voucher needs a description
                    Console.WriteLine("Would you like to add a description?\n[1]Yes\n[2]No");
                    bool isNumAnswer = int.TryParse(Console.ReadLine(), out answer);
                    Console.Clear();
                    if (!isNumAnswer)
                    {
                        Console.WriteLine("Invalid input. Must be a number");
                    }
                    else if (answer != 1 && answer != 2)
                    {
                        Console.WriteLine("Invalid input. Enter 1 or 2");
                    }
                } while (answer != 1 && answer != 2);
                if (answer == 1)
                {
                    Console.WriteLine("Type your description");
                    description = Console.ReadLine();
                }

                Console.Clear();
            
                do
                {
                    Console.WriteLine("Enter the expiration date?(YYYY-MM-DD)");
                    enterDate = Console.ReadLine();
                    Console.Clear();

                    bool isValidDateTime = DateTime.TryParse(enterDate, out dateTime);
                    if (!isValidDateTime)
                    {
                        Console.WriteLine("Not a valid date time format. Try again.");
                    }
                    else if (dateTime < currentDate)
                    {
                        Console.WriteLine("The date must not have passed yet");
                    }

                } while (dateTime < currentDate);

                Random res = new Random();

                string codeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                int size = 7;
                string code = "";

                // The loop goes seven times through the characters
                for (int i = 0; i < size; i++)
                {
                    // Select an random index
                    int random = res.Next(codeChars.Length);

                    // Add the character to the string
                    code += codeChars[random];
                }

                // If the code already exists in the database, the code is generated again
                List<VoucherModel> vouchers = VoucherLogic.GetAllVouchers();
                foreach (VoucherModel voucher in vouchers)
                {
                    if (voucher.Code == code)
                    {
                        code = "";

                        for (int i = 0; i < size; i++)
                        {
                            // Select an random index
                            int random = res.Next(codeChars.Length);

                            // Add the character to the string
                            code += codeChars[random];
                        }
                    }
                }

                // A new voucher will be made and send to the create voucher function
                VoucherModel newVoucher = new VoucherModel(0, code, description, amount, type, enterDate, null);
                VoucherLogic.CreateVoucher(newVoucher);

                Console.WriteLine("The voucher is created!");
                Thread.Sleep(2000);
            }
            else if (input == 2)
            {
                int count = 1;

                List<VoucherModel> vouchers = VoucherLogic.GetAllVouchers();

                foreach (VoucherModel voucher in vouchers)
                {
                    Console.WriteLine($"[{count}]");
                    Console.WriteLine($"Code: {voucher.Code}");
                    if (voucher.Type == "percentage")
                    {
                        Console.WriteLine($"Amount: {voucher.Amount}%");
                    }
                    else if (voucher.Type == "euro")
                    {
                        Console.WriteLine($"Amount: €{voucher.Amount},-");
                    }
                    Console.WriteLine($"Description: {voucher.Description}");
                    Console.WriteLine($"Expiration Date: {voucher.ExpirationDate}\n---------------------------------------");
                    count += 1;
                }

                Console.WriteLine("\n[1]Go back to voucher menu");
                Console.WriteLine("[2]Exit to admin menu");

                while (true)
                {
                    string menuChoice = Console.ReadLine();
                    if (menuChoice == "1")
                    {
                        Console.Clear();
                        break;
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
            else if (input == 3)
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. This option doesn't exist");
                Thread.Sleep(2000);
            }
        }
    }

    public static void UserStart(UserModel acc)
    {
        // Option to go back to the user menu
        bool menuChoice = false;
        while (!menuChoice)
        {
            Console.Clear();
            PrintAllUserVouchers(acc);
            Console.WriteLine("Would you like to go back to the user menu?\n[1]Yes\n[2]No");
            bool isCorrectFormat = int.TryParse(Console.ReadLine(), out int choice);
            if (!isCorrectFormat)
            {
                Console.WriteLine("Invalid format. Make sure to enter a number.");
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
            else if (0 >= choice || choice > 2)
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
            else if (choice == 1)  // In case the answer is Yes
            {
                Console.Clear();
                User.Start(acc);
                menuChoice = true;
            }
            else  // In case the answer is No
            {
                Console.Clear();
                PrintAllUserVouchers(acc);
            }
        }
    }

    public static void PrintAllUserVouchers(UserModel acc)
    {
        List<VoucherModel> vouchers = VoucherLogic.GetVouchersByUserId(acc);

        int count = 1;

        if (vouchers.Count == 0)
        {
            Console.WriteLine("Sorry, you have currently no vouchers.");
        }

        foreach (VoucherModel voucher in vouchers)
        {
            Console.WriteLine($"[{count}]");
            Console.WriteLine($"Code: {voucher.Code}");
            if (voucher.Type == "percentage")
            {
                Console.WriteLine($"Amount: {voucher.Amount}%");
            }
            else if (voucher.Type == "euro")
            {
                Console.WriteLine($"Amount: €{voucher.Amount},-");
            }
            Console.WriteLine($"Description: {voucher.Description}");
            Console.WriteLine($"Expiration Date: {voucher.ExpirationDate}\n---------------------------------------");
            count += 1;
        }
    }

}