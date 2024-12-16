class Voucher
{
    public static void AdminStart()
    {
        while (true)
        {
            Console.WriteLine("[1]Add a new voucher");
            Console.WriteLine("[2]Show all the vouchers");
            Console.WriteLine("[3]Go back");
            int input = Convert.ToInt16(Console.ReadLine());

            if (input == 1)
            {
                string type = "";

                // Ask the admin what kind of voucher he would like to make
                Console.WriteLine("What kind of voucher would you like to add?\n[1]percentage\n[2]amount of money");
                int typeAnswer = Convert.ToInt16(Console.ReadLine());

                do
                {
                    Console.WriteLine("Please enter a valid answer.");
                    typeAnswer = Convert.ToInt16(Console.ReadLine());
                } while (typeAnswer != 1 && typeAnswer != 2);
                if (typeAnswer == 1)
                {
                    type = "percentage";
                }
                else if (typeAnswer == 2)
                {
                    type = "euro";
                }

                // Ask the admin what the amount of the voucher will be
                Console.WriteLine("What is the amount?");
                decimal amount = Convert.ToInt16(Console.ReadLine());

                // Ask the admin if the voucher needs a description
                string description = "";
                Console.WriteLine("Would you like to add a description?(yes/no)");
                string answer = Console.ReadLine().ToLower();

                do
                {
                    Console.WriteLine("Please enter a valid answer.");
                    answer = Console.ReadLine().ToLower();
                } while (answer != "yes" && answer != "no");
                if (answer == "yes")
                {
                    Console.WriteLine("Type your description");
                    description = Console.ReadLine();
                }
                else if (answer == "no")
                {
                    Console.WriteLine("No");
                }

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
                Console.WriteLine($"Generated code {code}");

                // A new voucher will be made and send to the create voucher function
                VoucherModel newVoucher = new VoucherModel(0, code, description, amount, type, "2025-02-12 14:30", null);
                VoucherLogic.CreateVoucher(newVoucher);

                Console.WriteLine("Done!!");

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


            }
            else if (input == 3)
            {
                Console.Clear();

            }
        }
    }

    public static void UserStart(UserModel acc)
    {
        Console.WriteLine("[1] See all your vouchers");
        Console.WriteLine("[2] Go back");
        Console.WriteLine("What would you like to do?");

        bool isCorrectFormat = int.TryParse(Console.ReadLine(), out int choice);
        if (!isCorrectFormat)
        {
            Console.WriteLine("Invalid format. Make sure to enter a number.");
            UserStart(acc);
        }

        switch (choice)
        {
            case 1:
                Console.Clear();
                PrintAllUserVouchers(acc);
                UserStart(acc);
                break;
            case 2:
                Console.Clear();
                User.Start(acc);
                break;
            default:
                Console.Clear();
                Console.WriteLine("Invalid number. This option is not available.");
                UserStart(acc);
                break;
        }
    }

    public static void PrintAllUserVouchers(UserModel acc)
    {
        List<VoucherModel> vouchers = VoucherLogic.GetVouchersByUserId(acc);

        foreach (VoucherModel voucher in vouchers)
        {
            /*
            I have choosen to use the voucher id instead of the iteration number, 
            because in the theaterbase.cs this method is used and in the theaterbase the user chooses a voucher id.
            */
            Console.WriteLine($"[{voucher.Id}]");
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
        }
    }

}

