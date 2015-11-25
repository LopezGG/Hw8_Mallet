using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LearnMallet
{
    class ProcessInputFile
    {
        public static void ProcessInput(string outputFile, string inputFile, string targetLabel)
        {
            bool blank = false;
            string line;
            Regex re = new Regex("[^a-zA-Z]");
            StreamWriter Sw = new StreamWriter(outputFile, true);
            Dictionary<String, int> WordCount = new Dictionary<string, int>();
            using (StreamReader Sr = new StreamReader(inputFile))
            {
                while ((line = Sr.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(line))
                    {
                        blank = true;
                        continue;
                    }
                    if (blank)
                    {
                        
                        line = re.Replace(line, " ");
                        line = line.ToLower();
                        if (String.IsNullOrWhiteSpace(line))
                            continue;
                        string[] words = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in words)
                        {
                            if (WordCount.ContainsKey(word))
                                WordCount[word]++;
                            else
                                WordCount.Add(word, 1);
                        }

                    }
                }
            }
            Sw.Write(inputFile + " " + targetLabel + " ");
            foreach (var item in WordCount.OrderBy(i => i.Key))
            {
                Sw.Write(item.Key + " " + item.Value + " ");
            }
            Sw.WriteLine();
            Sw.Close();
        }
    }
}
