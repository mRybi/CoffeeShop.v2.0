using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.v2.Data;
using CoffeeShop.v2.Models;
using Moq;
using Xunit;

namespace CoffeeShop.Tests
{
    public class CoffeeServiceTests
    {
        public ICoffeeRepository _repo;
        //public Mock<DataContext> dataContextMock;
        public CoffeeServiceTests(ICoffeeRepository repo)
        {
            _repo = repo;
        }

        [Fact]
        public void passing_test()
        {
            Assert.True(true);
        }


        [Fact]
        public async Task returns_coffee_when_proper_id_is_given()
        {
            //dataContextMock = new Mock<DataContext>();
           var coffee =  await _repo.GetCoffee(2);

            

            Assert.NotNull(coffee);
        }
    }
}
