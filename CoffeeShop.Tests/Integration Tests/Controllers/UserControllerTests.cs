using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Tests.Fixtures;
using CoffeeShop.v2.Models;
using Newtonsoft.Json;
using Xunit;

namespace CoffeeShop.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly TestContext _sut;

        public UserControllerTests()
        {
            _sut = new TestContext();
        }

        [Fact(DisplayName = "get_users_returns_ok")]
        public async Task get_users_returns_ok()
        {
            var response = await _sut.Client.GetAsync($"api/user");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(responseString);

            Assert.NotNull(users);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact(DisplayName = "getting_user_with_proper_id_returns_ok")]
        public async Task getting_user_with_proper_id_returns_ok()
        {
            var userId = 2;

            var response = await _sut.Client.GetAsync($"api/user/{userId}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(responseString);

            Assert.NotNull(user);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(userId, user.Id);
        }
    }
}
