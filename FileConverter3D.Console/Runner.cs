using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    class Runner
    {
        private IInputProcessor[] _processors;

        public Runner(params IInputProcessor[] processors) => _processors = processors;

        public async Task Run(string[] inputs)
        {
            var commands = ExtractCommands(inputs);

            if (!commands.Any())
            {
                System.Console.WriteLine("No supported operations specified.");
                return;
            }

            System.Console.WriteLine($"Operations successfully added: {commands.Count}.");

            foreach (var c in commands)
            {
                var (canExecute, failReason) = c.CanExecute();
                if (!canExecute)
                {
                    System.Console.WriteLine("Operation cannot execute under current state. Reason: " + failReason);
                    continue;
                }

                try
                {
                    await c.ExecuteAsync();
                }
                catch (AggregateException ae)
                {
                    System.Console.WriteLine("Execution failed: " + ae.InnerExceptions.First().Message);
                }
            }
        }

        private List<ICommandAsync> ExtractCommands(string[] inputs)
        {
            var commands = new List<ICommandAsync>();

            for (int i = 0, len = inputs.Length; i < len; i++)
            {
                var processor = _processors.SingleOrDefault(p => String.Equals(p.OptionName, inputs[i], StringComparison.OrdinalIgnoreCase));

                if (processor == null)
                    continue;

                if (i + processor.ArgumentCount >= len)
                    throw new ArgumentException($"Insufficient number of arguments for option '{inputs[i]}'");

                var pArgs = new string[processor.ArgumentCount];
                Array.Copy(inputs, i + 1, pArgs, 0, processor.ArgumentCount);

                commands.Add(
                    processor.Process(pArgs));
            }

            return commands;
        }
    }
}
