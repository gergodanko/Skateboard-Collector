using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SkateboardCollector.api;
using System.Windows;
using SkateboardCollector.api.Exceptions;
using SkateboardCollector.api.Competitions;
namespace SkateboardCollector.cmd
{
    class UI
    {
        /// <summary>
        /// True if the user is in the competition menu.
        /// </summary>
        private bool inCompetition;
        /// <summary>
        /// Prints out the menu.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="list"></param>
        /// <param name="exitmessage"></param>
        public void PrintMenu(string title, string[] list, string exitmessage)
        {
            Console.Clear();
            Console.WriteLine(title + $":{Environment.NewLine}");
            int counter = 0;
            foreach (string option in list)
            {
                counter++;
                Console.WriteLine(" (" + Convert.ToString(counter) + ") " + option);
            }
            Console.WriteLine(" (0) " + exitmessage);
        }

        /// <summary>
        /// Prints out the screen which shows up after starting .
        /// </summary>
        public void StartScreen()
        {
            Console.WriteLine("Welcome to Skateboard Collector!\n");
            TimeSpan ts = new TimeSpan(0, 0, 3);
            Thread.Sleep(ts);
            Console.Clear();
        }

        /// <summary>
        /// Contains the menupoints of the competition menu, and prints them out.
        /// </summary>
        public void HandleCompetitionMenu()
        {
            string[] options = new string[]
            {
                "Almost's Street SKATE Competition",
                "Baker's Whacky Hillbomb Race",
                "Race against the Elements, Halfpipe Competition",
                "Enjoi's Megaramp Competition",
                "Women's Halfpipe Competition, Powered By Girl"
            };
            PrintMenu("Competitions menu", options, "Back to main menu");
        }

        /// <summary>
        /// Contains the menu points of the main menu and prints them out.
        /// </summary>
        public void HandleMenu()
        {
            string[] options = new string[]
            {
                    "Create a new skateboard",
                    "Print out existing skateboards",
                    "Update a hardware on your skateboard",
                    "Delete a skateboard",
                    "Competitions"
            };
            PrintMenu("Main menu", options, "Exit program");
        }

        /// <summary>
        /// Lets the user choose between menu points .
        /// </summary>
        public void Choose()
        {
            Console.WriteLine("\nPlease enter a number: ");
            string option = Console.ReadLine();
            Storage storage = new Storage();
            storage.LoadXML();
            switch (option)
            {
                //Create a new skateboard
                case "1":
                    Skateboard skateboard;
                    while (true)
                    {
                        try
                        {
                            skateboard = storage.SkateboardAssembly();
                            break;
                        }
                        catch (NameExistsException)
                        {
                            UI.PrintErrorMessage("You already have a board with the same name!");
                        }
                    }
                    storage.Skateboards.Add(skateboard);
                    storage.Save();
                    break;
                    //Print out existing skateboards
                case "2":
                    {
                        while (true)
                        {
                            Console.Clear();
                            PrintAllBoards(storage);
                            Console.WriteLine("Press enter to exit!");
                            Console.ReadKey();
                            break;
                        }
                        break;
                    }
                    //Update skateboards
                case "3":
                    {
                        while (true)
                        {
                            try
                            {
                                Console.Clear();
                                PrintAllBoards(storage);
                                storage.SkateboardUpdate();
                                PrintAllBoards(storage);
                                Console.WriteLine("Do you want to continue updating your skateboards?(y/n)");
                                string choice = Console.ReadLine();
                                if (choice.Equals("n") || choice.Equals("N"))
                                {
                                    storage.Save();
                                    break;
                                }
                            }
                            catch (NoMatchingBoardException)
                            {
                                PrintErrorMessage("There is no board with the given name.");
                                TimeSpan ts = new TimeSpan(0, 0, 2);
                                Thread.Sleep(ts);
                                Console.Clear();
                                PrintAllBoards(storage);
                                Console.WriteLine("Do you want to continue updating your skateboards?(y/n)");
                                string choice = Console.ReadLine();
                                if (choice.Equals("n") || choice.Equals("N"))
                                {
                                    break;
                                }
                                continue;
                            }
                        }
                        break;
                    }
                    //Delete a skateboard
                case "4":
                    {
                        while (true)
                        {
                            try
                            {
                                PrintAllBoards(storage);
                                Console.WriteLine("Type in the name of the skateboard you want to delete");
                                string name = Console.ReadLine();
                                storage.DeleteSkateboard(name);
                                storage.Save();
                                break;
                            }
                            catch (NoMatchingBoardException)
                            {
                                PrintErrorMessage("There is no board with the given name.");
                                TimeSpan ts = new TimeSpan(0, 0, 2);
                                Thread.Sleep(ts);
                            }
                        }
                        break;
                    }
                    //Open competition menu
                case "5":
                    {
                        inCompetition = true;
                        while (inCompetition)
                        {
                            HandleCompetitionMenu();
                            try
                            {
                                ChooseACompetition(storage);
                            }
                            catch (InvalidOperationException)
                            {
                                Console.WriteLine("");
                            }
                        }
                        break;
                    }
                    //Exit the program
                case "0":
                    {
                        TimeSpan ts = new TimeSpan(0, 0, 2);
                        Console.WriteLine("Exiting...");
                        Thread.Sleep(ts);
                        Environment.Exit(0);
                        break;
                    }
                    //Wrong input
                default:
                    {
                        Console.WriteLine("Wrong input! Try again.");
                        Console.WriteLine("Press enter to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
            }
        }

        /// <summary>
        /// Competition menu. Lets the user choose between competitions
        /// </summary>
        /// <param name="storage"></param>
        public void ChooseACompetition(Storage storage)
        {
            Console.Clear();
            HandleCompetitionMenu();
            Console.WriteLine("\nChoose a competition by entering a number: ");
            string compOption = Console.ReadLine();
            switch (compOption) 
            {
                    //Almost competition
                case "1":
                    {
                        AlmostCompetition almostCompetition = new AlmostCompetition();
                        try
                        {
                            Console.Clear();
                            Compete(storage, almostCompetition);
                        }
                        catch (NotEligibleException)
                        {
                            PrintErrorMessage("That board is not eligible for this competition!");
                        }
                        break;
                    }
                    //Baker competition
                case "2":
                    {
                        BakerCompetition bakerCompetition = new BakerCompetition();
                        try
                        {
                            Console.Clear();
                            Compete(storage, bakerCompetition);
                        }
                        catch (NotEligibleException)
                        {
                            PrintErrorMessage("That board is not eligible for this competition!");
                        }
                        break;
                    }
                    //Element competition
                case "3":
                    {
                        ElementCompetition elementCompetition = new ElementCompetition();
                        try
                        {
                            Console.Clear();
                            Compete(storage, elementCompetition);
                        }
                        catch (NotEligibleException)
                        {
                            PrintErrorMessage("That board is not eligible for this competition!");
                        }
                        break;
                    }
                    //Enjoi competition
                case "4":
                    {
                        EnjoiCompetition enjoiCompetition = new EnjoiCompetition();
                        try
                        {
                            Console.Clear();
                            Compete(storage, enjoiCompetition);
                        }
                        catch (NotEligibleException)
                        {
                            PrintErrorMessage("That board is not eligible for this competition!");
                        }
                        break;
                    }
                    //Girl competition
                case "5":
                    {
                        GirlCompetition girlCompetition = new GirlCompetition();
                        try
                        {
                            Console.Clear();
                            Compete(storage, girlCompetition);
                        }
                        catch (NotEligibleException)
                        {
                            PrintErrorMessage("You are not eligible for this competition!");
                        }
                        break;
                    }
                    //Back to main menu
                case "0":
                    {
                        inCompetition = false;
                        break;
                    }
                    //Wrong input 
                default:
                    {
                        Console.WriteLine("Wrong input! Try again.");
                        Console.WriteLine("Press enter to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
            }
            
            
        }

        /// <summary>
        /// Simulates a competition that is given as parameter. 
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="competition"></param>
        public void Compete(Storage storage, Competition competition)
        {
            TimeSpan ts = new TimeSpan(0, 0, 2);
            Skateboard competitor;
            while (true)
            {
                try
                {
                    competitor = ChooseBoard(storage, competition.GetRequirements());
                    break;
                }
                catch (NoMatchingBoardException)
                {
                    PrintErrorMessage("There is no board with the given name.");
                    Thread.Sleep(ts);
                }
            }
            if (competition.EligibleForCompetition(competitor) == true)
            {
                if (competition.Race(storage,competitor) == true)
                {
                    Console.WriteLine("Congratulations, you won!");
                    Thread.Sleep(ts);
                }
                else{
                    Console.WriteLine("You lost the competition this time.");
                    Thread.Sleep(ts);
                }
            }
            else
            {
                throw new NotEligibleException();
            }
        }

        /// <summary>
        /// Prints out parts from the csv file. It can print out any type of parts or all of the parts, it depends on the chosen part parameter.
        /// chosenPart can be : "Decks","Wheels","Trucks","Griptapes","Bearings","All"
        /// </summary>
        /// <param name="csvFile"></param>
        /// <param name="chosenPart"></param>
        public static void PrintAllParts(CsvReader csvFile, string chosenPart)
        {
            switch (chosenPart) {
                case "Decks":
                    {
                        Console.WriteLine("Decks");
                        for (int i = 0; i < csvFile.Decks.Length; i++)
                        {
                            Console.WriteLine(csvFile.Decks[i].ToString());
                        }
                        break;
                    }
                case "Wheels":
                    {
                        Console.WriteLine("Wheels");
                        for (int i = 0; i < csvFile.Wheels.Length; i++)
                        {
                            Console.WriteLine(csvFile.Wheels[i].ToString());
                        }
                        break;
                    }
                case "Trucks":
                    {
                        Console.WriteLine("Trucks");
                        for (int i = 0; i < csvFile.Trucks.Length; i++)
                        {
                            Console.WriteLine(csvFile.Trucks[i].ToString());
                        }
                        break;
                    }
                case "Griptapes":
                    {
                        Console.WriteLine("Griptapes");
                        for (int i = 0; i < csvFile.Griptapes.Length; i++)
                        {
                            Console.WriteLine(csvFile.Griptapes[i].ToString());
                        }
                        break;
                    }
                case "Bearings":
                    {
                        Console.WriteLine("Bearings");
                        for (int i = 0; i < csvFile.Bearings.Length; i++)
                        {
                            Console.WriteLine(csvFile.Bearings[i].ToString());
                        }
                        break;
                    }
                case "All":
                    {
                        Console.WriteLine("Decks");
                        for (int i = 0; i < csvFile.Decks.Length; i++)
                        {
                            Console.WriteLine(csvFile.Decks[i].ToString());
                        }
                        Console.WriteLine("Wheels");
                        for (int i = 0; i < csvFile.Wheels.Length; i++)
                        {
                            Console.WriteLine(csvFile.Wheels[i].ToString());
                        }
                        Console.WriteLine("Trucks");
                        for (int i = 0; i < csvFile.Trucks.Length; i++)
                        {
                            Console.WriteLine(csvFile.Trucks[i].ToString());
                        }
                        Console.WriteLine("Griptapes");
                        for (int i = 0; i < csvFile.Griptapes.Length; i++)
                        {
                            Console.WriteLine(csvFile.Griptapes[i].ToString());
                        }
                        Console.WriteLine("Bearings");
                        for (int i = 0; i < csvFile.Bearings.Length; i++)
                        {
                            Console.WriteLine(csvFile.Bearings[i].ToString());
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Asks for a given attribute of a given hardware and returns the user's input as a string.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="hardwareType"></param>
        /// <returns></returns>
        public static string AskForHardware(string attribute, string hardwareType)
        {
            Console.WriteLine($"Type in the {attribute} of {hardwareType} you want to choose from the list: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Asks the user to name their board.
        /// </summary>
        /// <returns></returns>
        public static string GetNameOfBoard()
        {
            Console.WriteLine("Name for your board: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Prints out a string.
        /// </summary>
        /// <param name="message"></param>
        public static void Printer(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Asks for input.
        /// </summary>
        /// <returns></returns>
        public static string Scanner()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Prints out an error message which stays on screen for 2 seconds.
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void PrintErrorMessage(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            TimeSpan ts = new TimeSpan(0, 0, 2);
            Thread.Sleep(ts);
        }

        /// <summary>
        /// Prints out the existing skateboard and the requirements for a competition and asks the user for a board then returns the board with that name.
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="requirements"></param>
        /// <returns></returns>
        private Skateboard ChooseBoard(Storage storage, string requirements)
        {
            PrintAllBoards(storage);
            Console.WriteLine(requirements);
            Console.WriteLine("Choose a board by entering its name:");
            return storage.GetSkateboardByName(Console.ReadLine());
        }

        /// <summary>
        /// Print pattern for skateboards
        /// </summary>
        /// <param name="storage"></param>
        public static void PrintAllBoards(Storage storage)
        {
            foreach(Skateboard skateboard in storage.Skateboards)
            {
                Console.WriteLine("Name: "+skateboard.name);
                Console.WriteLine("Deck: "+skateboard.deck.ToString());
                Console.WriteLine("Truck: "+skateboard.truck.ToString());
                Console.WriteLine("Griptape: "+skateboard.griptape.ToString());
                Console.WriteLine("Wheel: "+skateboard.wheel.ToString());
                Console.WriteLine("Bearings: "+skateboard.bearings.ToString());
                Console.WriteLine("\n");
            }
        }
    }
}
