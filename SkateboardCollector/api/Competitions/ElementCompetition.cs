using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SkateboardCollector.cmd;

namespace SkateboardCollector.api.Competitions
{
    class ElementCompetition : Competition
    {
        public ElementCompetition( string name = "Race against the Elements, Halfpipe Competition") : base(name)
        {
            
        }

        /// <summary>
        /// Checks if a board is eligible for a competition .
        /// Returns true if the competitor skateboard's length is more than 32 and the wheel's hardness is either medium or soft.
        /// </summary>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public override bool EligibleForCompetition(Skateboard competitor)
        {
            if (competitor.deck.length < 32 && competitor.wheel.hardness.Equals("medium") || competitor.wheel.hardness.Equals("soft"))
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
            return "You must have a deck with a length less than 32 and a wheel that is either medium or soft to enter this competition! \n Beware! We take no responsibility for your skateboard! ";
        }

        /// <summary>
        /// Simulates a competition where the competitor is against 3 other skaters. There is a 25% chance of their board breaking at the end.
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public override bool Race(Storage storage , Skateboard competitor)
        {
            var rand = new Random();
            TimeSpan ts = new TimeSpan(0, 0, 3);
            Console.Clear();
            UI.Printer("Competing against 3 other skaters...");
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
