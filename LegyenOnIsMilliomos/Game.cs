using System;
using System.Collections.Generic;

namespace LegyenOnIsMilliomos
{
    internal class Game
    {
        internal int level = 0; // level 0 is entry question, 1 is level 1



        private HelpHandler helpHandler = new HelpHandler();

        internal int[] prizeAmounts =
        {
            0,
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
            ColoredWrite.Write(" játékban!\n\n", ConsoleColor.Black, ConsoleColor.White);

            ColoredWrite.Write("Ebben a játékban 15 egyre nehezebb kérdést kell megválaszolnod.\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("A kérdések feleletválasztósak, ", ConsoleColor.Cyan, ConsoleColor.Black);
            ColoredWrite.Write("négy lehetséges válasszal (A, B, C, D).\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("Minden helyes válasz után nő a nyereményed, ", ConsoleColor.Cyan, ConsoleColor.Black);
            ColoredWrite.Write("és az 5. és 10. kérdés után garantált összeget kapsz.\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("Ha hibázol, a garantált nyereményig visszazuhansz, így nem távozol üres kézzel.\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("Az első kérdés helyes megválaszolásával ", ConsoleColor.Green, ConsoleColor.Black);
            ColoredWrite.Write("10 000 Ft-ot nyersz.\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("Minden kérdés után a nyereményed körülbelül ", ConsoleColor.Green, ConsoleColor.Black);
            ColoredWrite.Write("megduplázódik.\n\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write("A játékot megelőzi egy gyorsasági „sorkérdés”, ahol a helyes válaszok helyes sorrendjét kell megtalálnod.\n", ConsoleColor.Magenta, ConsoleColor.Black);
            ColoredWrite.Write("A leggyorsabb helyes válaszadó kerül be a játékba.\n\n", ConsoleColor.Magenta, ConsoleColor.Black);

            ColoredWrite.Write("Készen állsz? Akkor kezdjük is el a játékot! ", ConsoleColor.Yellow, ConsoleColor.Black);
            ColoredWrite.Write("(Készen állsz? nyomj meg tetszőleges billentyűt)\n", ConsoleColor.DarkYellow, ConsoleColor.Black);

            Console.ReadKey();
            StartQues();
        }

        private void StartQues()
        {
            Console.Clear();
            ColoredWrite.Write("\nKezdésként következik egy gyorsasági feladat:\n", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write("Ez a híres-hírhedt ", ConsoleColor.Magenta, ConsoleColor.Black);
            ColoredWrite.Write("\"sorkérdés\"!\n", ConsoleColor.Yellow, ConsoleColor.Black);

            ColoredWrite.Write("A feladatod: ", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write("helyezd a megadott négy elemet a megfelelő sorrendbe.\n", ConsoleColor.Cyan, ConsoleColor.Black);

            ColoredWrite.Write("Lehet ez időrendi, nagyságrendi vagy logikai sorrend is - figyelj jól!\n", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write("Csak a leggyorsabb helyes válasz jut tovább a játékba!\n\n", ConsoleColor.Yellow, ConsoleColor.Black);

            bool isAnsCorrect;
            int tries = 0;

            do
            {
                int index = rnd.Next(sorQuestions.Count);
                Question selQues = sorQuestions[index];
                isAnsCorrect = AskQuestion.Ask(selQues, true);
                tries++;
                sorQuestions.RemoveAt(index);
            } while (!isAnsCorrect && tries < 10);

            if (isAnsCorrect)
            {
                ColoredWrite.Write("\nGratulálok, jól válaszoltál!\n", ConsoleColor.Green, ConsoleColor.Black);
                level = 1;
                NormQues();
            }
            else
            {
                ColoredWrite.Write("\nSajnálom, de a mai nap nem sikerült bekerülnöd a játékba! :(\n", ConsoleColor.Red, ConsoleColor.Black);
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
            ColoredWrite.Write($"{prizeAmounts[level]} Ft-ot ér, ", ConsoleColor.Green, ConsoleColor.Black);
            ColoredWrite.Write("és minden további válasz után nő a nyereményed!\n", ConsoleColor.Green, ConsoleColor.Black);

            ColoredWrite.Write("Az 5. és a 10. kérdés után garantált összeget kapsz - ", ConsoleColor.Yellow, ConsoleColor.Black);
            ColoredWrite.Write("ezek biztonsági szintek.\n", ConsoleColor.Yellow, ConsoleColor.Black);

            ColoredWrite.Write("\nMinden körben a kérdés után a játék megkérdezi, szeretnél-e segítséget használni.\n", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write("Ezeket a segítségeket csak egyszer tudod használni a játék során.\n", ConsoleColor.Cyan, ConsoleColor.Black);
            ColoredWrite.Write("A segítségek és a megoldás beírása között a ", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write("SZÓKÖZ ", ConsoleColor.Yellow, ConsoleColor.Black);
            ColoredWrite.Write("gombbal tudsz váltani.\n", ConsoleColor.White, ConsoleColor.Black);

            int index = 0;

            while (level <= 15 && normQuestions.Count > 0)
            {
                index = rnd.Next(normQuestions.Count);
                ColoredWrite.Write($"\nAz {level}. kérdésed {prizeAmounts[level]} Ft összegért:\n", ConsoleColor.White, ConsoleColor.Black);
                ColoredWrite.Write($"Eddigi biztos nyeremény: {prizeAmounts[Math.Max(level - 1, 0)]}\n", ConsoleColor.White, ConsoleColor.Black);

                Question selQues = normQuestions[index];
                bool correct = AskQuestion.Ask(selQues);

                if (correct)
                {
                    ColoredWrite.Write("Jól válaszoltál!\n", ConsoleColor.Green, ConsoleColor.Black);
                    level++;
                    normQuestions.RemoveAt(index);
                }
                else
                {
                    ColoredWrite.Write("Rossz válasz.\n", ConsoleColor.Red, ConsoleColor.Black);
                    int guaranteed = 0;
                    if (level >= 10) guaranteed = prizeAmounts[10];
                    else if (level >= 5) guaranteed = prizeAmounts[5];

                    Failed(level, guaranteed);
                    return;
                }
            }

            if (level > 15)
            {
                Won(prizeAmounts[15]);
            }
        }

        private void Failed(int level, int prize)
        {
            ColoredWrite.Write($"Sajnos vesztettél a(z) {level}. szinten. Nyereményed: {prize} Ft.\n", ConsoleColor.Red, ConsoleColor.Black);
        }

        private void Won(int prize)
        {
            ColoredWrite.Write($"Gratulálok, nyertél {prize} Ft-ot!\n", ConsoleColor.Green, ConsoleColor.Black);
        }
    }
}
