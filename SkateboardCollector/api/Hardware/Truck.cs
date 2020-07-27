using System;
using System.Collections.Generic;
using System.Text;

namespace SkateboardCollector.api.Hardware
{
    public class Truck : Hardware
    {
        
        public float size;
        
        public Truck(string brand, float size) :base(brand)
        {
            
            this.size = size;
        }
        public Truck()
        {

        }
        public override string ToString()
        {
            return $"Brand: {this.brand}\tSize: {this.size}{Environment.NewLine}";
        }

    }
}
