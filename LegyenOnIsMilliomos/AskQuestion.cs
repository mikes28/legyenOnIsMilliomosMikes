using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegyenOnIsMilliomos
{
    internal class AskQuestion
    {
        internal static bool Ask(Question question, bool _isItOrder = false)
        {

            System.Console.WriteLine("szia a kerdesed:");

            System.Console.WriteLine($"Kategoriaja {question.Category}");
            System.Console.WriteLine(question.Ques);
            System.Console.WriteLine($"A: {question.Ans[0]}");
            System.Console.WriteLine($"B: {question.Ans[1]}");
            System.Console.WriteLine($"C: {question.Ans[2]}");
            System.Console.WriteLine($"D: {question.Ans[3]}");
            if (_isItOrder)
            {
                System.Console.WriteLine("kerlek gepeld be a helyes sorrendet majd nyomj entert. A betuket szokozok nelkul ird egymas utan.");
                if (question.IsItCorrect(Console.ReadLine().ToString().ToCharArray()))
                {
                    System.Console.WriteLine("jo valasz");
                    return true;

                }
                System.Console.WriteLine("sajnos rossz valasz");
                return false;
            }
            else
            {
                System.Console.WriteLine("Kerlek nyomd le a megoldas betujet, vagy a szokozt!");
                if (question.IsItCorrect([Console.ReadKey().KeyChar]))
                {
                    System.Console.WriteLine("jo valasz");
                    return true;

                }
                System.Console.WriteLine("sajnos rossz valasz");
                return false;
            }

        }
        //todo ask question, decide it the ans is good. return bool. should make a func for colored print, and try to amke a good looking font based gui
    }
}