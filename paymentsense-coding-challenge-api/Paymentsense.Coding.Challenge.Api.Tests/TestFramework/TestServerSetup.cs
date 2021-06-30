using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Paymentsense.Coding.Challenge.Api.Tests.TestFramework
{
    public static class TestServerSetup
    {
        private static readonly TestServer _testServer;

        public static TestServer TestServer => _testServer ?? new TestServer(CreateWebHostBuilder());

        private static IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder()
                        .UseEnvironment("Development")
                        .UseConfiguration(new ConfigurationBuilder()
                            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), @"appsettings.json"))
                            .Build())
                        .UseStartup<Startup>();
        }
    }
}
