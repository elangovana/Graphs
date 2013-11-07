using System;
using System.IO;

namespace AE.Graphs.Application
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Incorrect Usage. \nUsage AE.Graphs.Application.exe <TestFileContainingGraph>");
                return;
            }
            string file = args[0];
            if (!File.Exists(file))
            {
                Console.WriteLine("The File {0} does not exist. Please povide a valid text file", file);
                return;
            }
            Process(File.ReadAllText(file));
        }


        private static void Process(string inputGraph)
        {
            try
            {
                var result = new Calculate(inputGraph).BuiltInRun();

                int i = 0;
                Console.WriteLine("GRAPH: {0}", inputGraph);
                foreach (var item in result)
                {
                    i++;
                    Console.WriteLine("Output #{0}: {1}", i, item);
                    ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Has Occurred : {0}", ex.Message);
            }
        }
    }
}