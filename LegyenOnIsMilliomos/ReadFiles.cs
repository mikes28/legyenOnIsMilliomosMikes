using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace LegyenOnIsMilliomos
{
    internal class FileAction
    {

        public async Task<List<Question>> ReadFileAsync(string fileName)
        {
            string assetsFolder = Path.Combine(AppContext.BaseDirectory, "assets");
            string filePath = Path.Combine(assetsFolder, fileName);

            if (!File.Exists(filePath))throw new FileNotFoundException($"File not found: {filePath}");


            List<Question> questions = [];


            foreach (string line in await File.ReadAllLinesAsync(filePath))
            {

                var sections = line
                .Split(';')
                .Select(s => s.Trim())
                .ToArray();

                string _question = sections[0];
                string[] _answers = [sections[1], sections[2], sections[3], sections[4]];
                char[] _correct = sections[5].ToCharArray();
                string _cat = sections[6];
                Question question = new(_question, _answers, _correct, _cat);
                questions.Add(question);
            }
            
            return questions;
        }
    }
}