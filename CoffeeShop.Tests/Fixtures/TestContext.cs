using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using CoffeeShop.v2._0;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace CoffeeShop.Tests.Fixtures
{
    public class TestContext
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }

        public TestContext()
        {
            SetUpClient();
        }

        private void SetUpClient()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());


            Client = _server.CreateClient();
        }
    }
}
