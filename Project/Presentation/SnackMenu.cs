public class SnackMenu
{

    public static void AdminSnackMenu(UserModel acc)
    {
        Console.Clear();
        Console.WriteLine("[1] See current snack menu");
        Console.WriteLine("[2] Add menu item");
        Console.WriteLine("[3] Delete menu item");
        Console.WriteLine("[4] Go back to Admin page");

        string choice = Console.ReadLine();
        if (choice == "1")
        {
            bool continueViewing = true;
            while (continueViewing)
            {
                Console.Clear();
                ShowSnackMenu();

                Console.WriteLine("\n[1] Go back to admin snack menu");
                string returnChoice = Console.ReadLine();

                if (returnChoice == "1")
                {
                    continueViewing = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose [1] to go back to admin snack menu.");
                    Console.ReadKey();//it will go back when pressing 1, i made it like this because i didnt want to change showsnackmenu but that method immidiatly ends
                    //i wanted it to show to the admin for longer
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
        else if (choice == "3")
        {
            Console.Clear();
            DeleteMenuItem(acc);
        }
        else if (choice == "4")
        {
            Console.Clear();
            Admin.Start(acc);
        }
    }


    public static void CreateMenu(UserModel acc)
    {
        Console.Clear();
        Console.WriteLine("===== Create Menu Item =====\n");

        Console.WriteLine("Enter the name of the menu item:");
        string name = Console.ReadLine();

        var existingItems = MenuItemLogic.GetAllMenuItems();//get all menu items to check if item to add is already on the menu

        foreach (var item in existingItems)
        {
            if (item.Name.ToLower() == name.ToLower())
            {
                Console.WriteLine("A snack with this name already exists.");
                return;

            }
        }
        Console.WriteLine("Enter the price of the menu item:");
        decimal price;
        while (!decimal.TryParse(Console.ReadLine(), out price)) //will parse user string to user ceimal if its succesfull it will save to price
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
        Console.WriteLine("[2] See current menu");
        Console.WriteLine("[3] Go back to admin snack menu");

        string choice = Console.ReadLine();
        if (choice == "1")
        {
            Console.Clear();
            CreateMenu(acc);
        }
        else if (choice == "2")
        {
            bool continueViewing = true;
            while (continueViewing)
            {
                Console.Clear();
                ShowSnackMenu();

                Console.WriteLine("\n[1] Go back to admin menu");
                string returnChoice = Console.ReadLine();

                if (returnChoice == "1")
                {
                    Console.Clear();
                    continueViewing = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose [1] to go back to admin menu.");
                    Console.ReadKey();//it will go back when pressing 1, i made it like this because i didnt want to change showsnackmenu but that method immidiatly ends
                                      //i wanted it to show to the admin for longer
                }
            }
        }
        else if (choice == "3")
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

        List<MenuItem> foodItems = new List<MenuItem>();
        List<MenuItem> drinkItems = new List<MenuItem>();

        foreach (var item in snacks)
        {
            if (item.Type)
            {
                drinkItems.Add(item);
            }
            else
            {
                foodItems.Add(item);
            }
        }

        int itemNumber = 1;

        if (foodItems.Count > 0)
        {
            Console.WriteLine("=== Food ===");
            itemNumber = AllignmentMenuItems(foodItems, itemNumber);
        }
        else
        {
            Console.WriteLine("No food items available.");
        }

        if (drinkItems.Count > 0)
        {
            Console.WriteLine("\n=== Drinks ===");
            itemNumber = AllignmentMenuItems(drinkItems, itemNumber);
        }
        else
        {
            Console.WriteLine("\nNo drink items available.");
        }

        Console.WriteLine($"[{itemNumber}] Done ordering");
    }



    private static int AllignmentMenuItems(List<MenuItem> items, int startNumber)
    {
        int longest = 0;
        foreach (var item in items)
        {
            if (item.Name.Length > longest)
            {
                longest = item.Name.Length; //get longest length word
            }
        }
        longest = longest + 5; //+5 for alligments

        for (int i = 0; i < items.Count; i++)
        {
            string number = $"[{startNumber}]"; //number for item selecting
            string name = items[i].Name;
            string spaces = "";

            int spacesNeeded = longest - name.Length;
            for (int j = 0; j < spacesNeeded; j++)
            {
                spaces += " "; //it takes the number for longest word +5 and that is the max alligment, so it takes current word - that number = the amount of spaces needed
            }

            string price = $"€{items[i].Price:F2}";
            Console.WriteLine($"{number} {name}{spaces}{price}");
            startNumber++;
        }

        return startNumber;
    }


    public static List<MenuItem> SelectSnacks()
    {
        List<MenuItem> snacks = MenuItemLogic.GetAllMenuItems();
        List<MenuItem> orderList = new List<MenuItem>();// will hold all users snack orders
        bool stillOrdering = true;

        while (stillOrdering) //a while loop that will run while the user is stillOrdering
        {
            ShowSnackMenu();

            Console.WriteLine("\nYour order:");
            if (orderList.Count == 0)
            {
                Console.WriteLine("Your order is empty");
            }
            else
            {
                decimal orderTotal = 0; //keep track of orderTotal money
                //this here prints everytime user picks new item because of while loop
                //so it looks like live updating, it prints the item name and price with 2 decimal
                // adds that price to toal price, and then prints the total order money so that user
                // can keep track of how much they are spending
                foreach (var item in orderList)
                {
                    Console.WriteLine($"- {item.Name}");//(€{item.Price:F2})
                    orderTotal += item.Price;
                }
                Console.WriteLine($"Total: €{orderTotal:F2}");

            }

            Console.Write($"\nWhat would you like to order? input any number from the menu above or type {snacks.Count + 1} to stop ordering ");
            string answer = Console.ReadLine();

            bool isNumber = int.TryParse(answer, out int choice); //change user string input into int

            if (isNumber)
            {
                if (choice == snacks.Count + 1)
                {
                    stillOrdering = false; //this will always refere to the done ordering and will stop the loop
                }
                else if (choice > 0 && choice <= snacks.Count) //if choice is bigger then 0 and smaller then max length of list
                {
                    MenuItem chosenSnack = snacks[choice - 1]; //have to do -1 because of 0 based index, (if user picks number 7 they have actually picked number six)
                    orderList.Add(chosenSnack); //add to order list
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
        //this will just show the user their final order works the same as earlier
    }

    public static void DeleteMenuItem(UserModel acc)
    {
        List<MenuItem> snacks = MenuItemLogic.GetAllMenuItems();

        bool continueDeleting = true; //make bool so i can keep displaying the menu untill admin breaks out of loop

        while (continueDeleting)
        {
            Console.Clear();
            Console.WriteLine("===== Delete Snack Item =====\n");

            for (int i = 0; i < snacks.Count; i++) //loop over all the snacks and print name and price
            {
                Console.WriteLine($"[{i + 1}] {snacks[i].Name} (€{snacks[i].Price:F2})");
            }
            Console.WriteLine($"[{snacks.Count + 1}] Go back to Snack Menu"); //use same +1 menu as in selecting snacks in user

            Console.WriteLine($"\nPlease select a snack to delete by entering its number or type {snacks.Count + 1} to go back to the snack menu.");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= snacks.Count) //tries to make string into int, and if the choice falls in our marges it contines
            {
                Console.WriteLine($"\nAre you sure you want to delete {snacks[choice - 1].Name}? "); // ask for confirmation
                Console.WriteLine("[1] Yes");
                Console.WriteLine("[2] No");

                string confirm = Console.ReadLine();

                if (confirm == "1")
                {
                    MenuItemLogic.DeleteMenuItem(snacks[choice - 1]);
                    Console.WriteLine("\nSnack deleted successfully!");
                    //delete snacks and show new menu. will loop until admin breaks out if it
                    snacks = MenuItemLogic.GetAllMenuItems();
                    continue;
                }
                else if (confirm == "2")
                {
                    Console.WriteLine("\nCancelled deleting item.");
                    continue;
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    continue;
                }
            }
            else if (choice == snacks.Count + 1)
            {
                Console.WriteLine("\nReturning to Snack Menu...");
                continueDeleting = false;
            }
            else
            {
                Console.WriteLine("\nInvalid selection. Please try again.");
            }
        }

        Console.Clear();
        AdminSnackMenu(acc);
    }



}