using System;
using System.Collections.Generic;
using System.Text;

namespace SkateboardCollector.cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();
            ui.StartScreen();
            while (true)
            {
                ui.HandleMenu();
                try
                {
                    ui.Choose();
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("");
                }
            }
        }
    }
}
