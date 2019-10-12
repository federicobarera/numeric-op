using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericOperator
{
    public interface ICommandLexer
    {
        (decimal, Stack<(string, decimal)>) CreateExecutableStack(IEnumerable<(string, decimal)> commands);
    }

    public class CommandLexer : ICommandLexer
    {
        public (decimal, Stack<(string, decimal)>) CreateExecutableStack(IEnumerable<(string, decimal)> commands)
        {
            var (applyCommand, initiator) = commands.Last();

            if (applyCommand != "apply")
                throw new Exception("last command must be apply");

            var commandStack = new Stack<(string, decimal)>(
                    commands
                        .Take(commands.Count() - 1)
                        .Reverse());

            return (initiator, commandStack);
        }
    }
}
