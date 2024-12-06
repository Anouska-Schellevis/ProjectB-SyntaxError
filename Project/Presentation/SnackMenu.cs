public class SnackMenu
{

    public static void AdminSnackMenu(UserModel acc)
    {
        Console.Clear();
        Console.WriteLine("[1] See current snack menu");
        Console.WriteLine("[2] Add menu item");

        string choice = Console.ReadLine();
        if (choice == "1")
        {
            bool continueViewing = true;
            while (continueViewing)
            {
                Console.Clear();
                ShowSnackMenu();

                Console.WriteLine("[1] Go back to admin menu");
                string returnChoice = Console.ReadLine();

                if (returnChoice == "1")
                {
                    continueViewing = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose [1] to go back to admin menu.");
                    Console.ReadKey();//it will
                }
            }

            Console.Clear();
            AdminSnackMenu(acc);
        }
        else if (choice == "2")
        {
            Console.Clear();
            CreateMenu(acc);
        }
    }


    public static void CreateMenu(UserModel acc)
    {
        Console.Clear();
        Console.WriteLine("===== Create Menu Item =====\n");

        Console.WriteLine("Enter the name of the menu item:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter the price of the menu item:");
        decimal price;
        while (!decimal.TryParse(Console.ReadLine(), out price))
        {
            Console.WriteLine("Invalid price. Please enter a valid number:");
        }

        Console.WriteLine("\nWhat type is this menu item?");
        Console.WriteLine("[1] Drink");
        Console.WriteLine("[2] Food");

        bool type;
        while (true)
        {
            string typeChoice = Console.ReadLine();
            if (typeChoice == "1")
            {
                type = true;
                break;
            }
            else if (typeChoice == "2")
            {
                type = false;
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1 for drink or 2 for food:");
            }
        }

        MenuItem newItem = new MenuItem(name, price, type);
        MenuItemLogic.WriteMenuItem(newItem);

        Console.WriteLine("\nMenu item created successfully!");
        Console.WriteLine("Do you want to add another item or go back to the admin menu");
        Console.WriteLine("[1] Add another menu item");
        Console.WriteLine("[2] Go back to admin menu");

        string choice = Console.ReadLine();
        if (choice == "1")
        {
            Console.Clear();
            CreateMenu(acc);
        }
        else if (choice == "2")
        {
            Console.Clear();
            return;
        }
    }

    public static void ShowSnackMenu()
    {
        List<MenuItem> snacks = MenuItemLogic.GetAllMenuItems();
        Console.Clear();
        Console.WriteLine("===== Snack Menu =====\n");

        int longest = 0;
        foreach (var snack in snacks)
        {
            if (snack.Name.Length > longest)
            {
                longest = snack.Name.Length;
            }
        }
        longest = longest + 5;

        for (int i = 0; i < snacks.Count; i++)
        {
            string number = $"[{i + 1}]";
            string name = snacks[i].Name;
            string spaces = "";

            int spacesNeeded = longest - name.Length;
            for (int j = 0; j < spacesNeeded; j++)
            {
                spaces += " ";
            }

            string price = $"€{snacks[i].Price:F2}";
            Console.WriteLine($"{number} {name}{spaces}{price}");
        }

        Console.WriteLine($"[{snacks.Count + 1}] Done ordering");
    }

    public static List<MenuItem> SelectSnacks()
    {
        List<MenuItem> snacks = MenuItemLogic.GetAllMenuItems();
        List<MenuItem> orderList = new List<MenuItem>();
        bool stillOrdering = true;

        while (stillOrdering)
        {
            ShowSnackMenu();

            Console.WriteLine("\nYour order:");
            if (orderList.Count == 0)
            {
                Console.WriteLine("Your order is empty");
            }
            else
            {
                decimal orderTotal = 0;
                foreach (var item in orderList)
                {
                    Console.WriteLine($"- {item.Name} (€{item.Price:F2})");
                    orderTotal += item.Price;
                }
                Console.WriteLine($"Total: €{orderTotal:F2}");
            }

            Console.Write("\nWhat would you like to order? ");
            string answer = Console.ReadLine();

            bool isNumber = int.TryParse(answer, out int choice);

            if (isNumber)
            {
                if (choice == snacks.Count + 1)
                {
                    stillOrdering = false;
                }
                else if (choice > 0 && choice <= snacks.Count)
                {
                    MenuItem chosenSnack = snacks[choice - 1];
                    orderList.Add(chosenSnack);
                    Console.WriteLine($"\nAdded {chosenSnack.Name} to your order.");
                }
                else
                {
                    Console.WriteLine("\nPlease pick a number from the menu.");
                }
            }
            else
            {
                Console.WriteLine("\nPlease enter a number.");
            }
        }

        Console.Clear();
        Console.WriteLine("===== Your Final Order =====");

        if (orderList.Count == 0)
        {
            Console.WriteLine("No snacks ordered.");
        }
        else
        {
            decimal finalTotal = 0;
            foreach (var item in orderList)
            {
                Console.WriteLine($"- {item.Name} (€{item.Price:F2})");
                finalTotal += item.Price;
            }
            Console.WriteLine($"Total: €{finalTotal:F2}");
        }

        return orderList;
    }
}