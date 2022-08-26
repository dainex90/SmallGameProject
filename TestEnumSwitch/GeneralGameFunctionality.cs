using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    internal class GeneralGameFunctionality
    {
        public GeneralGameFunctionality()
        {
            // Constructor
        }

        public static void DelayTextOutput(string text)
        {
            foreach (var character in text)
            {
                Thread.Sleep(50);
                Console.Write(character);
            }
        }

        public static void ChangeForegroundFontColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}
