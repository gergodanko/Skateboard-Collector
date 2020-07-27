using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace SkateboardCollector.api
{
    public class Skateboard //: ISerializable
    {
        public string name;
        public Hardware.Wheels wheel;
        public Hardware.Truck truck;
        public Hardware.Griptape griptape;
        public Hardware.Deck deck;
        public Hardware.Bearings bearings;

        public Skateboard(string name, Hardware.Wheels wheel, Hardware.Truck truck, Hardware.Griptape griptape, Hardware.Deck deck, Hardware.Bearings bearings)
        {
            this.name = name;
            this.wheel = wheel;
            this.truck = truck;
            this.griptape = griptape;
            this.deck = deck;
            this.bearings = bearings;

        }
        public Skateboard()
        {

        }
        
        /// <summary>
        /// Searches for a given hardware in the store. If there is a matching hardware ,returns it from the store, else it returns null.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="hardware"></param>
        /// <returns></returns>
        public Hardware.Hardware PartInStore(CsvReader store,Hardware.Hardware hardware)
        {
            if(hardware is Hardware.Deck)
            {
                Hardware.Deck deckHardware = hardware as Hardware.Deck;
                foreach(Hardware.Deck deck in store.Decks)
                {
                    if(deck.brand.ToLower().Equals(deckHardware.brand.ToLower()) && deck.length.Equals(deckHardware.length) && deck.width.Equals(deckHardware.width))
                    {
                        return deck;
                    }
                }
            }
            else if(hardware is Hardware.Truck)
            {
                Hardware.Truck truckHardware = hardware as Hardware.Truck;
                foreach(Hardware.Truck truck in store.Trucks)
                {
                    if(truck.brand.ToLower().Equals(truckHardware.brand.ToLower()) && truck.size.Equals(truckHardware.size))
                    {
                        return truck;
                    }
                }
            }
            else if(hardware is Hardware.Wheels)
            {
                Hardware.Wheels wheelsHardware = hardware as Hardware.Wheels;
                foreach(Hardware.Wheels wheels in store.Wheels)
                {
                    if(wheels.brand.ToLower().Equals(wheelsHardware.brand.ToLower()) && wheels.size.Equals(wheelsHardware.size) && wheels.hardness.ToLower().Equals(wheelsHardware.hardness.ToLower()))
                    {
                        return wheels;
                    }
                }
            }
            else if(hardware is Hardware.Griptape)
            {
                Hardware.Griptape griptapeHardware = hardware as Hardware.Griptape;
                foreach(Hardware.Griptape griptape in store.Griptapes)
                {
                    if (griptape.brand.ToLower().Equals(griptapeHardware.brand.ToLower()))
                    {
                        return griptape;
                    }
                }
            }
            else if(hardware is Hardware.Bearings)
            {
                Hardware.Bearings bearingsHardware = hardware as Hardware.Bearings;
                foreach(Hardware.Bearings bearings in store.Bearings)
                {
                    if(bearings.brand.ToLower().Equals(bearingsHardware.brand.ToLower()) && bearings.type.ToLower().Equals(bearingsHardware.type.ToLower()))
                    {
                        return bearings;
                    }
                }
            }
            return null;
        }

        //This was necessary for testing the serialization.
        /*public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            name = (string)info.GetValue("Name", typeof(string));
            wheel.brand = (string)info.GetValue("Brand", typeof(string));
            wheel.size = (int)info.GetValue("Size", typeof(int));
            wheel.hardness = (string)info.GetValue("Hardness", typeof(string));
            truck.brand = (string)info.GetValue("Brand", typeof(string));
            truck.size = (float)info.GetValue("Size", typeof(float));
            griptape.brand = (string)info.GetValue("Brand", typeof(string));
            deck.brand = (string)info.GetValue("Brand", typeof(string));
            deck.width = (float)info.GetValue("Width", typeof(float));
            deck.length = (float)info.GetValue("Length", typeof(float));
            bearings.brand = (string)info.GetValue("Brand", typeof(string));
            bearings.type = (string)info.GetValue("Type", typeof(string));


        }*/
    }
}
    

