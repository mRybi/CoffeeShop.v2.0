using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Tests.Fixtures;
using CoffeeShop.v2._0;
using CoffeeShop.v2.Models;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace CoffeeShop.Tests.Controllers
{
    public class CoffeeControllerTests
    {
        private readonly TestContext _sut;

        public CoffeeControllerTests()
        {
            _sut = new TestContext();
        }

        [Fact(DisplayName = "get_coffees_returns_ok")]
        public async Task get_coffees_returns_ok()
        {
            var response = await _sut.Client.GetAsync("api/coffee");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var coffees = JsonConvert.DeserializeObject<IEnumerable<Coffee>>(responseString);

            Assert.NotNull(coffees);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "get_coffee_id_returns_ok_when_proper_id")]
        public async Task get_coffee_id_returns_ok_when_proper_id()
        {
            int id = 3;
            var response = await _sut.Client.GetAsync($"api/coffee/{id}");
            response.EnsureSuccessStatusCode();
            var responseString =await response.Content.ReadAsStringAsync();
            var coffee = JsonConvert.DeserializeObject<Coffee>(responseString);

            Assert.NotNull(coffee);
            Assert.Equal(id, coffee.Id);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "get_coffee_id_returns_not_found_when_wrong_id")]
        public async Task get_coffee_id_returns_not_found_when_wrong_id()
        {
            int id = 10000;
            var response = await _sut.Client.GetAsync($"api/coffee/{id}");
            var responseString =await response.Content.ReadAsStringAsync();
            var coffee = JsonConvert.DeserializeObject<Coffee>(responseString);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(coffee);
        }

    }
}
