using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var state = new ConverterState();
            var r = new Runner(
                new IInputProcessor[] {
                    new ImportOperation(state),
                    new ExportOperation(state),
                    new RotateOperation(state),
                    new ScaleOperation(state),
                    new TranslateOperation(state)
                });

            System.Console.WriteLine("FileConverter3D started in interactive mode. Please enter operations.");
            System.Console.WriteLine();

            string line;
            while (!string.IsNullOrEmpty(line = System.Console.ReadLine()))
            {
                var inputs = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    await r.Run(inputs);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("At least one operation faulted: " + e.Message);
                }
            }
        }
    }
}
