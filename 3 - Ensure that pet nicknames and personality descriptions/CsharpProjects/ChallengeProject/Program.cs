using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // Constants
        const int MaxPets = 8;

        // Arrays and Variables
        string[,] ourAnimals = new string[MaxPets, 7];
        string menuSelection;

        // Sample Data Initialization
        InitializeSampleData(ourAnimals, MaxPets);

        // Top-level menu options
        do
        {
            Console.Clear();
            DisplayMainMenu();

            string readResult = Console.ReadLine();
            menuSelection = readResult?.ToLower() ?? "";

            // Switch-case to process the selected menu option
            switch (menuSelection)
            {
                case "1":
                    ListAllPetInfo(ourAnimals, MaxPets);
                    break;

                case "2":
                    SearchDogsByCharacteristics(ourAnimals, MaxPets);
                    break;

                default:
                    break;
            }
        } while (menuSelection != "exit");
    }

    // Initialize sample data in the ourAnimals array
    static void InitializeSampleData(string[,] ourAnimals, int maxPets)
    {
        for (int i = 0; i < maxPets; i++)
        {
            string animalSpecies, animalID, animalAge, animalPhysicalDescription, animalPersonalityDescription, animalNickname, suggestedDonation;

            switch (i)
            {
                // Sample data entries
                case 0:
                    animalSpecies = "dog";
                    animalID = "d1";
                    animalAge = "2";
                    animalPhysicalDescription = "medium sized cream colored female golden retriever weighing about 45 pounds. housebroken.";
                    animalPersonalityDescription = "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.";
                    animalNickname = "lola";
                    suggestedDonation = "85.00";
                    break;

                case 1:
                    animalSpecies = "dog";
                    animalID = "d2";
                    animalAge = "9";
                    animalPhysicalDescription = "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.";
                    animalPersonalityDescription = "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.";
                    animalNickname = "gus";
                    suggestedDonation = "49.99";
                    break;

                case 2:
                    animalSpecies = "cat";
                    animalID = "c3";
                    animalAge = "1";
                    animalPhysicalDescription = "small white female weighing about 8 pounds. litter box trained.";
                    animalPersonalityDescription = "friendly";
                    animalNickname = "snow";
                    suggestedDonation = "40.00";
                    break;

                case 3:
                    animalSpecies = "cat";
                    animalID = "c4";
                    animalAge = "";
                    animalPhysicalDescription = "";
                    animalPersonalityDescription = "";
                    animalNickname = "lion";
                    suggestedDonation = "";
                    break;

                default:
                    animalSpecies = animalID = animalAge = animalPhysicalDescription = animalPersonalityDescription = animalNickname = suggestedDonation = "";
                    break;
            }

            // Populate ourAnimals array
            ourAnimals[i, 0] = "ID #: " + animalID;
            ourAnimals[i, 1] = "Species: " + animalSpecies;
            ourAnimals[i, 2] = "Age: " + animalAge;
            ourAnimals[i, 3] = "Nickname: " + animalNickname;
            ourAnimals[i, 4] = "Physical description: " + animalPhysicalDescription;
            ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;

            // Handle suggested donation parsing
            decimal decimalDonation;
            if (!decimal.TryParse(suggestedDonation, out decimalDonation))
            {
                decimalDonation = 45.00m; // Default to 45.00 if suggestedDonation is not a number
            }
            ourAnimals[i, 6] = $"Suggested Donation: {decimalDonation:C2}";
        }
    }

    // Display the main menu options
    static void DisplayMainMenu()
    {
        Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
        Console.WriteLine(" 1. List all of our current pet information");
        Console.WriteLine(" 2. Display all dogs with a specified characteristic");
        Console.WriteLine();
        Console.WriteLine("Enter your selection number (or type Exit to exit the program)");
    }

    // List all pet information
    static void ListAllPetInfo(string[,] ourAnimals, int maxPets)
    {
        for (int i = 0; i < maxPets; i++)
        {
            if (ourAnimals[i, 0] != "ID #: ")
            {
                Console.WriteLine();
                for (int j = 0; j < 7; j++)
                {
                    Console.WriteLine(ourAnimals[i, j]);
                }
            }
        }
        Console.WriteLine("\r\nPress the Enter key to continue");
        Console.ReadLine();
    }

    // Search dogs by specified characteristics
    static void SearchDogsByCharacteristics(string[,] ourAnimals, int maxPets)
    {
        string dogCharacteristics = "";
        while (dogCharacteristics == "")
        {
            Console.WriteLine($"\nEnter dog characteristics to search for separated by commas");
            string readResult = Console.ReadLine();
            dogCharacteristics = readResult?.ToLower() ?? "";
            Console.WriteLine();
        }

        string[] dogSearches = dogCharacteristics.Split(",");
        for (int i = 0; i < dogSearches.Length; i++)
        {
            dogSearches[i] = dogSearches[i].Trim();
        }

        Array.Sort(dogSearches);
        string[] searchingIcons = { " |", " /", "--", " \\", " *" };

        bool matchesAnyDog = false;
        string dogDescription = "";

        for (int i = 0; i < maxPets; i++)
        {
            if (ourAnimals[i, 1].Contains("dog"))
            {
                dogDescription = ourAnimals[i, 4] + "\n" + ourAnimals[i, 5];
                bool matchesCurrentDog = false;

                foreach (string term in dogSearches)
                {
                    if (!string.IsNullOrWhiteSpace(term))
                    {
                        for (int j = 2; j > -1; j--)
                        {
                            foreach (string icon in searchingIcons)
                            {
                                Console.Write($"\rsearching our dog {ourAnimals[i, 3]} for {term.Trim()} {icon} {j}");
                                Thread.Sleep(100);
                            }
                            Console.Write($"\r{new String(' ', Console.BufferWidth)}");
                        }

                        if (dogDescription.Contains($" {term.Trim()} "))
                        {
                            Console.WriteLine($"\rOur dog {ourAnimals[i, 3]} matches your search for {term.Trim()}");
                            matchesCurrentDog = true;
                            matchesAnyDog = true;
                        }
                    }
                }

                if (matchesCurrentDog)
                {
                    Console.WriteLine($"\r{ourAnimals[i, 3]} ({ourAnimals[i, 0]})\n{dogDescription}\n");
                }
            }
        }

        if (!matchesAnyDog)
        {
            Console.WriteLine("None of our dogs are a match found for: " + dogCharacteristics);
        }

        Console.WriteLine("\n\rPress the Enter key to continue");
        Console.ReadLine();
    }
}
