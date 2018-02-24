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
{//DOKONCZYC CARTA
    public class CartRepositoryTests
    {
        private readonly IMapper _mapper;
        public CartRepositoryTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));
        }

        private void InitDbContext(DataContext context)
        {
            context.Carts.Add(new Cart { Id = 1, CoffeeId = 1, ItemsQuantity = 1, Coffee = new Coffee(), User = new User()});
            context.Carts.Add(new Cart { Id = 2, CoffeeId = 2, ItemsQuantity = 2, Coffee = new Coffee(), User = new User() });
            context.Carts.Add(new Cart { Id = 3, CoffeeId = 3, ItemsQuantity = 3, Coffee = new Coffee(), User = new User() });
            context.SaveChanges();
        }

        [Fact(DisplayName = "get_cart_id_returns_cart")]
        public async Task Test2()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "get_cart_id_returns_cart");

            var context = new DataContext(dbContextOptions.Options);
            InitDbContext(context);

            var repo = new CartRepository(context);
            var cart = await repo.GetCart(1);

            Assert.NotNull(cart);
            Assert.Equal(1, cart.Id);

        }

        [Fact(DisplayName = "get_all_carts_returns_carts_list")]
        public async Task Test1()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "get_all_carts_returns_carts_list");

            var context = new DataContext(dbContextOptions.Options);
            InitDbContext(context);

            var repo = new CoffeeRepository(context);
            var carts = await repo.GetAll();

            Assert.NotEmpty(carts);

        }

        [Fact(DisplayName ="adding_item_to_not_existing_user_throws_exception")]
        public async Task Test3()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
                databaseName: "adding_item_to_not_existing_user_throws_exception");

            var context = new DataContext(dbContextOptions.Options);
            context.Users.Add(new User { Username = "user100", PaswordHash = { }, PaswordSalt = { } });
            context.Coffees.Add(new Coffee { Id = 100, Capacity = 100, Description = "desc", Name = "caffee100", PhotoUrl = "url", Price = 3.5 });
            
            var repo = new CartRepository(context);

            //var itemToAdd = await repo.AddItem(100, 2, 100);
            Exception ex = await Assert.ThrowsAsync<Exception>(() => repo.AddItem(100, 2, 100));
    
            Assert.Equal("There is no user with that id", ex.Message);


        }

        [Fact(DisplayName = "adding_proper_item_to_existing_user_returns_added_cart")]
        public async Task Test4()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
                databaseName: "adding_proper_item_to_existing_user_returns_added_cart");

            var context = new DataContext(dbContextOptions.Options);
            context.Users.Add(new User {Id = 100, Username = "user100", PaswordHash = { }, PaswordSalt = { } });
            context.Coffees.Add(new Coffee { Id = 100, Capacity = 100, Description = "desc", Name = "caffee100", PhotoUrl = "url", Price = 3.5 });
            context.SaveChanges();
            var repo = new CartRepository(context);
            var repoUsers = new UserRepositry(context);
            var user = await repoUsers.GetUser("user100");

            Assert.NotNull(user);
            var itemToAdd = await repo.AddItem(100, 2, user.Id);

            Assert.Equal(100, itemToAdd.CoffeeId);


        }

        [Fact(DisplayName = "get_sum_for_user_returns_float_sum")]
        public async Task Test5()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
                databaseName: "get_sum_for_user_returns_float_sum");

            var context = new DataContext(dbContextOptions.Options);
           
            context.Carts.Add(new Cart { Id = 1, CoffeeId = 100, ItemsQuantity = 1, Coffee = new Coffee { Id = 100, Capacity = 100, Description = "desc", Name = "caffee100", PhotoUrl = "url", Price = 3.5 }, User = new User { Id = 100, Username = "user100", PaswordHash = { }, PaswordSalt = { } } });
            context.Carts.Add(new Cart { Id = 2, CoffeeId = 100, ItemsQuantity = 1, Coffee = new Coffee { Id = 100, Capacity = 100, Description = "desc", Name = "caffee100", PhotoUrl = "url", Price = 3.5 }, User = new User { Id = 100, Username = "user100", PaswordHash = { }, PaswordSalt = { } } });
            context.Carts.Add(new Cart { Id = 3, CoffeeId = 100, ItemsQuantity = 1, Coffee = new Coffee { Id = 100, Capacity = 100, Description = "desc", Name = "caffee100", PhotoUrl = "url", Price = 3.5 }, User = new User { Id = 100, Username = "user100", PaswordHash = { }, PaswordSalt = { } } });
            context.SaveChanges();
            var repo = new CartRepository(context);
            var repoUsers = new UserRepositry(context);
            var user = await repoUsers.GetUser("user100");

            Assert.NotNull(user);
            var sum = await repo.GetSum(user.Id);

            Assert.NotNull(sum);
            Assert.Equal(10.5, sum);


        }
    }
}
