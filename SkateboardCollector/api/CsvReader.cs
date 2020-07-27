using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SkateboardCollector.api
{
    public class CsvReader
    {
        public Hardware.Griptape[] Griptapes { get; private set; }
        public Hardware.Bearings[] Bearings { get; private set; }
        public Hardware.Deck[] Decks { get; private set; }
        public Hardware.Truck[] Trucks { get; private set; }
        public Hardware.Wheels[] Wheels { get; private set; }

        public CsvReader()
        {
            Griptapes = new Hardware.Griptape[ArraySizer("Griptape")];
            Bearings = new Hardware.Bearings[ArraySizer("Bearings")];
            Decks = new Hardware.Deck[ArraySizer("Deck")];
            Trucks = new Hardware.Truck[ArraySizer("Truck")];
            Wheels = new Hardware.Wheels[ArraySizer("Wheels")];

        }
        
        /// <summary>
        /// Loads every type of hardware from the CSV file into the CsvReader.
        /// </summary>
        public void LoadEverything()
        {
            LoadBearings();
            LoadDecks();
            LoadGriptape();
            LoadTruck();
            LoadWheels();
        }

        /// <summary>
        /// Counts how many elements an array will have after reading data out of Csv and returns it .
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        private int ArraySizer(string searchTerm)
        {
            int size = 0;
            using (var reader = new StreamReader(@"Storage.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(",");
                    if (values[0] == searchTerm)
                    {
                        size++;
                    }
                }
            }
            return size;
        }

        /// <summary>
        /// Reads out decks out of the CSV file and store them in CsvReader Decks array.
        /// </summary>
        private void LoadDecks()
        {
            using (var reader = new StreamReader(@"Storage.csv"))
            {
                int index = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(",");
                    if (values[0] == "Deck")
                    {
                        Hardware.Deck deck = new Hardware.Deck(values[1], float.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture), float.Parse(values[3], System.Globalization.CultureInfo.InvariantCulture));
                        Decks[index] = deck;
                        index++;
                    }
                }
            }
        }

        /// <summary>
        /// Reads bearings out of the CSV file and store them in CsvReader Bearings array.
        /// </summary>
        private void LoadBearings()
        {
            using (var reader = new StreamReader(@"Storage.csv"))
            {
                int index = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(",");
                    if (values[0] == "Bearings")
                    {
                        Hardware.Bearings bearing = new Hardware.Bearings(values[1], values[2]);
                        Bearings[index] = bearing;
                        index++;
                    }
                }
            }
        }

        /// <summary>
        /// Reads griptapes out of the CSV file and store them in CsvReader Griptapes array.
        /// </summary>
        private void LoadGriptape()
        {
            using (var reader = new StreamReader(@"Storage.csv"))
            {
                int index = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(",");
                    if (values[0] == "Griptape")
                    {
                        Hardware.Griptape griptape = new Hardware.Griptape(values[1]);
                        Griptapes[index] = griptape;
                        index++;
                    }
                }
            }
        }

        /// <summary>
        /// Reads trucks out of the CSV file and store them in CsvReader Trucks array.
        /// </summary>
        private void LoadTruck()
        {
            using (var reader = new StreamReader(@"Storage.csv"))
            {
                int index = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(",");
                    if (values[0] == "Truck")
                    {
                        Hardware.Truck truck = new Hardware.Truck(values[1], float.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture));
                        Trucks[index] = truck;
                        index++;
                    }
                }
            }
        }

        /// <summary>
        /// Reads wheels out of the CSV file and store them in CsvReader Wheels array.
        /// </summary>
        private void LoadWheels()
        {
            using (var reader = new StreamReader(@"Storage.csv"))
            {
                int index = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(",");
                    if (values[0] == "Wheels")
                    {
                        Hardware.Wheels wheel = new Hardware.Wheels(values[1], Int32.Parse(values[2]), values[3]);
                        Wheels[index] = wheel;
                        index++;
                    }
                }
            }
        }
    }



}



