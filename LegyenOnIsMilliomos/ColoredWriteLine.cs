using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegyenOnIsMilliomos
{
    internal class ColoredWrite
    {

        public static void Line(string text, ConsoleColor font = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {


            Console.ForegroundColor = font;
            Console.BackgroundColor = background;
            Console.WriteLine(text);
            Console.ResetColor();


        }

        public static void Write(string text, ConsoleColor font = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {


            Console.ForegroundColor = font;
            Console.BackgroundColor = background;
            Console.Write(text);
            Console.ResetColor();


        }
    }
}