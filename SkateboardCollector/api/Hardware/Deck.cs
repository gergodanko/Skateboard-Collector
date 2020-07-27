using System;
using System.Collections.Generic;
using System.Text;

namespace SkateboardCollector.api.Hardware
{
    public class Deck : Hardware
    {
        //private string brand;
        

        public float width;
        public float length;
        public Deck(string brand, float width, float length) :base(brand)
        {
            
            this.width = width;
            this.length = length;
        }
        public Deck()
        {

        }
        public override string ToString()
        {
            return $"Brand: {this.brand}\tWidth: {this.width}\tLength: {this.length}{Environment.NewLine}";
        }

    }
}
