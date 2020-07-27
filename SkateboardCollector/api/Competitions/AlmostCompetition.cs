using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SkateboardCollector.cmd;

namespace SkateboardCollector.api.Competitions
{
    class AlmostCompetition : Competition
    {
        
        
        public AlmostCompetition( string name = "Almost's Street SKATE Competition") : base(name)
        {
            
        }
        /// <summary>
        /// Checks if a board is eligible for a competition .
        /// Returns true if the competitor skateboard has an "Almost" branded deck and a wheel with a "hard" hardness.
        /// </summary>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public override bool EligibleForCompetition(Skateboard competitor)
        {
            if(competitor.deck.brand.Equals("Almost") && competitor.wheel.hardness.Equals("hard"))
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
            return "You must have an Almost branded deck and a hard wheel to enter this race!";
        }
        /// <summary>
        /// Simulates a 3 round competition where every round the competitor is against 1 other competitor.
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public override bool Race(Storage storage,Skateboard competitor)
        {
            TimeSpan ts = new TimeSpan(0, 0, 3);
            var rand = new Random();
            int wonRounds = 0;
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                UI.Printer("Competing...");
                Thread.Sleep(ts);
                Console.Clear();
                if (rand.Next(0,2) == 0)
                {
                    UI.Printer($"You won the {i + 1}. round! ");
                    wonRounds++;
                    Thread.Sleep(ts);
                    Console.Clear();
                }
                else
                {
                    UI.Printer("You lost this round, and you're out of the competition.");
                    Thread.Sleep(ts);
                    Console.Clear();
                    break;
                }

            }
            if(wonRounds == 3)
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
