using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LearnMallet
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
                throw new Exception("Incorrect number of arguments");
            string inputFile = args[0];
            string targetLabel = args[1];
            string outputFile = args[2];
            if (File.Exists(outputFile))
                File.Delete(outputFile);

            ProcessInputFile.ProcessInput(outputFile, inputFile, targetLabel);
            Console.ReadLine();
        }
        
    }
}
