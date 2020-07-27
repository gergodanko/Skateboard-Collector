using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SkateboardCollector.cmd;
namespace SkateboardCollector.api.Competitions
{
    class BakerCompetition : Competition
    {
        public BakerCompetition(string name = "Baker's Whacky Hillbomb Race") : base(name)
        {
            
        }
        /// <summary>
        /// Checks if a board is eligible for a competition .
        /// Returns true if the competitor skateboard's bearing type is one of these: "reds", "super reds", "g2", "big balls reds" 
        /// and the wheel's size must be atleast 56.
        /// </summary>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public override bool EligibleForCompetition(Skateboard competitor)
        {
            List<string> eligibleBearings = new List<string>() { "reds", "super reds", "g2", "big balls reds" };
            if (eligibleBearings.Contains(competitor.bearings.type.ToLower()) && competitor.wheel.size>=56)
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
            return "You must have Bones or Bronson Speed Co. bearings and atleast size 56 wheels to enter this race ! ";
        }
        /// <summary>
        /// Simulates a competition where the competitor is against 14 other skaters. 
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public override bool Race(Storage storage,Skateboard competitor)
        {
            var rand = new Random();
            TimeSpan ts = new TimeSpan(0, 0, 3);
            Console.Clear();
            UI.Printer("Competing against 14 other skaters...");
            Thread.Sleep(ts);
            Console.Clear();
            if (rand.Next(0,16) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
