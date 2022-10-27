using Campaign.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

namespace Campaign.Test.Contexts
{
    public class TestContext : IDisposable
    {
        private TestServer _testServer;
        public HttpClient TestClient { get; private set; }

        public TestContext()
        {
            SetUpContext();
        }

        private void SetUpContext()
        {
            var configDirectory = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json");

            _testServer = new TestServer(new WebHostBuilder().ConfigureAppConfiguration((appContext, configuration) =>
            {
                configuration.AddJsonFile(configDirectory);
            }).UseStartup<Startup>());

            TestClient = _testServer.CreateClient();
        }

        public void Dispose()
        {
            _testServer?.Dispose();
            TestClient?.Dispose();
        }
    }
}