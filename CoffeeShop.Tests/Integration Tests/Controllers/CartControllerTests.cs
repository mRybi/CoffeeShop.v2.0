using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Tests.Fixtures;
using CoffeeShop.v2.Dto;
using CoffeeShop.v2.Models;
using Newtonsoft.Json;
using Xunit;

namespace CoffeeShop.Tests.Controllers
{
    public class CartControllerTests
    {
        private readonly TestContext _sut;

        public CartControllerTests()
        {
            _sut = new TestContext();
        }

        private static StringContent GetRequestBody(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task SetUp()
        {
            int userId = 2;
            var item = new AddItemDto
            {
                IdItem = 1,
                Quantity = 2
            };

            var responseAdd = await _sut.Client.PostAsync($"api/users/{userId}/cart", GetRequestBody(item));
            var responseString = await responseAdd.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<Cart>(responseString);
        }

        private async Task LogginUser()
        {
            var userToLogin = new UserToRegister
            {
                Username = "John",
                Password = "password"
            };
            var logUser = await _sut.Client.PostAsync($"api/auth/login", GetRequestBody(userToLogin));
            var responseBody = await logUser.Content.ReadAsStringAsync();
            var tokenString = JsonConvert.DeserializeObject<ReturnTokenAndUser>(responseBody);

            var token = tokenString.TokenString;
            _sut.Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        }

        [Fact(DisplayName = "add_item_returns_ok")]
        public async Task add_item_returns_ok()
        {
            int userId = 2;
            await LogginUser();
         
            var item = new AddItemDto
            {
                IdItem = 1,
                Quantity = 2
            };

            var response = await _sut.Client.PostAsync($"api/users/{userId}/cart", GetRequestBody(item));
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "clear_cart_for_logged_user_returns_no_content")]
        public async Task clear_cart_for_logged_user_returns_no_content()
        {
            int userId = 2;
            await LogginUser();
            await SetUp();

            var clearResponse = await _sut.Client.PostAsync($"api/users/{userId}/cart/clear", null);
            clearResponse.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, clearResponse.StatusCode);
        }

        [Fact(DisplayName = "get_sum_for_userid_returns_ok_with_correct_sum")]
        public async Task get_sum_for_userid_returns_ok_with_correct_sum()
        {
            int userId = 2;
            await LogginUser();

            var item = new AddItemDto
            {
                IdItem = 1,
                Quantity = 2
            };

            var responseAdd = await _sut.Client.PostAsync($"api/users/{userId}/cart", GetRequestBody(item));
            var responseString = await responseAdd.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<Cart>(responseString);

            var price = cart.Coffee.Price * 2;

            var responseSum = await _sut.Client.GetAsync($"api/users/{userId}/cart/sum");
            responseSum.EnsureSuccessStatusCode();
            var responseSumString = await responseSum.Content.ReadAsStringAsync();
            var sum = JsonConvert.DeserializeObject<double>(responseSumString);

            Assert.Equal(HttpStatusCode.OK, responseSum.StatusCode);
            Assert.Equal(price, sum);



        }

        [Fact(DisplayName = "delete_item_with_proper_id_returns_no_content")]
        public async Task delete_item_with_proper_id_returns_no_content()
        {
            int userId = 2;
            await LogginUser();

            var item = new AddItemDto
            {
                IdItem = 1,
                Quantity = 2
            };

            var responseAdd = await _sut.Client.PostAsync($"api/users/{userId}/cart", GetRequestBody(item));
            var responseString = await responseAdd.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<Cart>(responseString);


            var response = await _sut.Client.PostAsync($"api/users/{userId}/cart/delete/{cart.Id}", null);
            //response.EnsureSuccessStatusCode();

            var responseGetCartsForUser = await _sut.Client.GetAsync($"api/users/{userId}/cart");
            response.EnsureSuccessStatusCode();
            var responseGetCartsString = await responseGetCartsForUser.Content.ReadAsStringAsync();
            var cartsForUser = JsonConvert.DeserializeObject<IEnumerable<ItemsFromCartDto>>(responseGetCartsString);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Empty(cartsForUser);
        }



    }
}
