using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.v2.Data;
using CoffeeShop.v2.Helpers;
using CoffeeShop.v2.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoffeeShop.Tests.Repositories
{
    public class CoffeeRepositoryTests
    {
        private readonly IMapper _mapper;
        public CoffeeRepositoryTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));
        }

        private void InitDbContext(DataContext context)
        {
            context.Coffees.Add(new Coffee { Id = 1, Capacity = 100, Description=" ", Name="caffee1", PhotoUrl="photourl", Price=7.50});
            context.Coffees.Add(new Coffee { Id = 2, Capacity = 150, Description = " ", Name = "caffee2", PhotoUrl = "photourl2", Price = 8.50 });
            context.Coffees.Add(new Coffee { Id = 3, Capacity = 200, Description = " ", Name = "caffee3", PhotoUrl = "photourl3", Price = 9.50 });
            context.SaveChanges();
        }

        [Fact(DisplayName = "get_all_coffees_returns_coffees_list")]
        public async Task Test1()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "get_all_coffees_returns_coffees_list");

            var context = new DataContext(dbContextOptions.Options);
            InitDbContext(context);

            var repo = new CoffeeRepository(context);
            var coffees = await repo.GetAll();

            Assert.NotEmpty(coffees);

        }

        [Fact(DisplayName = "get_coffee_id_returns_coffee")]
        public async Task Test2()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "get_coffee_id_returns_coffee");

            var context = new DataContext(dbContextOptions.Options);
            InitDbContext(context);

            var repo = new CoffeeRepository(context);
            var coffee = await repo.GetCoffee(1);

            Assert.NotNull(coffee);
            Assert.Equal(100, coffee.Capacity);

        }
    }
}
