using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace SkateboardCollector.api.Hardware
{
    public class Wheels : Hardware
    {
        
        public int size;
        public string hardness;

        public Wheels(string brand, int size,string hardness) : base(brand)
        {
            
            this.size = size;
            this.hardness = hardness;
        }
        public Wheels()
        {

        }

        public override string ToString()
        {
            return $"Brand: {this.brand}\tSize: {this.size}\tHardness: {this.hardness}{Environment.NewLine}";
        }
    }
}
