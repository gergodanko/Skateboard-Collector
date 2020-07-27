using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SkateboardCollector.cmd;

namespace SkateboardCollector.api.Competitions
{
    class EnjoiCompetition : Competition
    {
        public EnjoiCompetition(string name = "Enjoi's Megaramp Competition") :base(name)
        {
            
        }

        /// <summary>
        /// Checks if a board is eligible for a competition .
        /// Returns true if the competitor skateboard deck's width is atleast 8 and the wheel's hardness is either medium or soft.
        /// </summary>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public override bool EligibleForCompetition(Skateboard competitor)
        {
            if(competitor.deck.width>=8 && competitor.wheel.hardness.Equals("soft") || competitor.wheel.hardness.Equals("medium"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string GetRequirements()
        {
            return "You must have a deck with atleast 8 width and a wheel that is either medium or soft to enter this competition!\n Beware! We take no responsibility for the damage you or your skateboard might take! ";
        }

        /// <summary>
        /// Simulates a competition where the competitor is against 3 other skaters. There is a 25% chance of their board breaking at the end.
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public override bool Race(Storage storage,Skateboard competitor)
        {
            var rand = new Random();
            TimeSpan ts = new TimeSpan(0, 0, 3);
            Console.Clear();
            UI.Printer("Competing against 3 other skaters on the megaramp...");
            Thread.Sleep(ts);
            Console.Clear();
            if (rand.Next(0, 4) == 0)
            {
                storage.BrokeBoard(competitor);
                return true;
            }
            else
            {
                storage.BrokeBoard(competitor);
                return false;
            }
            
        }
    }
}
