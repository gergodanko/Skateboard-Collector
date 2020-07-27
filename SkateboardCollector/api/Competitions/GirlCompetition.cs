using System;
using System.Collections.Generic;
using System.Text;
using SkateboardCollector.cmd;
using System.Threading;


namespace SkateboardCollector.api.Competitions
{
    class GirlCompetition : Competition
    {
        public GirlCompetition(string name = "Women's Halfpipe Competition, Powered By Girl") :base(name)
        {
            
        }
        /// <summary>
        /// Checks if a board is eligible for a competition .
        /// Asks the user their gender, if they answer female it returns true.
        /// </summary>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public override bool EligibleForCompetition(Skateboard competitor)
        {
            UI.Printer("Are you a male or a female? (m/f)");
            string gender = UI.Scanner();
            if (gender.ToLower().Equals("f"))
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
            return "Only women can enter this competition !\n Beware! We take no responsibility for the damage you or your skateboard might take! ";
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
            UI.Printer("Competing against 3 other skaters on the halfpipe...");
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
