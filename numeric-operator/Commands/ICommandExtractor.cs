using Microsoft.Extensions.Configuration;
using NumericOperator.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NumericOperator
{
    public interface ICommandExtractor
    {
        Task<string> Extract();
    }

    public class TextFileExtractor : ICommandExtractor
    {
        private readonly IConfiguration _configuration;

        public TextFileExtractor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> Extract()
        {
            if (_configuration["source-file"].IsNullOrEmpty())
                throw new Exception("location must be specified");

            return File.ReadAllTextAsync(_configuration["source-file"]);
        }
    }
}
