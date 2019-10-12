using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace NumericOperator.UnitTests
{
    public class CommandFixture
    {
        private readonly Dictionary<string, string> _configs;

        public CommandFixture(Dictionary<string, string> configs = null)
        {
            _configs = configs ?? new Dictionary<string, string>();
        }
        public CalculateCommand Create(Action<IServiceCollection> configDelegate) {

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(Program.DefaultConfigs)
                .AddInMemoryCollection(_configs)
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddSingleton<IConfiguration>(config)
                .AddSingleton<ICommandExtractor, TextFileExtractor>()
                .AddSingleton<ICommandLexer, CommandLexer>()
                .AddSingleton<ICommandParser, CommandParser>()
                .AddSingleton<CalculateCommand>()
                .AddSingleton<IDictionary<string, Operator>>(svc => new Dictionary<string, Operator>() {
                    { "add", Operators.Add },
                    { "subtract", Operators.Subtract },
                    { "multiply", Operators.Multiply },
                    { "divide", Operators.Divide },
                });

            configDelegate.Invoke(serviceCollection);

            return serviceCollection
                .BuildServiceProvider()
                .GetService<CalculateCommand>();
        }
    }
}
