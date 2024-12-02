class Voucher
{
    public static void start()
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

                Console.WriteLine("What is the amount?");
                int amount = Convert.ToInt16(Console.ReadLine());

                string answer = "";
                string description = "";

                do
                {
                    Console.WriteLine("Would you like to add a description?(yes/no)");
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

                for (int i = 0; i < size; i++)
                {
                    // Select an random index
                    int random = res.Next(codeChars.Length);

                    // Add the character to the string
                    code += codeChars[random];
                }

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

                VoucherModel newVoucher = new VoucherModel(0, code, description, amount, type, null);
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
                    Console.WriteLine($"Description: {voucher.Description}\n---------------------------------------");
                    count += 1;
                }
            }
            else if (input == 3)
            {
                Console.Clear();
                break;
            }
        }
    }
}