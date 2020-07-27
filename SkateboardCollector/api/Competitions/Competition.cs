using System;
using System.Collections.Generic;
using System.Text;

namespace SkateboardCollector.api
{

    abstract class Competition
    {
        public readonly string name;
        
        public Competition(string name)
        {
            this.name = name;
            
        }

        /// <summary>
        /// Checks if a board is eligible for a competition . Returns true if its eligible.
        /// </summary>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public abstract bool EligibleForCompetition(Skateboard competitor);

        /// <summary>
        /// Simulates a competition . Returns true if the competitor won.
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="competitor"></param>
        /// <returns></returns>
        public abstract bool Race(Storage storage,Skateboard competitor);

        /// <summary>
        /// Returns the requirements for the chosen competition.
        /// </summary>
        /// <returns></returns>
        public abstract string GetRequirements();
    }
    
   
}
