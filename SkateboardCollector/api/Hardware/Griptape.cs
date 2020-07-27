using System;
using System.Collections.Generic;
using System.Text;

namespace SkateboardCollector.api.Hardware
{
    public class Griptape : Hardware
    {
        

        public Griptape(string brand) : base(brand)
        {
            
        }
        public Griptape()
        {

        }
        public override string ToString()
        {
            return $"Brand: {this.brand}{Environment.NewLine}";
        }
    }
}
