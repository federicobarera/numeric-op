using Microsoft.Extensions.Configuration;
using NumericOperator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericOperator
{
    public interface ICommandParser
    {
        IEnumerable<(string, decimal)> ParseInstructions(string instructions);
    }

    public class CommandParser : ICommandParser
    {
        private readonly IConfiguration _configuration;

        public CommandParser(IConfiguration configuration) {
            _configuration = configuration;
        }

        public IEnumerable<(string, decimal)> ParseInstructions(string instructions)
        {
            return instructions
                .Split(_configuration[Consts.SEPARATOR])
                .Select(x => x.Trim())
                .Where(x => !x.IsNullOrEmpty())
                .Select(x => x.Split(_configuration[Consts.SPLIT].ToCharArray()))
                .Select(x => {
                    if (x.Length != 2 || !decimal.TryParse(x[1], out decimal num))
                        throw new Exception($"command {String.Join(" ", x)} is invalid");

                    return (x[0], num);
                });
        }
    }
}
