using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Tests.Fixtures;
using Xunit;
using System.Net.Http;
using Newtonsoft.Json;
using CoffeeShop.v2.Models;
using System.Net;
using CoffeeShop.v2.Dto;

namespace CoffeeShop.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly TestContext _sut;

        public OrderControllerTests()
        {
            _sut = new TestContext();
        }

        [Fact(DisplayName = "get_orders_returns_ok")]
        public async Task get_orders_returns_ok()
        {
            var userId = 2;
            var response = await _sut.Client.GetAsync($"api/users/{userId}/order");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(responseString);

            Assert.NotNull(orders);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact(DisplayName = "adding_proper_order_returns_ok")]
        public async Task adding_proper_order_returns_ok()
        {
            var userId = 2;
            var order = new OrderToCreateDto
            {
                Email = "test@google.pl",
                Name = "Bob",
                Surname = "Bob",
                Country = "UK",
                City = "London",
                Street = "Riverland",
                HouseNumber = 1,
                PhoneNumber = 123456
            };
       
            var response = await _sut.Client.PostAsync($"api/users/{userId}/order",GetPostContent(order));
            response.EnsureSuccessStatusCode();
            var responseString =await response.Content.ReadAsStringAsync();
            var orderReturned = JsonConvert.DeserializeObject<OrderSummaryDto>(responseString);

            Assert.NotNull(orderReturned);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(order.City, orderReturned.City);
        }



        private static StringContent GetPostContent(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
