using System;

namespace LegyenOnIsMilliomos
{
    public class HelpHandler
    {
        private bool used5050 = false;
        private bool usedPhone = false;
        private bool usedAudience = false;
        private Random rnd = new();

        internal void PrintHelpMenu()
        {
            ColoredWrite.Write("\nSegítség lehetőségek:\n", ConsoleColor.Cyan, ConsoleColor.Black);
            ColoredWrite.Write($"1: 50:50 {(used5050 ? "(Már használt)" : "")}\n", ConsoleColor.Cyan, ConsoleColor.Black);
            ColoredWrite.Write($"2: Telefonos segítség {(usedPhone ? "(Már használt)" : "")}\n", ConsoleColor.Cyan, ConsoleColor.Black);
            ColoredWrite.Write($"3: Közönségi szavazás {(usedAudience ? "(Már használt)" : "")}\n", ConsoleColor.Cyan, ConsoleColor.Black);
        }

        internal bool UseHelp(int helpNumber, Question question)
        {
            switch (helpNumber)
            {
                case 1:
                    if (used5050)
                    {
                        ColoredWrite.Write("A 50:50 segítséget már használtad.\n", ConsoleColor.Yellow, ConsoleColor.Black);
                        return false;
                    }
                    used5050 = true;
                    Activate5050(question);
                    return true;

                case 2:
                    if (usedPhone)
                    {
                        ColoredWrite.Write("A telefonos segítséget már használtad.\n", ConsoleColor.Yellow, ConsoleColor.Black);
                        return false;
                    }
                    usedPhone = true;
                    ActivatePhoneHelp(question);
                    return true;

                case 3:
                    if (usedAudience)
                    {
                        ColoredWrite.Write("A közönségi szavazást már használtad.\n", ConsoleColor.Yellow, ConsoleColor.Black);
                        return false;
                    }
                    usedAudience = true;
                    ActivateAudienceHelp(question);
                    return true;

                default:
                    ColoredWrite.Write("Érvénytelen segítség választás.\n", ConsoleColor.Yellow, ConsoleColor.Black);
                    return false;
            }
        }

        private void Activate5050(Question question)
        {
            // Correct answer letter is the first char in question.corrAns
            char correctLetter = char.ToUpperInvariant(question.CorrAns[0]);

            // Get all possible answer letters: A,B,C,D
            char[] allAnswers = { 'A', 'B', 'C', 'D' };

            // Remove correct letter from list to pick random wrong answer
            var incorrectAnswers = Array.FindAll(allAnswers, a => a != correctLetter);

            // Pick random incorrect answer to show with correct one
            char randomIncorrect = incorrectAnswers[rnd.Next(incorrectAnswers.Length)];

            int correctIndex = correctLetter - 'A';
            int incorrectIndex = randomIncorrect - 'A';

            ColoredWrite.Write("50:50 segítség:\n", ConsoleColor.Magenta, ConsoleColor.Black);
            ColoredWrite.Write($"{correctLetter}: {question.Ans[correctIndex]}\n", ConsoleColor.Magenta, ConsoleColor.Black);
            ColoredWrite.Write($"{randomIncorrect}: {question.Ans[incorrectIndex]}\n", ConsoleColor.Magenta, ConsoleColor.Black);
        }

        private void ActivatePhoneHelp(Question question)
        {
            char correctLetter = char.ToUpperInvariant(question.CorrAns[0]);
            char[] allAnswers = { 'A', 'B', 'C', 'D' };

            // Probability weights:
            // 10% doesn't know
            // 20% guesses wrong (but not sure)
            // 70% guesses right (but not 100% sure)

            int chance = rnd.Next(100);

            string hint;

            if (chance < 10)
            {
                // Doesn't know
                hint = "Őszintén szólva, nem vagyok biztos benne, sajnálom, nem tudok segíteni.";
            }
            else if (chance < 30)
            {
                // Wrong guess but unsure
                var wrongAnswers = allAnswers.Where(c => c != correctLetter).ToArray();
                char guess = wrongAnswers[rnd.Next(wrongAnswers.Length)];
                hint = $"Nem vagyok benne teljesen biztos, de talán az '{guess}' lehet a helyes válasz.";
            }
            else
            {
                // Right guess but unsure
                string[] confidentHints = new[]
                {
            $"Úgy gondolom, hogy a helyes válasz az '{correctLetter}'.",
            $"Szerintem az '{correctLetter}' a jó választás, de nem vagyok teljesen biztos.",
            $"Ha engem kérdezel, akkor az '{correctLetter}' tűnik a legvalószínűbbnek.",
            $"Nekem az a tippem, hogy az '{correctLetter}' lehet a megoldás."
        };
                hint = confidentHints[rnd.Next(confidentHints.Length)];
            }

            ColoredWrite.Write("Telefonos segítség:\n", ConsoleColor.Magenta, ConsoleColor.Black);
            ColoredWrite.Write(hint + "\n", ConsoleColor.Magenta, ConsoleColor.Black);
        }

        private void ActivateAudienceHelp(Question question)
        {
            char correctLetter = char.ToUpperInvariant(question.CorrAns[0]);
            char[] allAnswers = { 'A', 'B', 'C', 'D' };

            int correctIndex = correctLetter - 'A';

            int correctPercent = rnd.Next(50, 81);
            int remainingPercent = 100 - correctPercent;

            int[] percents = new int[4];

            for (int i = 0; i < 4; i++)
            {
                if (i == correctIndex) continue;
                percents[i] = rnd.Next(0, remainingPercent + 1);
            }

            int sumOthers = 0;
            for (int i = 0; i < 4; i++)
                if (i != correctIndex)
                    sumOthers += percents[i];

            for (int i = 0; i < 4; i++)
            {
                if (i != correctIndex)
                    percents[i] = (int)((double)percents[i] / sumOthers * remainingPercent);
            }

            int fix = remainingPercent - percents.Where((p, i) => i != correctIndex).Sum();
            percents[correctIndex] = correctPercent + fix;

            ColoredWrite.Write("Közönségi szavazás eredménye:\n", ConsoleColor.Magenta, ConsoleColor.Black);

            for (int i = 0; i < 4; i++)
            {
                char ansChar = (char)('A' + i);
                ConsoleColor color = i == correctIndex ? ConsoleColor.Green : ConsoleColor.Gray;
                ColoredWrite.Write($"{ansChar}: {percents[i]}%\n", color, ConsoleColor.Black);
            }
        }
    }
}
