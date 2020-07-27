using System;
using System.Collections.Generic;
using System.Text;

namespace SkateboardCollector.api.Hardware
{
    public class Bearings : Hardware
    {
        
        public string type;

        public Bearings(string brand ,string type) :base(brand)
        {
            
            this.type = type;
        }
        public Bearings()
        {

        }
        public override string ToString()
        {
            return $"Brand: {this.brand}\tType: {this.type}{Environment.NewLine}";
        }
        
    }
}
