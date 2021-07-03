using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Paymentsense.Coding.Challenge.Api.Specs.TestFramework
{
    public static class TestServerSetup
    {
        private static TestServer _testServer;

        public static TestServer TestServer => _testServer ?? new TestServer(CreateWebHostBuilder());

        public static void StartServer()
        {
            if (_testServer != null)
            {
                _testServer = new TestServer(CreateWebHostBuilder());
            }
        }

        public static void StopServer()
        {
            if (_testServer != null)
            {
                _testServer.Dispose();
                _testServer = null;
            }
        }

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
