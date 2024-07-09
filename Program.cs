using System.Numerics;


internal class Program
{
    private static void Main(string[] args)
    {
        List<Plants> plants =
        [
            new("Venus Fly Trap", 4, 8.99m, "Columbia", "38401", false),
            new("Pitcher Plants", 3, 10.99m, "Columbia", "38401", false),
            new("Sundew", 5, 12.99m, "Nashville", "54876", true),
            new("Waterwheel plant", 2, 12.73m, "Columbia", "38401", true),
            new("Bladderwort", 5, 8.62m, "Nashville", "54876", false)
        ];

        // Random is used to select a plant of the day displayed in the greeting
        Random random = new();
        Plants? PotD = null;
        while (PotD == null || PotD.Sold)
        {
            int randomPlant = random.Next(1, plants.Count);
            PotD = plants[randomPlant];
        }


        string greeting = @$"Welcome to the Voracious Verde~
The Plant of the day is {PotD.Species}!
Please make a selection:";

        string? choice = null;
        while (choice != "0")
        {
            Console.WriteLine(@$"{greeting}
                        0. Exit
                        1. Display all plants
                        2. Post a plant to be adopted
                        3. Adopt a plant
                        4. Delist a plant
                        5. Search for light specific needs");
            choice = Console.ReadLine();
            switch (choice)
            {
                case "0":
                    Console.WriteLine("Thank you for viting Voracious Verde please hit anykey to leave, and come again soon.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "1":
                    displayAllPlants();
                    break;
                case "2":
                    PostPlant(plants);
                    break;
                case "3":
                    AdoptAPlant(plants);
                    break;
                case "4":
                    DelistAPlant(plants);
                    break;
                case "5":
                    PlantSearch(plants);
                    break;
                default:
                    Console.WriteLine("PLEASE MAKE A SELECTION BETWEEN 0-4");
                    break;
            }
        }

        //method used for the first selection in the menu
        void displayAllPlants()
        {
            for (int i = 0; i < plants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold" : "is available")} for ${plants[i].AskingPrice}.");
            }
        }

        void PostPlant(List<Plants> plants)
        {
            Console.WriteLine("Enter the plant's details...");
            Console.WriteLine("Plant species:");
            string? species = Console.ReadLine();

            Console.WriteLine("How much light? enter a number 1-5. 1 minimal light 5 most light");
            int lightNeeds;
            while (!int.TryParse(Console.ReadLine(), out lightNeeds) || lightNeeds < 1 || lightNeeds > 5)
            {
                Console.WriteLine("Please only enter a value 1-5.");
                Console.WriteLine("How much light? enter a number 1-5. 1 minimal light 5 most light");
            }

            Console.WriteLine("What is the asking price? (0.00)");
            decimal askingPrice;
            while (!decimal.TryParse(Console.ReadLine(), out askingPrice))
            {
                Console.WriteLine("that is not a valid number please enter a format of 1.23");
                Console.WriteLine("What is the asking price? (0.00)");
            }

            Console.WriteLine("City: ");
            string? city = Console.ReadLine();

            Console.WriteLine("Zip code: ");
            string? zipCode = Console.ReadLine();

            bool sold = false;

            Plants newPlant = new(species, lightNeeds, askingPrice, city, zipCode, sold);

            plants.Add(newPlant);
            Console.WriteLine("Plant added.");
        }

        void AdoptAPlant(List<Plants> plants)
        {
            for (int i = 0; i < plants.Count; i++)
            {
                if (!plants[i].Sold)
                {
                    Console.WriteLine($"If you'd like to adopt {plants[i].Species}, enter {i}");
                }
            }
            Console.Write("Enter the plant's number you'd like to adopt: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < plants.Count)
            {
                plants[index] = plants[index] with { Sold = true };
                Console.WriteLine($"You have adopted {plants[index].Species}.");
            }
            else
            {
                Console.WriteLine("Invalid input or the plant is already sold.");
            }
        }

        void DelistAPlant(List<Plants> plants)
        {
            Console.WriteLine("To remove a plant enter thier number, or enter q, to return to the main menu.");
            for (int i = 0; i < plants.Count; i++)
            {
                Console.WriteLine($"For {plants[i].Species} enter {i}.");
            }
            string? input = Console.ReadLine();

            if (input == "q")
            {
                return;
            }

            if (int.TryParse(input, out int index) && index >= 0 && index < plants.Count)
            {
                Console.WriteLine($"The plant {plants[index].Species} has been removed.");
                plants.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

        }

        void PlantSearch(List<Plants> plants)
        {
            Console.WriteLine($"Please enter a number from 1 to 5.");
            if (int.TryParse(Console.ReadLine().Trim(), out int searchValue) && searchValue >= 1 && searchValue <= 5)
            {
                var plantlights = plants.Where(i => i.LightNeeds == searchValue).ToList();

                foreach(var plant in plantlights)
                {
                    Console.WriteLine($"{plant.Species}");
                }
            }
            else
            {
                Console.WriteLine("1 through 5 only");
            }

        }
    }
}