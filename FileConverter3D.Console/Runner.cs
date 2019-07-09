using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    class Runner
    {
        public IEnumerable<IInputProcessor> Processors => _processors;

        private IInputProcessor[] _processors;

        public Runner(params IInputProcessor[] processors) => _processors = processors;

        public async Task Run(string[] inputs)
        {
            List<ICommandAsync> commands = default;
            try
            {
                commands = ExtractCommands(inputs);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Command processing failed. Reason: " + e.Message);
                return;
            }

            if (!commands.Any())
            {
                System.Console.WriteLine("No supported operation is recognized in input.");
                return;
            }

            for (int i = 0; i < commands.Count; i++)
            {
                var c = commands[i];
                var (canExecute, failReason) = c.CanExecute();
                if (!canExecute)
                {
                    System.Console.WriteLine($"Operation '{c.OperationName}' cannot execute under current state. Reason: " + failReason);
                    goto ReturnCancelled;
                }

                try
                {
                    await c.ExecuteAsync();
                }
                catch (Exception e)
                {
                    var msg = e is AggregateException ae ? ae.InnerException.Message : e.Message;
                    System.Console.WriteLine("Operation failed with message: " + msg);
                    goto ReturnCancelled;
                }

                continue;

                ReturnCancelled:
                var remainingOps = commands.Count - 1 - i;
                if (remainingOps > 0)
                    System.Console.WriteLine($"The execution of {remainingOps} operation(s) in queue is cancelled.");
                return;
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
