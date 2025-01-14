public class SnackMenu
{

    public static void AdminSnackMenu(UserModel acc)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Snack menu =====");
            Console.WriteLine("[1]See current snack menu");
            Console.WriteLine("[2]Add menu item");
            Console.WriteLine("[3]Delete menu item");
            Console.WriteLine("[4]Edit menu item");
            Console.WriteLine("[5]See snack overview");
            Console.WriteLine("[6]Go back");


            bool isNum = int.TryParse(Console.ReadLine(), out int choice);
            if (!isNum)
            {
                Console.WriteLine("Invalid input. Must be a number");
                Thread.Sleep(2000);
            }
            else if (choice == 1)
            {
                bool continueViewing = true;
                while (continueViewing)
                {
                    Console.Clear();
                    ShowSnackMenu();

                    Console.WriteLine("\n[1]Go back to admin snack menu");
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
            else if (choice == 2)
            {
                Console.Clear();
                CreateMenu(acc);
            }
            else if (choice == 3)
            {
                Console.Clear();
                DeleteMenuItem(acc);
            }
            else if (choice == 4)
            {
                Console.Clear();
                EditMenuItem(acc);
            }
            else if (choice == 5)
            {
                Console.Clear();
                SnackAdminOverview(acc);
            }
            else if (choice == 6)
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
        Admin.Start(acc);
    }

    public static void CreateMenu(UserModel acc)
    {
        Console.Clear();
        Console.WriteLine("===== Create Menu Item =====");

        string name;
        do
        {
            Console.WriteLine("\nEnter the name of the menu item:");
            name = Console.ReadLine();
        } while (!OnlyLetters(name));

        var existingItems = MenuItemLogic.GetAllMenuItems();//get all menu items to check if item to add is already on the menu

        foreach (var item in existingItems)
        {
            if (item.Name.ToLower() == name.ToLower())
            {
                Console.WriteLine("A snack with this name already exists.");
                Thread.Sleep(2000);
                return;
            }
        }
        Console.WriteLine("\nEnter the price of the menu item (use a comma (,) for decimals, example, 5,50):");
        decimal price;
        while (true)
        {
            string input = Console.ReadLine();

            //check if the input contains a dot (.) because we dont want that
            if (input.Contains("."))
            {
                Console.Clear();
                Console.WriteLine("Invalid price. Use a comma (,) for decimals, example, 5,50:");
                continue;
            }

            if (decimal.TryParse(input, out price))//try to make it a decimal
            {
                break;
            }

            Console.Clear();
            Console.WriteLine("Invalid price. Please enter a valid number using a comma (,) for decimals, example, 5,50:");
        }


        Console.WriteLine("\nWhat type is this menu item?");
        Console.WriteLine("[1]Drink");
        Console.WriteLine("[2]Food");

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
                Console.Clear();
                Console.WriteLine("Invalid choice. Please enter 1 for drink or 2 for food:");
            }
        }

        MenuItem newItem = new MenuItem(name, price, type);
        MenuItemLogic.WriteMenuItem(newItem);

        Console.Clear();

        Console.WriteLine("\nMenu item created successfully!");
        Console.WriteLine("Do you want to add another item or go back to the admin menu");
        Console.WriteLine("[1]Add another menu item");
        Console.WriteLine("[2]See current menu");
        Console.WriteLine("[3]Go back to snack menu");

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

                Console.WriteLine("\n[1]Go back to admin menu");
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
            SnackMenu.AdminSnackMenu(acc);
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
        int longest = 0;

        // Find the longest item name for alignment
        foreach (var item in foodItems)
        {
            if (item.Name.Length > longest)
            {
                longest = item.Name.Length; //get longest length word
            }
        }
        foreach (var item in drinkItems)
        {
            if (item.Name.Length > longest)
            {
                longest = item.Name.Length; //get longest length word
            }
        }
        longest += 5; //+5 for alignment

        if (foodItems.Count > 0)
        {
            Console.WriteLine("=== Food ===");
            foreach (var item in foodItems)
            {
                string number = $"[{itemNumber}]";
                string name = item.Name;
                string spaces = "";  //create empty string to hold the spaces

                //calculate how many spaces are needed
                int spacesNeeded = longest - name.Length;
                for (int i = 0; i < spacesNeeded; i++)
                {
                    spaces += " ";  //add space one by one
                }

                string price = $"€{item.Price:F2}";
                Console.WriteLine($"{number} {name}{spaces}{price}");
                itemNumber++;
            }
        }
        else
        {
            Console.WriteLine("No food items available.");
        }

        if (drinkItems.Count > 0)
        {
            Console.WriteLine("\n=== Drinks ===");
            foreach (var item in drinkItems)
            {
                string number = $"[{itemNumber}]";
                string name = item.Name;
                string spaces = "";  //create empty string to hold the spaces

                //calculate how many spaces are needed
                int spacesNeeded = longest - name.Length;
                for (int i = 0; i < spacesNeeded; i++)
                {
                    spaces += " ";  //add space one by one
                }

                string price = $"€{item.Price:F2}";
                Console.WriteLine($"{number}{name}{spaces}{price}");
                itemNumber++;
            }
        }
        else
        {
            Console.WriteLine("\nNo drink items available.");
        }

        Console.WriteLine($"[{itemNumber}]Done ordering");
    }


    public static Dictionary<MenuItem, int> SelectSnacks()
    {
        List<MenuItem> snacks = MenuItemLogic.GetAllMenuItems();
        Dictionary<MenuItem, int> orderDict = new Dictionary<MenuItem, int>();
        bool stillOrdering = true;

        while (stillOrdering)
        {
            ShowSnackMenu();

            Console.WriteLine("\nYour order:");
            if (orderDict.Count == 0)
            {
                Console.WriteLine("Your order is empty.");
            }
            else
            {
                decimal orderTotal = 0;
                foreach (var order in orderDict)
                {
                    Console.WriteLine($"- {order.Value} x {order.Key.Name}");
                    orderTotal += order.Key.Price * order.Value;
                }
                Console.WriteLine($"Total: €{orderTotal:F2}");
            }

            Console.Write($"\nWhat would you like to order? Input any number from the menu above or type {snacks.Count + 1} to stop ordering: ");
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
                    Console.Write($"\nHow many of {chosenSnack.Name} would you like to order? ");
                    string AmountSnacks = Console.ReadLine();

                    bool validAmount = int.TryParse(AmountSnacks, out int quantity) && quantity > 0;
                    if (validAmount)
                    {
                        if (orderDict.ContainsKey(chosenSnack))
                        {
                            orderDict[chosenSnack] += quantity;
                        }
                        else
                        {
                            orderDict[chosenSnack] = quantity;
                        }
                        Console.WriteLine($"\nAdded {quantity} x {chosenSnack.Name} to your order.");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.WriteLine("\nPlease enter a valid quantity.");
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    Console.WriteLine("\nPlease pick a number from the menu.");
                    Thread.Sleep(2000);
                }
            }
            else
            {
                Console.WriteLine("\nPlease enter a number.");
                Thread.Sleep(2000);
            }
        }

        bool orderConfirmed = false;
        while (!orderConfirmed)
        {
            Console.Clear();
            Console.WriteLine("===== Your Final Order =====");
            decimal finalTotal = 0;
            if (orderDict.Count == 0)
            {
                Console.WriteLine("No snacks ordered.");
            }
            else
            {
                foreach (var order in orderDict)
                {
                    Console.WriteLine($"- {order.Value} x {order.Key.Name}");
                    finalTotal += order.Key.Price * order.Value;
                }
                Console.WriteLine($"Total: €{finalTotal:F2}");
            }

            int editChoice;
            do
            {
                Console.WriteLine("\nWould you like to:");
                Console.WriteLine("[1]Confirm your order");
                Console.WriteLine("[2]Edit your order");
                bool isNum = int.TryParse(Console.ReadLine(), out editChoice);

                if (!isNum)
                {
                    Console.WriteLine("Invalid input. Must be a number");
                    Thread.Sleep(2000);
                }
                else if (editChoice == 1)
                {
                    orderConfirmed = true;
                }
                else if (editChoice == 2)
                {
                    bool editingOrder = true;
                    while (editingOrder)
                    {
                        Console.Clear();
                        Console.WriteLine("Choose the number for which item you want to edit");
                        Console.WriteLine("===== Your Order =====");
                        int itemNumber = 1;
                        foreach (var order in orderDict)
                        {
                            Console.WriteLine($"[{itemNumber}] {order.Value} x {order.Key.Name}");
                            itemNumber++;
                        }
                        Console.WriteLine($"[{itemNumber}]Go back to final order.");

                        string order_edit_choice = Console.ReadLine();

                        if (order_edit_choice == (itemNumber.ToString()))
                        {
                            editingOrder = false;
                            continue;
                        }


                        if (int.TryParse(order_edit_choice, out int selectedItem) && selectedItem > 0 && selectedItem <= orderDict.Count)
                        {
                            Console.Clear();
                            var selectedOrderItem = orderDict.ElementAt(selectedItem - 1);//pick user pick -1 for 0 based index


                            Console.WriteLine("[1]Change amount");
                            Console.WriteLine("[2]Remove this item");
                            Console.WriteLine("[3]Go back to final order");

                            string user_edit_order_choice = Console.ReadLine();

                            if (user_edit_order_choice == "1")
                            {

                                Console.WriteLine("Enter the new amount you want of this item:");
                                string newQuantityInput = Console.ReadLine();
                                if (int.TryParse(newQuantityInput, out int newQuantity) && newQuantity > 0)
                                {
                                    orderDict[selectedOrderItem.Key] = newQuantity;
                                    Console.WriteLine($"Updated {selectedOrderItem.Key.Name} to {newQuantity}.");
                                }
                                else
                                {
                                    Console.WriteLine("invalid amount");
                                }
                            }
                            else if (user_edit_order_choice == "2")
                            {
                                orderDict.Remove(selectedOrderItem.Key);
                                Console.WriteLine($"Removed {selectedOrderItem.Key.Name} from your order");
                            }

                            Console.Clear();
                            Console.WriteLine("===== Your Final Order =====");
                            decimal finalOrderTotal = 0;
                            if (orderDict.Count == 0)
                            {
                                Console.WriteLine("No snacks ordered");
                            }
                            else
                            {
                                foreach (var order in orderDict)
                                {
                                    Console.WriteLine($"- {order.Value} x {order.Key.Name}");
                                    finalOrderTotal += order.Key.Price * order.Value;
                                }
                                Console.WriteLine($"Total: €{finalOrderTotal:F2}");
                            }

                            Console.WriteLine("\nWould you like to:");
                            Console.WriteLine("[1]Confirm your order");
                            Console.WriteLine("[2]Edit your order");
                            string finalChoice = Console.ReadLine();

                            if (finalChoice == "1")
                            {
                                editingOrder = false;
                            }
                            else if (finalChoice == "2")
                            {
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose [1] to confirm or [2] to edit.");
                    Thread.Sleep(2000);
                }
            } while (editChoice != 1 && editChoice != 2);
        }
        return orderDict;
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
                Console.WriteLine($"[{i + 1}]{snacks[i].Name} (€{snacks[i].Price:F2})");
            }
            Console.WriteLine($"[{snacks.Count + 1}]Go back to Snack Menu"); //use same +1 menu as in selecting snacks in user

            Console.WriteLine($"\nPlease select a snack to delete by entering its number or type {snacks.Count + 1} to go back to the snack menu.");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Invalid input. Must be a number");
                Thread.Sleep(2000);
            }
            else if (choice <= 0 || choice > snacks.Count + 1)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                Thread.Sleep(2000);
            }
            else if (int.TryParse(input, out choice) && choice > 0 && choice <= snacks.Count) //tries to make string into int, and if the choice falls in our marges it contines
            {
                Console.WriteLine($"\nAre you sure you want to delete {snacks[choice - 1].Name}? "); // ask for confirmation
                Console.WriteLine("[1]Yes");
                Console.WriteLine("[2]No");

                bool isNum = int.TryParse(Console.ReadLine(), out int confirm);

                if (!isNum)
                {
                    Console.WriteLine("Invalid input. Must be a number");
                    Thread.Sleep(2000);
                }
                else if (confirm == 1)
                {
                    MenuItemLogic.DeleteMenuItem(snacks[choice - 1]);
                    Console.WriteLine("\nSnack deleted successfully!");
                    Thread.Sleep(2000);
                    //delete snacks and show new menu. will loop until admin breaks out if it
                    snacks = MenuItemLogic.GetAllMenuItems();
                    continue;
                }
                else if (confirm == 2)
                {
                    Console.WriteLine("\nCancelled deleting item.");
                    Thread.Sleep(2000);
                    continue;
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    Thread.Sleep(2000);
                    continue;
                }
            }
            else if (choice == snacks.Count + 1)
            {
                Console.WriteLine("\nReturning to Snack Menu...");
                Thread.Sleep(2000);
                continueDeleting = false;
            }
            else
            {
                Console.WriteLine("\nInvalid selection. Please try again.");
                Thread.Sleep(2000);
            }
        }

        Console.Clear();
        AdminSnackMenu(acc);
    }

    public static void EditMenuItem(UserModel acc)
    {
        List<MenuItem> snacks = MenuItemLogic.GetAllMenuItems();

        bool continueEditing = true; //make bool so i can keep displaying the menu untill admin breaks out of loop

        while (continueEditing)
        {
            Console.Clear();
            Console.WriteLine("===== Edit Snack Item =====\n");

            for (int i = 0; i < snacks.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {snacks[i].Name} (€{snacks[i].Price:F2})");
            }
            Console.WriteLine($"[{snacks.Count + 1}]Go back to Snack Menu");

            Console.WriteLine($"\nPlease select a snack to edit by entering its number or type [{snacks.Count + 1}] to go back.");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Invalid input. Must be a number");
                Thread.Sleep(2000);
                EditMenuItem(acc);
            }
            else if (int.TryParse(input, out choice) && choice > 0 && choice <= snacks.Count)
            {
                Console.Clear();
                MenuItem selectedSnack = snacks[choice - 1];

                Console.WriteLine($"You are editing: {selectedSnack.Name} (€{selectedSnack.Price:F2})");

                int nameChoice = 0;
                do
                {
                    Console.WriteLine("\nWould you like to edit the name?");
                    Console.WriteLine("[1]Yes");
                    Console.WriteLine("[2]No");
                    bool isNum = int.TryParse(Console.ReadLine(), out nameChoice);
                    Console.Clear();
                    if (!isNum)
                    {
                        Console.WriteLine("Invalid input. Must be a number.");
                    }
                    else if (nameChoice != 1 && nameChoice != 2)
                    {
                        Console.WriteLine("Invalid input try again.");
                    }
                } while (nameChoice != 1 && nameChoice != 2);
                if (nameChoice == 1)
                {
                    Console.WriteLine("Enter the new name:");
                    selectedSnack.OldName = selectedSnack.Name; // keep the old name for search in the db
                    selectedSnack.Name = Console.ReadLine();
                }

                Console.Clear();

                int priceChoice = 0;
                do
                {
                    Console.WriteLine("\nWould you like to edit the price?");
                    Console.WriteLine("[1] Yes");
                    Console.WriteLine("[2] No");
                    bool isNum = int.TryParse(Console.ReadLine(), out priceChoice);
                    Console.Clear();
                    if (!isNum)
                    {
                        Console.WriteLine("Invalid input. Must be a number.");
                    }
                    else if (priceChoice != 1 && priceChoice != 2)
                    {
                        Console.WriteLine("Invalid input. Try again.");
                    }
                } while (priceChoice != 1 && priceChoice != 2);

                if (priceChoice == 1)
                {
                    Console.WriteLine("Enter the new price (use a comma (,) for decimals, example, 5,50):");
                    decimal newPrice;

                    while (true)
                    {
                        string userinput = Console.ReadLine();

                        //check if the input contains a dot (.) because we don't want that
                        if (userinput.Contains("."))
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid price. Use a comma (,) for decimals, example, 5,50:");
                            continue;
                        }

                        if (decimal.TryParse(userinput, out newPrice) && newPrice > 0)
                        {
                            break;
                        }

                        Console.Clear();
                        Console.WriteLine("Invalid price. Please enter a valid number greater than 0 using a comma (,) for decimals, example, 5,50:");
                    }

                    selectedSnack.Price = newPrice;
                }


                Console.Clear();

                int typeChoice = 0;
                do
                {
                    Console.WriteLine("\nWould you like to edit the type (drink/food)?");
                    Console.WriteLine("[1]Yes");
                    Console.WriteLine("[2]No");
                    bool isNum = int.TryParse(Console.ReadLine(), out typeChoice);
                    Console.Clear();
                    if (!isNum)
                    {
                        Console.WriteLine("Invalid input. Must be a number.");
                    }
                    else if (typeChoice != 1 && typeChoice != 2)
                    {
                        Console.WriteLine("Invalid input try again.");
                    }
                } while (typeChoice != 1 && typeChoice != 2);
                if (typeChoice == 1)
                {
                    bool type;
                    while (true)
                    {
                        Console.WriteLine("\nWhat type is this menu item?");
                        Console.WriteLine("[1]Drink");
                        Console.WriteLine("[2]Food");

                        string typeSelection = Console.ReadLine();
                        if (typeSelection == "1")
                        {
                            type = true;
                            break;
                        }
                        else if (typeSelection == "2")
                        {
                            type = false;
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid choice. Please enter 1 for drink or 2 for food:");
                        }
                    }

                    selectedSnack.Type = type;
                }

                Console.Clear();

                MenuItemLogic.UpdateMenuItem(selectedSnack);

                Console.WriteLine("\nMenu item updated successfully!");
                while (true)
                {
                    Console.WriteLine("\nWould you like to edit another item?");
                    Console.WriteLine("[1]Yes");
                    Console.WriteLine("[2]Go back to Snack Menu");

                    string againChoice = Console.ReadLine();
                    if (againChoice == "1")
                    {
                        continueEditing = false;
                        EditMenuItem(acc);
                        break;
                    }
                    else if (againChoice == "2")
                    {
                        Console.WriteLine("\nReturning to Snack Menu...");
                        Thread.Sleep(2000);
                        continueEditing = false;
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid input. Try again");
                    }
                }

            }
            else if (choice == snacks.Count + 1)
            {
                Console.Clear();
                AdminSnackMenu(acc);
            }
            else
            {
                Console.WriteLine("\nInvalid selection. Please try again.");
                Thread.Sleep(2000);
                EditMenuItem(acc);
            }
        }
    }

    //method that loops over whatever input you give it and checks if every char is a letter, used for name validation
    private static bool OnlyLetters(string input)
    {
        string removeSpaces = input.Replace(" ", "");

        foreach (char i in removeSpaces)
        {
            if (!char.IsLetter(i))
            {
                Console.WriteLine("Invalid input. You can only use letters.");
                return false;
            }
        }
        return true;
    }

    public static void SnackAdminOverview(UserModel acc)
    {
        List<ReservationModel> allReservations = ReservationAccess.GetAllReservations();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nSnack Popularity Menu:");
            Console.WriteLine("[1] See snacks per reservation");
            Console.WriteLine("[2] See snacks per show");
            Console.WriteLine("[3] See snacks per day");
            Console.WriteLine("[4] Go back to admin menu");
            Console.WriteLine("[5] Log out");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Clear();
                DisplaySnacksPerReservation(allReservations);
            }
            else if (choice == "2")
            {
                Console.Clear();
                DisplaySnacksPerShow(allReservations);
            }
            else if (choice == "3")
            {
                Console.Clear();
                DisplaySnacksPerDay(allReservations);
            }
            else if (choice == "4")
            {
                Console.Clear();
                Admin.Start(acc);
                return;
            }
            else if (choice == "5")
            {
                Console.Clear();
                Menu.Start();
            }
            else
            {
                Console.WriteLine("Invalid option. Please enter 1, 2, 3, or 4.");
                Thread.Sleep(2000);
            }
        }
    }

    public static void DisplaySnacksPerReservation(List<ReservationModel> allReservations)
    {
        Console.Clear();
        Console.WriteLine("===== Snacks per Reservation =====\n");

        List<MenuItem> menuItems = MenuItemLogic.GetAllMenuItems();

        foreach (var reservation in allReservations)
        {
            int userId = reservation.UserId;
            UserLogic userLogic = new UserLogic();
            UserModel currentUser = userLogic.GetById(userId);

            if (currentUser == null)
            {
                Console.WriteLine($"  No user found for User ID: {userId}");
                continue;
            }

            string snacks = reservation.Snacks;
            if (string.IsNullOrEmpty(snacks))
            {
                continue;
            }

            Console.WriteLine($"Reservation ID: {reservation.Id}");
            Console.WriteLine($"  User: {currentUser.FirstName} {currentUser.LastName}");

            string[] snackList = snacks.Split(',');
            Dictionary<string, int> snackCounts = new Dictionary<string, int>();
            decimal totalCost = 0;

            foreach (string snack in snackList.Select(s => s.Trim()))
            {
                if (snackCounts.ContainsKey(snack))
                {
                    snackCounts[snack]++;
                }
                else
                {
                    snackCounts[snack] = 1;
                }
            }

            bool hasOrderedSnacks = false;
            foreach (var snack in snackCounts)
            {
                MenuItem menuItem = null;

                foreach (var item in menuItems)
                {
                    if (item.Name == snack.Key)
                    {
                        menuItem = item;
                        break;
                    }
                }

                if (menuItem != null)
                {
                    hasOrderedSnacks = true;
                    decimal cost = menuItem.Price * snack.Value;
                    totalCost += cost;

                    Console.WriteLine($"     {snack.Value} x {menuItem.Name} - €{cost:F2}");
                }
                else
                {
                    Console.WriteLine($"     {snack.Value} x {snack.Key} (Price not found)");
                }
            }

            if (hasOrderedSnacks)
            {
                Console.WriteLine($"     Total Cost: €{totalCost:F2}\n");
            }
        }

        while (true)
        {
            Console.WriteLine("\n[1] Back to Snack Menu");
            Console.WriteLine("[2] Log out");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                return;
            }
            else if (choice == "2")
            {
                Console.Clear();
                Menu.Start();
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
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
        }
    }

    public static void DisplaySnacksPerShow(List<ReservationModel> allReservations)
    {
        Console.Clear();
        Console.WriteLine("===== Snacks per Show =====\n");

        var groupedByShow = allReservations
            .GroupBy(reservation => reservation.ShowId)
            .ToList();

        List<MenuItem> menuItems = MenuItemLogic.GetAllMenuItems();

        foreach (var group in groupedByShow)
        {
            int showId = group.Key;

            ShowModel show = ShowLogic.GetByID(showId);
            MoviesModel movie = MoviesLogic.GetById((int)show.MovieId);

            if (show != null && movie != null)
            {
                Dictionary<string, int> snackCountsForShow = new Dictionary<string, int>();

                foreach (var reservation in group)
                {
                    string snacks = reservation.Snacks;
                    if (!string.IsNullOrEmpty(snacks))
                    {
                        string[] snackList = snacks.Split(',');
                        foreach (string snack in snackList.Select(s => s.Trim()))
                        {
                            if (snackCountsForShow.ContainsKey(snack))
                            {
                                snackCountsForShow[snack]++;
                            }
                            else
                            {
                                snackCountsForShow[snack] = 1;
                            }
                        }
                    }
                }

                Console.WriteLine($"Show Date: {show.Date}");
                Console.WriteLine($"Movie Title: {movie.Title}");
                Console.WriteLine($"Theatre Room: {show.TheatreId}");

                decimal totalCost = 0;
                foreach (var snack in snackCountsForShow)
                {
                    MenuItem menuItem = menuItems.FirstOrDefault(item => item.Name == snack.Key);

                    if (menuItem != null)
                    {
                        decimal cost = menuItem.Price * snack.Value;
                        totalCost += cost;
                        Console.WriteLine($"     {snack.Value} x {menuItem.Name} - €{cost:F2}");
                    }
                    else
                    {
                        Console.WriteLine($"     {snack.Value} x {snack.Key} (Price not found)");
                    }
                }

                Console.WriteLine($"    Total Cost: €{totalCost:F2}\n");
            }
        }

        while (true)
        {
            Console.WriteLine("\n[1] Back to Snack Menu");
            Console.WriteLine("[2] Log out");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                return;
            }
            else if (choice == "2")
            {
                Console.Clear();
                Menu.Start();
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
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
        }
    }

    public static void DisplaySnacksPerDay(List<ReservationModel> allReservations)
    {
        Console.Clear();
        Console.WriteLine("===== Snacks per Day =====\n");

        List<MenuItem> menuItems = MenuItemLogic.GetAllMenuItems();


        var groupedByShow = allReservations
            .GroupBy(reservation => reservation.ShowId)
            .ToList();

        foreach (var group in groupedByShow)
        {
            int showId = group.Key;


            ShowModel show = ShowLogic.GetByID(showId);
            if (show == null) continue;

            DateTime showDate = DateTime.Parse(show.Date);
            Console.WriteLine($"Date: {showDate.ToShortDateString()}");

            Dictionary<string, int> snackCountsForDay = new Dictionary<string, int>();
            decimal totalCost = 0;

            foreach (var reservation in group)
            {
                string snacks = reservation.Snacks;
                if (!string.IsNullOrEmpty(snacks))
                {
                    string[] snackList = snacks.Split(',');
                    foreach (string snack in snackList.Select(s => s.Trim()))
                    {
                        if (snackCountsForDay.ContainsKey(snack))
                        {
                            snackCountsForDay[snack]++;
                        }
                        else
                        {
                            snackCountsForDay[snack] = 1;
                        }
                    }
                }
            }

            foreach (var snack in snackCountsForDay)
            {
                MenuItem menuItem = menuItems.FirstOrDefault(item => item.Name == snack.Key);

                if (menuItem != null)
                {
                    decimal cost = menuItem.Price * snack.Value;
                    totalCost += cost;
                    Console.WriteLine($"     {snack.Value} x {menuItem.Name} - €{cost:F2}");
                }
                else
                {
                    Console.WriteLine($"     {snack.Value} x {snack.Key} (Price not found)");
                }
            }

            Console.WriteLine($"    Total Cost for {showDate.ToShortDateString()}: €{totalCost:F2}\n");
        }

        while (true)
        {
            Console.WriteLine("\n[1] Back to Snack Menu");
            Console.WriteLine("[2] Log out");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                return;
            }
            else if (choice == "2")
            {
                Console.Clear();
                Menu.Start();
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
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
        }
    }
}