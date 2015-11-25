using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructVectors
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 5)
                throw new Exception("Incorrect number of arguments");
            String trainFile = args[0];
            String testFile = args[1];
            if (File.Exists(trainFile))
                File.Delete(trainFile);
            if (File.Exists(testFile))
                File.Delete(testFile);
            double ratio = Convert.ToDouble(args[2]);
            string FileList = args[3];
            string DirList = args[4];
            string Line;
            int curDoc = 0;
            int testBoundary = 0;
            int totalFiles = 0;
            string label, fileName;
            Dictionary<String,Boolean> TrainDict = new Dictionary<string,bool>();
            Dictionary<String,Boolean> TestDict = new Dictionary<string,bool>();
            //read the filelist and sort them into test and training set
            using(StreamReader sr = new StreamReader(FileList))
            {
                while((Line =sr.ReadLine())!=null)
                {
                    if (String.IsNullOrWhiteSpace(Line))
                        continue;
                    if (curDoc >= totalFiles)
                    {
                        totalFiles = Convert.ToInt32(Line);
                        // this means we will have to get hte total number of files in the directory
                        testBoundary = Convert.ToInt32(Math.Round(totalFiles * ratio, 0));
                        curDoc = 0;
                        continue;
                    }
                    if(curDoc < testBoundary )
                    {
                        if (!TrainDict.ContainsKey(Line))
                            TrainDict.Add(Line, true);
                    }
                    else
                    {
                        if (!TestDict.ContainsKey(Line))
                            TestDict.Add(Line, true);
                    }
                    curDoc++;

                }
                
            }

            using (StreamReader Sd = new StreamReader(DirList))
            {
                while ((Line = Sd.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(Line))
                        continue;
                    string[] fileEntries = Directory.GetFiles(Line);
                    label = Path.GetFileName(Line);
                    foreach (var filePath in fileEntries)
                    {
                        fileName = Path.GetFileName(filePath);

                        if (TrainDict.ContainsKey(fileName))
                            LearnMallet.ProcessInputFile.ProcessInput(trainFile, filePath, label);
                        else if (TestDict.ContainsKey(fileName))
                            LearnMallet.ProcessInputFile.ProcessInput(testFile, filePath, label);
                        else
                            throw new Exception("File neither in test nor in train");
                    }
                }
            }
            
        }
    }
}
