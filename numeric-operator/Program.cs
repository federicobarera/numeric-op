using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericOperator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(DefaultConfigs)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<ICommandExtractor, TextFileExtractor>()
                .AddSingleton<ICommandLexer, CommandLexer>()
                .AddSingleton<ICommandParser, CommandParser>()
                .AddSingleton<CalculateCommand>()
                .AddSingleton<IDictionary<string, Operator>>(svc => new Dictionary<string, Operator>() {
                    { Consts.Operators.ADD, Operators.Add },
                    { Consts.Operators.SUBTRACT, Operators.Subtract },
                    { Consts.Operators.MULTIPLY, Operators.Multiply },
                    { Consts.Operators.DIVIDE, Operators.Divide },
                })
                .BuildServiceProvider();

            var result = serviceProvider
                .GetService<CalculateCommand>()
                .Calculate()
                .Result;

            Console.WriteLine(result);
        }

        public static List<KeyValuePair<string, string>> DefaultConfigs => new Dictionary<string, string>() {
                { Consts.SEPARATOR, Environment.NewLine },
                { Consts.SPLIT, " " }}.ToList();
    }
}
