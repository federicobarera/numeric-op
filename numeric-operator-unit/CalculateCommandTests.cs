using Microsoft.Extensions.DependencyInjection;
using Moq;
using NumericOperator.UnitTests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NumericOperator.UnitTest
{
    public class CalculateCommandTests
    {
        [Theory]
        [InlineData("apply 3", 3)]
        [InlineData("multiply 3|apply 3", 9)]
        [InlineData("add 2|apply 3", 5)]
        [InlineData("subtract 1|apply 1", 0)]
        [InlineData("divide 2    |apply 1", 0.5)]
        public async Task ShouldReturnExpectedResults(string instruction, decimal expected)
        {
            var extractorMock = CreateCommandExtractorMock(instruction);
            var command = 
                new CommandFixture(new Dictionary<string, string> {
                    { Consts.SEPARATOR, "|" }
                })
                .Create(svc => {
                    svc.AddSingleton<ICommandExtractor>(extractorMock);
                });

            Assert.Equal(expected, await command.Calculate());
        }

        [Theory]
        [InlineData("multiply 3")]
        [InlineData("apply 5 4")]
        [InlineData("unknown 2|apply 3")]
        [InlineData("divide 0|apply 1")]
        public async Task ShouldThrowException(string instruction)
        {
            var extractorMock = CreateCommandExtractorMock(instruction);
            var command =
                new CommandFixture(new Dictionary<string, string> {
                    { Consts.SEPARATOR, "|" }
                })
                .Create(svc => {
                    svc.AddSingleton<ICommandExtractor>(extractorMock);
                });

            await Assert.ThrowsAnyAsync<Exception>(command.Calculate);
        }

        private ICommandExtractor CreateCommandExtractorMock(string instruction)
        {
            var commandExtractorMock = new Mock<ICommandExtractor>();
            commandExtractorMock
                .Setup(x => x.Extract())
                .Returns(Task.FromResult(instruction));
            return commandExtractorMock.Object;
        }
    }
}
