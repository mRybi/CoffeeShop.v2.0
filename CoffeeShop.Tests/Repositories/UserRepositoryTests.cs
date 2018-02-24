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
    public class UserRepositoryTests
    {
        private readonly IMapper _mapper;
        public UserRepositoryTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));

        }

        private void InitDbContext(DataContext context)
        {
            context.Users.Add(new User { Id = 1, Username = "nick", PaswordHash = { }, PaswordSalt = { } });
            context.Users.Add(new User { Id = 2, Username = "ben", PaswordHash = { }, PaswordSalt = { } });
            context.Users.Add(new User { Id = 3, Username = "jack", PaswordHash = { }, PaswordSalt = { } });
            context.SaveChanges();
        }

        [Fact(DisplayName = "get_all_users_returns_user_lists")]
        public async Task Test1()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "get_all_users_returns_user_lists");

            var context = new DataContext(dbContextOptions.Options);
            InitDbContext(context);

            var repo = new UserRepositry(context);
            var users = await repo.GetUsers();

            Assert.NotEmpty(users);

        }

        [Fact(DisplayName = "get_user_string_username_returns_user")]
        public async Task Test2()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "get_user_string_username_returns_user");

            var context = new DataContext(dbContextOptions.Options);
            InitDbContext(context);

            var repo = new UserRepositry(context);
            var user = await repo.GetUser("nick");

            Assert.NotNull(user);
            Assert.Equal("nick", user.Username);
        }

        [Fact(DisplayName = "get_user_int_id_returns_user")]
        public async Task Test3()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "get_user_int_id_returns_user");

            var context = new DataContext(dbContextOptions.Options);
            InitDbContext(context);

            var repo = new UserRepositry(context);
            var id =await repo.GetUser("nick");
            var user = await repo.GetUser(id.Id);

            Assert.NotNull(user);
            Assert.Equal("nick", user.Username);
        }
    }
}
