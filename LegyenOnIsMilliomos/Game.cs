using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Quic;
using System.Threading.Tasks;

namespace LegyenOnIsMilliomos
{

    internal class Game
    {
        internal int level = 0;//level 0 is the entry question, 1 is the level 1


        internal int[] prizeAmounts = new int[]
        {
            10000,
            20000,
            50000,
            100000,
            250000,
            500000,
            750000,
            1000000,
            1500000,
            2000000,
            5000000,
            10000000,
            15000000,
            25000000,
            50000000
        };


        private static Random rnd = new();

        private List<Question> normQuestions;
        private List<Question> sorQuestions;
        internal string CompletedPhase => level switch
        {
            0 => "startup",
            15 => "completed",
            > 15 => "undefined",
            >= 10 => "second",
            >= 5 => "first",
            _ => "not achieved"
        };

        public Game(List<Question> sorQuestions, List<Question> normQuestions)
        {
            this.sorQuestions = sorQuestions;
            this.normQuestions = normQuestions;
        }
        public void StartGame()
        {
            Console.Clear();

            ColoredWrite.Write("Üdvözöllek a ", ConsoleColor.Black, ConsoleColor.White);
            ColoredWrite.Write("Legyen Ön is Milliomos", ConsoleColor.Yellow, ConsoleColor.DarkBlue);
            ColoredWrite.Write(" játékban!\n", ConsoleColor.Black, ConsoleColor.White);

            ColoredWrite.Write("Ebben a játékban 15 egyre nehezebb kérdést kell megválaszolnod.\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("A kérdések feleletválasztósak, ", ConsoleColor.Cyan, ConsoleColor.Black);
            ColoredWrite.Write("négy lehetséges válasszal (A, B, C, D).\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("Minden helyes válasz után nő a nyereményed, ", ConsoleColor.Cyan, ConsoleColor.Black);
            ColoredWrite.Write("és az 5. és 10. kérdés után garantált összeget kapsz.\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("Ha hibázol, a garantált nyereményig visszazuhansz, így nem távozol üres kézzel.\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("Az első kérdés helyes megválaszolásával ", ConsoleColor.Green, ConsoleColor.Black);
            ColoredWrite.Write("10 000 Ft-ot nyersz.", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("Minden kérdés után a nyereményed körülbelül ", ConsoleColor.Green, ConsoleColor.Black);
            ColoredWrite.Write("megduplázódik.\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("A játékot megelőzi egy gyorsasági „sorkérdés”, ahol a helyes válaszok helyes sorrendjét kell megtalálnod.", ConsoleColor.Magenta, ConsoleColor.Black);
            ColoredWrite.Write("A leggyorsabb helyes válaszadó kerül be a játékba.\n", ConsoleColor.Magenta, ConsoleColor.Black);

            ColoredWrite.Write("Készen állsz? Akkor kezdjük is el a játékot! ", ConsoleColor.Yellow, ConsoleColor.Black);
            ColoredWrite.Write("(Készen állsz? nyomj meg tetszoleges billentyut)\n", ConsoleColor.DarkYellow, ConsoleColor.Black);

            Console.ReadKey();
            StartQues();

        }

        private void StartQues()
        {
            Question selQues = sorQuestions[rnd.Next(sorQuestions.Count)];
            System.Console.Write("\n");
            System.Console.Write("\n");
            ColoredWrite.Write("\nKezdésként következik egy gyorsasági feladat:", ConsoleColor.Magenta, ConsoleColor.Black);
            ColoredWrite.Write("Ez a híres-hírhedt ", ConsoleColor.Magenta, ConsoleColor.Black);
            ColoredWrite.Write("\"sorkérdés\"!\n", ConsoleColor.Yellow, ConsoleColor.Black);

            ColoredWrite.Write("A feladatod: ", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write("helyezd a megadott négy elemet a megfelelő sorrendbe.", ConsoleColor.Cyan, ConsoleColor.Black);

            ColoredWrite.Write("Lehet ez időrendi, nagyságrendi vagy logikai sorrend is - figyelj jól!", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write("Csak a leggyorsabb helyes válasz jut tovább a játékba!\n", ConsoleColor.Yellow, ConsoleColor.Black);


            bool is_AnsCorrect;
            int i = 0;
            do
            {
                is_AnsCorrect = AskQuestion.Ask(selQues, true);
                System.Console.WriteLine("kerdes?");
                i += 1;
            } while (!is_AnsCorrect && i < 10);

            if (is_AnsCorrect)
            {
                System.Console.WriteLine("Gratulalok, jol valaszoltal!");
                level = 1;
                NormQues();
            }
            else
            {
                System.Console.WriteLine("Sajnalom, de a mai nap nem sikerult bekerulnod a jatekba! :c");
            }

        }


        private void NormQues()
        {
            ColoredWrite.Write("\nMost kezdődik a fő játék!\n", ConsoleColor.Green, ConsoleColor.Black);
            ColoredWrite.Write("15 kérdés vár rád, egyre nehezedő sorrendben.\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("Minden kérdéshez ", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write("négy válaszlehetőséget ", ConsoleColor.Cyan, ConsoleColor.Black);
            ColoredWrite.Write("kapsz, melyek közül csak egy helyes.\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("Az első kérdés ", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write($"{prizeAmounts[level - 1]} Ft-ot ér, ", ConsoleColor.Green, ConsoleColor.Black);
            ColoredWrite.Write("és minden további válasz után nő a nyereményed!\n", ConsoleColor.Green, ConsoleColor.Black);

            ColoredWrite.Write("Az 5. és a 10. kérdés után garantált összeget kapsz - ", ConsoleColor.Yellow, ConsoleColor.Black);
            ColoredWrite.Write("ezek biztonsági szintek.\n", ConsoleColor.Yellow, ConsoleColor.Black);

            ColoredWrite.Write("\nMinden körben a kérdés után a játék megkérdezi, szeretnél-e segítséget használni.\n", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write("Ezeket a segítségeket csak egyszer tudod használni a játék során.\n", ConsoleColor.Cyan, ConsoleColor.Black);
            ColoredWrite.Write("A segítségek és a megoldás beírása között a ", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write("SZÓKÖZ ", ConsoleColor.Yellow, ConsoleColor.Black);
            ColoredWrite.Write("gombbal tudsz váltani.\n", ConsoleColor.White, ConsoleColor.Black);
            Question selQues = normQuestions[rnd.Next(normQuestions.Count)];
            Console.WriteLine(AskQuestion.Ask(selQues) ? "Gratulálok!" : "Ez most nem sikerült.");


        }

    }
    //todo ask first question using Ask Question from the sorQuestions list, make it ask a type for the question
    //for 10 time
    //get random number from tha ques list, and run ask question, put the num on a list of used before. It should return true or false based on the correction of answer. 
    //every time check wether the ques was asked before and if so, generate a new.
}
