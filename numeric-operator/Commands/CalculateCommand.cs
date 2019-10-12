using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumericOperator
{
    public class CalculateCommand
    {
        private readonly ICommandParser _commandParser;
        private readonly ICommandLexer _commandLexer;
        private readonly ICommandExtractor _commandExtractor;
        private readonly IDictionary<string, Operator> _operators;

        public CalculateCommand(
            ICommandParser commandParser,
            ICommandLexer commandLexer,
            ICommandExtractor commandExtractor,
            IDictionary<string, Operator> operators)
        {
            _commandParser = commandParser;
            _commandLexer = commandLexer;
            _commandExtractor = commandExtractor;
            _operators = operators;
        }

        public async Task<decimal> Calculate() {
            var (initiator, commandStack) = _commandLexer
                .CreateExecutableStack(
                    _commandParser.ParseInstructions(
                        await _commandExtractor.Extract().ConfigureAwait(false)));

            decimal result = initiator;
            while (commandStack.TryPop(out var instruction))
            {
                var (command, value) = instruction;
                result = _operators[command].Invoke(result, value);
            }

            return result;
        }
    }
}
