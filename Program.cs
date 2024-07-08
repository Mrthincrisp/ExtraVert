using System.Numerics;


        List<Plants> plants = new List<Plants>
        {
            new Plants()
            {
                Species = "Venus Fly Trap",
                LightNeeds = 4,
                AskingPrice = 8.99m,
                City = "Columbia",
                ZIP = "38401",
                Sold = false
            },
            new Plants()
            {
                Species = "Pitcher Plants",
                LightNeeds = 3,
                AskingPrice = 10.99m,
                City = "Columbia",
                ZIP = "38401",
                Sold = false
            },
            new Plants()
            {
                Species = "Sundew",
                LightNeeds = 5,
                AskingPrice = 12.99m,
                City = "Nashville",
                ZIP = "54876",
                Sold = true
            },
            new Plants()
            {
                Species = "Waterwheel plant",
                LightNeeds = 2,
                AskingPrice = 12.73m,
                City = "Columbia",
                ZIP = "38401",
                Sold = true
            },
            new Plants()
            {
                Species = "Bladderwort",
                LightNeeds = 5,
                AskingPrice = 8.62m,
                City = "Nashville",
                ZIP = "54876",
                Sold = false
            }
        };

        string greeting = @"Welcome to the Voracious Verde~
Please make a selection:";

string choice = null;
while (choice != "0")
{
    Console.WriteLine(@$"{greeting}
                        0. Exit
                        1. Display all plants
                        2. Post a plant to be adopted
                        3. Adopt a plant
                        4. Delist a plant");
    choice = Console.ReadLine();
    switch (choice)
    {
        case "0":
            Console.WriteLine("Thank you for viting Voracious Verde please hit anykey to leave, and come again soon.");
            Console.ReadKey();
            Console.Clear();
            break;
        case "1":
            Console.WriteLine("a list"); // TODO
            break;
        case "2":
            Console.WriteLine("Who are you leaving with us?"); // TODO
            break;
        case "3":
            Console.WriteLine("Who are you taking home with you today?"); //TODO
            break;
        case "4":
            Console.WriteLine("Select a plant to remove"); //TODO
            break;
        default: 
            Console.WriteLine("PLEASE MAKE A SELECTION BETWEEN 0-4");
            break;
    }
}