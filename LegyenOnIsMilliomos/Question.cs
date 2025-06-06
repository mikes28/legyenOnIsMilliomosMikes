using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LegyenOnIsMilliomos
{
    internal class Question
    {
        private readonly string ques;
        private readonly string[] ans = new string[4];
        private readonly char[] corrAns = new char[4];
        private readonly string category;

        public Question(string ques, string[] ans, char[] corrAns, string category)
        {
            this.ques = ques;
            this.ans = ans;
            this.corrAns = corrAns;
            this.category = category;

        }
        public string Ques { get => ques; }
        public string[] Ans { get => ans; }
        public string Category { get => category; }

        internal char[] CorrAns {get => corrAns;}

        public bool IsItCorrect(char[] givenAns)
        {
            if (givenAns == null || givenAns.Length != corrAns.Length)
                return false;

            for (int i = 0; i < corrAns.Length; i++)
            {
                if (char.ToUpperInvariant(givenAns[i]) != char.ToUpperInvariant(corrAns[i]))
                    return false;
            }
            return true;
        }


        public override string ToString()
        {
            return $"A '{this.ques}' kerdesre a lehetseges valaszok: {string.Join(", ", this.ans)}, " +
                   $"a helyes megfejtes: {string.Join(", ", this.corrAns)}, " +
                   $"a kerdes kategoriaja: {this.category}";
        }

    }
}