using System;

namespace LegyenOnIsMilliomos
{
    internal class AskQuestion
    {
        private static HelpHandler helpHandler = new HelpHandler();

        internal static bool Ask(Question question, bool _isItOrder = false)
        {
            Console.Clear();
            ColoredWrite.Write("Kérdésed:\n", ConsoleColor.Yellow, ConsoleColor.Black);

            ColoredWrite.Write($"Kategória: {question.Category}\n", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write($"{question.Ques}\n", ConsoleColor.White, ConsoleColor.Black);

            ColoredWrite.Write($"A: {question.Ans[0]}\n", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write($"B: {question.Ans[1]}\n", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write($"C: {question.Ans[2]}\n", ConsoleColor.White, ConsoleColor.Black);
            ColoredWrite.Write($"D: {question.Ans[3]}\n", ConsoleColor.White, ConsoleColor.Black);

            if (_isItOrder)
            {
                ColoredWrite.Write("\nÍrd be a helyes sorrendet, szóköz nélkül (pl. ABCD), majd ENTER:\n", ConsoleColor.Cyan, ConsoleColor.Black);
                var input = Console.ReadLine().ToUpper().ToCharArray();
                if (question.IsItCorrect(input))
                {
                    ColoredWrite.Write("Helyes sorrend!\n", ConsoleColor.Green, ConsoleColor.Black);
                    return true;
                }
                else
                {
                    ColoredWrite.Write("Hibás sorrend.\n", ConsoleColor.Red, ConsoleColor.Black);
                    return false;
                }
            }
            else
            {
                while (true)
                {
                    ColoredWrite.Write("\nSegítség lehetőségek:\n", ConsoleColor.Cyan, ConsoleColor.Black);
                    ColoredWrite.Write("1: 50:50\n", ConsoleColor.Cyan, ConsoleColor.Black);
                    ColoredWrite.Write("2: Telefonos segítség\n", ConsoleColor.Cyan, ConsoleColor.Black);
                    ColoredWrite.Write("3: Közönségi szavazás\n", ConsoleColor.Cyan, ConsoleColor.Black);
                    ColoredWrite.Write("Írd be a válasz betűjét (A-D) vagy a segítség számát (1-3):\n", ConsoleColor.White, ConsoleColor.Black);

                    var key = Console.ReadKey(true).KeyChar;

                    if (char.IsDigit(key) && (key == '1' || key == '2' || key == '3'))
                    {
                        bool used = helpHandler.UseHelp(key - '0', question);
                        if (!used)
                        {
                            ColoredWrite.Write("Ez a segítség már használva vagy nem elérhető.\n", ConsoleColor.Yellow, ConsoleColor.Black);
                        }
                        continue;
                    }

                    char answerKey = char.ToUpper(key);
                    if (answerKey >= 'A' && answerKey <= 'D')
                    {
                        if (question.IsItCorrect(new char[] { answerKey }))
                        {
                            ColoredWrite.Write("Helyes válasz!\n", ConsoleColor.Green, ConsoleColor.Black);
                            return true;
                        }
                        else
                        {
                            ColoredWrite.Write("Helytelen válasz.\n", ConsoleColor.Red, ConsoleColor.Black);
                            return false;
                        }
                    }

                    ColoredWrite.Write("Érvénytelen bemenet, próbáld újra!\n", ConsoleColor.Yellow, ConsoleColor.Black);
                }
            }
        }
    }
}
