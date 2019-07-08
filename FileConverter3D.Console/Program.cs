using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    class Program
    {
        private static Runner _runner;
        private const string CommandLinePrefix = "Converter> ";
        private const string ExitCommand = "exit";
        private const string HelpCommand = "help";

        static async Task Main(string[] args)
        {
            Initialize();

            if (args.Length == 0)
                await InteractiveExecutorAsync();
            else
                await SingleLineExecutorAsync(args);

            System.Console.ReadKey();
        }

        private static void Initialize()
        {
            var state = new ConverterState();
            _runner = new Runner(
                new ImportOperation(state),
                new ExportOperation(state),
                new RotateOperation(state),
                new ScaleOperation(state),
                new TranslateOperation(state),
                new OverwriteSwitch(state)
            );
        }

        static async Task InteractiveExecutorAsync()
        {
            System.Console.WriteLine("FileConverter3D is running in interactive mode. Please type in operations, then press enter.");
            System.Console.WriteLine($"To see the list of supported operations, use '{HelpCommand}'. To exit, use '{ExitCommand}'.");

            while (true)
            {
                System.Console.WriteLine();
                System.Console.Write(CommandLinePrefix);
                var cmdLine = System.Console.ReadLine();

                if (String.Equals(cmdLine, ExitCommand, StringComparison.OrdinalIgnoreCase))
                    break;

                if (string.IsNullOrWhiteSpace(cmdLine))
                    continue;

                System.Console.WriteLine();

                await SingleLineExecutorAsync(SplitInput(cmdLine));
            }
        }

        static async Task SingleLineExecutorAsync(string[] args)
        {
            try
            {
                await _runner.Run(args);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("At least one operation faulted: " + e.Message);
            }
        }

        private static string[] SplitInput(string input)
        {
            return RemoveNewLines(input).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string RemoveNewLines(string s)
        {
            s = s.Replace("\n", String.Empty);
            s = s.Replace("\r", String.Empty);
            s = s.Replace("\t", String.Empty);
            return s;
        }
    }
}
