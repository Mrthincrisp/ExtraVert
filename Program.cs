using System.Drawing;
using System.Numerics;


internal class Program
{
    private static void Main(string[] args)
    {
        List<Plants> plants =
        [
            new("Venus Fly Trap", 4, 8.99m, "Columbia", "38401", false, new DateTime(2024, 5, 1) ),
            new("Pitcher Plants", 3, 10.99m, "Columbia", "38401", false, new DateTime(2024, 4, 10)),
            new("Sundew", 5, 12.99m, "Nashville", "54876", true, new DateTime(2024, 6, 11)),
            new("Waterwheel plant", 2, 12.73m, "Columbia", "38401", true, new DateTime(2025, 7, 21)),
            new("Bladderwort", 5, 8.62m, "Nashville", "54876", false, new DateTime(2025, 7, 30))
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
                        5. Search for light specific needs
                        6. Voracious Stats");
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
                case "6":
                    PlantStatistics(plants);
                    break;
                default:
                    Console.WriteLine("PLEASE MAKE A SELECTION BETWEEN 0-4");
                    Console.Write("press any key to continue");
                    Console.ReadKey();
                    break;
            }
        }

        //method used for the first selection in the menu
        void displayAllPlants()
        {
            for (int i = 0; i < plants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold" : "is available")} for ${plants[i].AskingPrice}. This planty is no longer available after{plants[i].AvailableUntil}");
            }
            Console.Write("press any key to continue");
            Console.ReadKey();
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

            try
            {
                Console.WriteLine("When will this post expire?");
                int year, month, day;

                Console.WriteLine("Please enter a four digit year 0000:");
                while (!int.TryParse(Console.ReadLine().Trim(), out year) || year < 1 || year > DateTime.MaxValue.Year)
                {
                    Console.WriteLine("Invalid input. Please enter a valid year. (YYYY)");
                }

                Console.WriteLine("Month in 2 character format (MM).");
                while (!int.TryParse(Console.ReadLine().Trim(), out month) || month < 1 || month > 12)
                {
                    Console.WriteLine("Invalid input. Please enter a valid month. (MM)");
                }

                Console.WriteLine("Month in 2 character format (DD).");
                while (!int.TryParse(Console.ReadLine().Trim(), out day) || day < 1 || day > 31)
                {
                    Console.WriteLine("Invalid input. Please enter a valid day. (DD)");
                }

                DateTime date = new DateTime(year, month, day);
            Plants newPlant = new(species, lightNeeds, askingPrice, city, zipCode, sold, date);
            plants.Add(newPlant);
            Console.WriteLine("Plant added.");
            Console.Write("press any key to continue");
            Console.ReadKey();
            }
            catch (ArgumentOutOfRangeException) 
            {
                Console.WriteLine($"The date put in is not valid, please try again a vaild date would look like 2025 11 20");
                Console.Write("press any key to continue");
                Console.ReadKey();
            }


        }

        void AdoptAPlant(List<Plants> plants)
        {
            for (int i = 0; i < plants.Count; i++)
            {
                if (!plants[i].Sold && plants[i].AvailableUntil > DateTime.Now)
                {
                    Console.WriteLine($"If you'd like to adopt {plants[i].Species}, enter {i}");
                }
            }
            Console.Write("Enter the plant's number you'd like to adopt: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < plants.Count)
            {
                plants[index] = plants[index] with { Sold = true };
                Console.WriteLine($"You have adopted {plants[index].Species}.");
                Console.Write("press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid input or the plant is already sold.");
                Console.Write("press any key to return to the main menu");
                Console.ReadKey();
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
                Console.Write("press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write("press any key to return to the main menu");
                Console.ReadKey();
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
                    Console.Write("press any key to continue");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("1 through 5 only");
            }

        }

        void PlantStatistics(List<Plants> plants)
        {
            string cheapest = GetCheapestPlant(plants);
            int available = GetAvailablePlants(plants);
            string light = GetLight(plants);
            double averageLight = GetAverageLight(plants);
            double success = GetAdoptionRate(plants);

            Console.WriteLine($@"Stats
Cheapest Plant: {cheapest}
{available} plant(s) are available
{light} require the most light
the average light needs: {averageLight}
plant addoption rate: {success:P2}");
            Console.Write("press any key to continue");
            Console.ReadKey();

            string GetCheapestPlant(List<Plants> plants)
            {
                Plants cheapest = plants.OrderBy(i => i.AskingPrice).FirstOrDefault();
                return cheapest != null ? cheapest.Species : "nada";
            }   
            
            int GetAvailablePlants(List<Plants> plants)
            {
                int count = 0;
                for (int i = 0; i < plants.Count; i++)
                {
                    if (!plants[i].Sold && plants[i].AvailableUntil > DateTime.Now)
                    {
                        count++;
                    }
                }
                return count;
                        
            }

            string GetLight(List<Plants> plants) 
            {
                int MostLightRequired = plants.Max(item => item.LightNeeds);
                var lightPlantsList = plants.Where(item => item.LightNeeds == MostLightRequired);
                return string.Join(", ", lightPlantsList.Select(IThreadPoolWorkItem => IThreadPoolWorkItem.Species));
            }

            double GetAverageLight(List<Plants> plants)
            {
                return plants.Average(item => item.LightNeeds);
            }

            double GetAdoptionRate(List<Plants> plants) 
            {
                int totalPlants = plants.Count;
                int soldPlants = plants.Count(item => item.Sold);
                return (double)soldPlants / totalPlants;
            }
        }
    }
}