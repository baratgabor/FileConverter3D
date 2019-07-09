using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    class Program
    {
        private static Runner _runner;
        private static ConverterState _state;
        private static bool _interactiveMode;
        private const string CommandLinePrefix = "Converter> ";
        private const string ExitCommand = "exit";
        private const string HelpCommand = "help";
        private const string ClearCommand = "clear";

        static async Task Main(string[] args)
        {
            Initialize();
            _interactiveMode = args.Length == 0;

            if (_interactiveMode)
                await InteractiveExecutorAsync();
            else
                await SingleLineExecutorAsync(args);
        }

        private static void Initialize()
        {
            _state = new ConverterState();
            _state.ModelChanged += PrintModelChange;
            _runner = new Runner(
                new ImportOperation(_state),
                new ExportOperation(_state),
                new RotateOperation(_state),
                new ScaleOperation(_state),
                new TranslateOperation(_state),
                new OverwriteSwitch(_state)
            );
        }

        static async Task InteractiveExecutorAsync()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("FileConverter3D is running in interactive mode. Please type in operations, then press enter.");
            System.Console.WriteLine($"To see the list of supported operations, use '{HelpCommand}'. To exit, use '{ExitCommand}'.");

            while (true)
            {
                System.Console.WriteLine();
                System.Console.Write(CommandLinePrefix);
                var cmdLine = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(cmdLine))
                    continue;

                if (String.Equals(cmdLine.Trim(), ExitCommand, StringComparison.OrdinalIgnoreCase))
                    break;

                if (String.Equals(cmdLine.Trim(), ClearCommand, StringComparison.OrdinalIgnoreCase))
                {
                    System.Console.Clear();
                    continue;
                }

                if (String.Equals(cmdLine.Trim(), HelpCommand, StringComparison.OrdinalIgnoreCase))
                {
                    PrintHelp();
                    continue;
                }

                System.Console.WriteLine();

                await SingleLineExecutorAsync(SplitInput(cmdLine).ToArray());
            }
        }

        static async Task SingleLineExecutorAsync(string[] args)
        {
            if (args.Length == 1 && String.Equals(args[0].Trim(), HelpCommand, StringComparison.OrdinalIgnoreCase))
            {
                PrintHelp();
                return;
            }

            try
            {
                await _runner.Run(args);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("At least one operation faulted: " + e.Message);
            }
        }

        private static IEnumerable<string> SplitInput(string input)
        {
            input = RemoveNewLines(input);
            bool inQuote = false;
            bool inSpace = false;
            int segmentStart = 0;

            for (int i = 0, len = input.Length; i < len; i++)
            {
                char c = input[i];

                if (c == '"') inQuote = !inQuote;

                if (!inQuote && c == ' ')
                {
                    if (!inSpace)
                        yield return CleanSubstr(input, segmentStart, i);
                    segmentStart = i + 1;
                    inSpace = true;
                }
                else
                    inSpace = false;
            }

            if(segmentStart < input.Length)
                yield return CleanSubstr(input, segmentStart, input.Length);

            string CleanSubstr(string value, int startPos, int endPos)
            {
                while (value[endPos - 1] == '"' && endPos > startPos) { endPos--; }
                while (value[startPos] == '"' && startPos < endPos) { startPos++; }

                return value.Substring(startPos, endPos - startPos);
            }

            string RemoveNewLines(string s)
            {
                s = s.Replace("\n", String.Empty);
                s = s.Replace("\r", String.Empty);
                s = s.Replace("\t", String.Empty);
                return s;
            }
        }

        private static void PrintModelChange(IModel newModel)
        {
            System.Console.WriteLine();

            if (newModel == null)
                System.Console.WriteLine($"# Model unloaded.");
            else
                System.Console.WriteLine($"# Model loaded: {newModel.Vertices.Count} vertices, {newModel.Faces.Count} faces.");

            System.Console.WriteLine($"# Total memory in use: {GC.GetTotalMemory(true)/1024} KB");
        }

        private static void PrintHelp()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Available converter operations:");
            System.Console.WriteLine();

            foreach (var p in _runner.Processors)
                System.Console.WriteLine(" " + p.HelpText);

            if (!_interactiveMode)
                System.Console.WriteLine(Environment.NewLine + "Additionally, if you execute the program without arguments, it starts in 'interactive' prompt where you can type in commands in seqeuence.");
        }
    }
}
