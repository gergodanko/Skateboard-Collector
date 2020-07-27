using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Globalization;
using SkateboardCollector.cmd;
using System.Threading;

namespace SkateboardCollector.api
{
    [Serializable()]
    class Storage
    {
        public List<Skateboard> Skateboards = new List<Skateboard>();

        /// <summary>
        /// Creates a new skateboard instance, with parts chosen by the user
        /// </summary>
        /// <returns></returns>
        public Skateboard SkateboardAssembly()
        {
            Console.Clear();
            CsvReader store = new CsvReader();
            //Loads hardware from the csv file
            store.LoadEverything();
            Skateboard skateboard = new Skateboard();
            //Checks if the given name already exists in the storage. Throws exception if it does.
            string name;
            while (true)
            {
                try
                {
                    name = CheckIfBoardNameExists(UI.GetNameOfBoard());
                    break;
                }
                catch (Exceptions.NameExistsException)
                {
                    UI.PrintErrorMessage("This name already exists.");
                }
            }
            skateboard.name = name;
            while (true)
            {
                Console.Clear();
                Hardware.Deck deck = DeckForAssembly(store);
                try
                {
                    //Gets the chosen deck from the store and puts it in deckReturnable .
                    Hardware.Deck deckReturnable = (Hardware.Deck)skateboard.PartInStore(store, deck);
                    if (!deckReturnable.Equals(null))
                    {
                        skateboard.deck = deckReturnable;
                        break;
                    }
                }
                catch (NullReferenceException)
                {
                    UI.PrintErrorMessage("There is no skateboard part with the given arguments. When typing a float number use a decimal point!");
                }
            }
            while (true)
            {
                Console.Clear();
                Hardware.Griptape griptape = GriptapeForAssembly(store);
                try
                {
                    Hardware.Griptape griptapeReturnable = (Hardware.Griptape)skateboard.PartInStore(store, griptape);
                    if (!griptapeReturnable.Equals(null))
                    {
                        skateboard.griptape = griptapeReturnable;
                        break;
                    }
                }
                catch (NullReferenceException)
                {
                    UI.PrintErrorMessage("There is no skateboard part with the given arguments. When typing a float number use a decimal point!");
                }
            }
            while (true)
            {
                Console.Clear();
                Hardware.Truck truck = TruckForAssembly(store);
                try
                {
                    Hardware.Truck truckReturnable = (Hardware.Truck)skateboard.PartInStore(store, truck);
                    if (!truckReturnable.Equals(null))
                    {
                        skateboard.truck = truckReturnable;
                        break;
                    }
                }
                catch (NullReferenceException)
                {
                    UI.PrintErrorMessage("There is no skateboard part with the given arguments. When typing a float number use a decimal point!");
                }
            }
            while (true)
            {
                Console.Clear();
                Hardware.Wheels wheels = WheelsForAssembly(store);
                try
                {
                    Hardware.Wheels wheelsReturnable = (Hardware.Wheels)skateboard.PartInStore(store, wheels);
                    if (!wheelsReturnable.Equals(null))
                    {
                        skateboard.wheel = wheelsReturnable;
                        break;
                    }
                }
                catch (NullReferenceException)
                {
                    UI.PrintErrorMessage("There is no skateboard part with the given arguments. When typing a float number use a decimal point!");
                }
            }
            while (true)
            {
                Console.Clear();
                Hardware.Bearings bearings = BearingsForAssembly(store);
                try
                {
                    Hardware.Bearings bearingsReturnable = (Hardware.Bearings)skateboard.PartInStore(store, bearings);
                    if (!bearingsReturnable.Equals(null))
                    {
                        skateboard.bearings = bearingsReturnable;
                        break;
                    }
                }
                catch (NullReferenceException)
                {
                    UI.PrintErrorMessage("There is no skateboard part with the given arguments. When typing a float number use a decimal point!");
                }
            }
            return skateboard;
        }

        /// <summary>
        /// Checks in the storage if the name given by the user has been assigned to a board before. Throws exception if it finds a matching name, returns name if it doesn't
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string CheckIfBoardNameExists(string name)
        {
            foreach (Skateboard skateboard in Skateboards)
            {
                if (skateboard.name.ToLower().Equals(name.ToLower()))
                {
                    throw new Exceptions.NameExistsException();
                }
            }
            return name;
        }

        /// <summary>
        /// Prints out the store and asks for every attribute of a deck from the user, returns a Deck with the user given attributes. 
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public Hardware.Deck DeckForAssembly(CsvReader store)
        {
            Hardware.Deck deck = new Hardware.Deck();
            while (true)
            {
                UI.PrintAllParts(store, "Decks");
                try
                {
                    deck.brand = UI.AskForHardware("brand", "deck");
                    deck.width = float.Parse(UI.AskForHardware("width", "deck"), CultureInfo.InvariantCulture.NumberFormat);
                    deck.length = float.Parse(UI.AskForHardware("Length", "deck"), CultureInfo.InvariantCulture.NumberFormat);
                    return deck;
                }
                catch (FormatException)
                {
                    UI.PrintErrorMessage("Invalid format!");
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Prints out the store and asks for every attribute of a truck from the user, returns a Truck with the user given attributes. 
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public Hardware.Truck TruckForAssembly(CsvReader store)
        {
            Hardware.Truck truck = new Hardware.Truck();
            while (true)
            {
                UI.PrintAllParts(store, "Trucks");
                try
                {
                    truck.brand = UI.AskForHardware("brand", "truck");
                    truck.size = float.Parse(UI.AskForHardware("size", "truck"), CultureInfo.InvariantCulture.NumberFormat);
                    return truck;
                }
                catch (FormatException)
                {
                    UI.PrintErrorMessage("Invalid format!");
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Prints out the store and asks for every attribute of a wheel from the user, returns a Wheel with the user given attributes. 
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public Hardware.Wheels WheelsForAssembly(CsvReader store)
        {
            Hardware.Wheels wheels = new Hardware.Wheels();
            while (true)
            {
                UI.PrintAllParts(store, "Wheels");
                try
                {
                    wheels.brand = UI.AskForHardware("brand", "wheels");
                    wheels.size = Convert.ToInt32(UI.AskForHardware("size", "wheels"));
                    wheels.hardness = UI.AskForHardware("hardness", "wheels");
                    return wheels;
                }
                catch (FormatException)
                {
                    UI.PrintErrorMessage("Invalid format!");
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Prints out the store and asks for every attribute of a griptape from the user, returns a Griptape with the user given attributes. 
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public Hardware.Griptape GriptapeForAssembly(CsvReader store)
        {
            UI.PrintAllParts(store, "Griptapes");
            Hardware.Griptape griptape = new Hardware.Griptape
            {
                brand = UI.AskForHardware("brand", "griptape")
            };
            return griptape;
        }

        /// <summary>
        /// Prints out the store and asks for every attribute of a bearing from the user, returns a Bearings with the user given attributes. 
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public Hardware.Bearings BearingsForAssembly(CsvReader store)
        {
            UI.PrintAllParts(store, "Bearings");
            Hardware.Bearings bearings = new Hardware.Bearings
            {
                brand = UI.AskForHardware("brand", "bearings"),
                type = UI.AskForHardware("type", "bearings")
            };
            return bearings;
        }

        /// <summary>
        /// Saves the current state of the storage, where the user created skateboards are stored.
        /// </summary>
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Skateboard>));
            using (Stream tw = new FileStream("inventory.xml", FileMode.Create,
                FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(tw, Skateboards);
            }
        }

        /// <summary>
        /// Loads the user created skateboards from an xml.
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadXML(string fileName = "inventory.xml")
        {
            XElement element = XElement.Load(fileName);
            var partNodes = element.Elements("Skateboard");
            foreach (var node in partNodes)
            {
                var skateboard = new Skateboard();
                var wheel = new Hardware.Wheels();
                var truck = new Hardware.Truck();
                var griptape = new Hardware.Griptape();
                var deck = new Hardware.Deck();
                var bearings = new Hardware.Bearings();
                skateboard.name = node.Element("name").Value;
                var wheelNode = new XElement(node.Element("wheel"));
                wheel.brand = wheelNode.Element("brand").Value;
                wheel.size = Convert.ToInt32(wheelNode.Element("size").Value);
                wheel.hardness = wheelNode.Element("hardness").Value;
                var truckNode = new XElement(node.Element("truck"));
                truck.brand = truckNode.Element("brand").Value;
                truck.size = float.Parse(truckNode.Element("size").Value, CultureInfo.InvariantCulture.NumberFormat);
                var griptapeNode = new XElement(node.Element("griptape"));
                griptape.brand = griptapeNode.Element("brand").Value;
                var deckNode = new XElement(node.Element("deck"));
                deck.brand = deckNode.Element("brand").Value;
                deck.width = float.Parse(deckNode.Element("width").Value, CultureInfo.InvariantCulture.NumberFormat);
                deck.length = float.Parse(deckNode.Element("length").Value, CultureInfo.InvariantCulture.NumberFormat);
                var bearingsNode = new XElement(node.Element("bearings"));
                bearings.brand = bearingsNode.Element("brand").Value;
                bearings.type = bearingsNode.Element("type").Value;
                skateboard.wheel = wheel;
                skateboard.truck = truck;
                skateboard.griptape = griptape;
                skateboard.deck = deck;
                skateboard.bearings = bearings;
                Skateboards.Add(skateboard);
            }
        }

        /// <summary>
        /// Updates part of a skateboard that the user can pick .
        /// </summary>
        public void SkateboardUpdate()
        {
            CsvReader store = new CsvReader();
            //Loads the parts from the csv file.
            store.LoadEverything();
            UI.Printer("Type in the name of the skateboard you want to update ");
            string name = UI.Scanner();
            //Check if there is a board with the name that the user gave
            GetSkateboardByName(name);
            for (int i = 0; i < Skateboards.Count; i++)
            {
                //Iterates through the storage and searches for the matching name
                if (Skateboards[i].name.ToLower().Equals(name.ToLower()))
                {
                    UI.Printer("Which hardware do you want to change?(Deck,Truck,Wheels,Griptape,Bearings)");
                    string hardwareType = UI.Scanner();
                    switch (hardwareType.ToLower())
                    {
                        case "deck":
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Hardware.Deck deck = DeckForAssembly(store);
                                    try
                                    {
                                        Hardware.Deck deckReturnable = (Hardware.Deck)Skateboards[i].PartInStore(store, deck);
                                        if (!deckReturnable.Equals(null))
                                        {
                                            Skateboards[i].deck = deckReturnable;
                                            break;
                                        }
                                    }
                                    catch (NullReferenceException)
                                    {
                                        UI.PrintErrorMessage("There is no skateboard part with the given arguments. When typing a float number use a decimal point!");
                                    }
                                }
                                break;
                            }
                        case "truck":
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Hardware.Truck truck = TruckForAssembly(store);
                                    try
                                    {
                                        Hardware.Truck truckReturnable = (Hardware.Truck)Skateboards[i].PartInStore(store, truck);
                                        if (!truckReturnable.Equals(null))
                                        {
                                            Skateboards[i].truck = truckReturnable;
                                            break;
                                        }
                                    }
                                    catch (NullReferenceException)
                                    {
                                        UI.PrintErrorMessage("There is no skateboard part with the given arguments. When typing a float number use a decimal point!");
                                    }
                                }
                                break;
                            }
                        case "wheels":
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Hardware.Wheels wheels = WheelsForAssembly(store);
                                    try
                                    {
                                        Hardware.Wheels wheelsReturnable = (Hardware.Wheels)Skateboards[i].PartInStore(store, wheels);
                                        if (!wheelsReturnable.Equals(null))
                                        {
                                            Skateboards[i].wheel = wheelsReturnable;
                                            break;
                                        }
                                    }
                                    catch (NullReferenceException)
                                    {
                                        UI.PrintErrorMessage("There is no skateboard part with the given arguments. When typing a float number use a decimal point!");
                                    }
                                }
                                break;
                            }
                        case "bearings":
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Hardware.Bearings bearings = BearingsForAssembly(store);
                                    try
                                    {
                                        Hardware.Bearings bearingsReturnable = (Hardware.Bearings)Skateboards[i].PartInStore(store, bearings);
                                        if (!bearingsReturnable.Equals(null))
                                        {
                                            Skateboards[i].bearings = bearingsReturnable;
                                            break;
                                        }
                                    }
                                    catch (NullReferenceException)
                                    {
                                        UI.PrintErrorMessage("There is no skateboard part with the given arguments. When typing a float number use a decimal point!");
                                    }
                                }
                                break;
                            }
                        case "griptape":
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Hardware.Griptape griptape = GriptapeForAssembly(store);
                                    try
                                    {
                                        Hardware.Griptape griptapeReturnable = (Hardware.Griptape)Skateboards[i].PartInStore(store, griptape);
                                        if (!griptapeReturnable.Equals(null))
                                        {
                                            Skateboards[i].griptape = griptapeReturnable;
                                            break;
                                        }
                                    }
                                    catch (NullReferenceException)
                                    {
                                        UI.PrintErrorMessage("There is no skateboard part with the given arguments. When typing a float number use a decimal point!");
                                    }
                                }
                                break;
                            }
                        default:
                            {
                                UI.PrintErrorMessage("That's not a valid hardware!");
                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Removes a skateboard from the storage.
        /// </summary>
        /// <param name="name"></param>
        public void DeleteSkateboard(string name)
        {
            Skateboards.Remove(GetSkateboardByName(name));
        }

        /// <summary>
        /// Simulates that the user's board broke in a competition .  If a board breaks it gets removed from the storage .
        /// </summary>
        /// <param name="competitor"></param>
        public void BrokeBoard(Skateboard competitor)
        {
            var rand = new Random();
            if (rand.Next(0, 5) == 0)
            {
                TimeSpan ts = new TimeSpan(0, 0, 3);
                UI.Printer($"Your board just broke into a million pieces. RIP {competitor.name}");
                Thread.Sleep(ts);
                DeleteSkateboard(competitor.name);
                Save();
            }
        }

        /// <summary>
        /// Returns a skateboard with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Skateboard GetSkateboardByName(string name)
        {
            foreach (Skateboard skateboard in Skateboards)
            {
                if (skateboard.name.ToLower().Equals(name.ToLower()))
                {
                    return skateboard;
                }
            }
            throw new Exceptions.NoMatchingBoardException();
        }
    }
}
