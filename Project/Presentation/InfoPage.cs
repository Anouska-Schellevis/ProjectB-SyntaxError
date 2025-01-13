class InfoPage
{
    static public void Start()
    {
        Console.Clear();
        Console.WriteLine("===== Info Page =====\n");
        while (true)
        {
            Console.WriteLine("This cinema was invented by Jake Darcy. Even though there was a corona crisis in 2021, ");
            Console.WriteLine("he decided to start a typical Dutch cinema chain. With the different kind of technologies ");
            Console.WriteLine("going to the movies becomes a movie experience and louging is the key word. You can even ");
            Console.WriteLine("reserve a place at the bar to get a drink after the movie.\n");

            int count = 1;
            List<LocationModel> allLocations = LocationAccess.GetAllLocations();
            foreach (LocationModel location in allLocations)
            {
                Console.WriteLine($"Location {count}: {location.Address}, {location.PostalCode} in {location.City}");
                Console.WriteLine("Opening hours: 10:00 uur - 00:00 uur\n");
                count++;
            }
            Console.WriteLine("[B]Go back");

            string input = Console.ReadLine().ToLower();

            if (input == "b")
            {
                Console.Clear();
                Menu.Start();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please choose [B] to go back to the cinema menu.");
                Thread.Sleep(2000);
                Start();
            }

            
        }
    }
}