using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.v2.Data;
using CoffeeShop.v2.Data.Auth;
using CoffeeShop.v2.Dto;
using CoffeeShop.v2.Helpers;
using CoffeeShop.v2.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CoffeeShop.Tests.Repositories
{
    public class AuthRepositoryTests
    {
        private readonly IMapper _mapper;
        public AuthRepositoryTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));
        }

        private void InitDbContext(DataContext context)
        {
            context.Users.Add(new User { Id = 1, Username = "ben", PaswordHash = { }, PaswordSalt = { } });
            context.Users.Add(new User { Id = 2, Username = "nick", PaswordHash = { }, PaswordSalt = { } });
            context.Users.Add(new User { Id = 3, Username = "bob", PaswordHash = { }, PaswordSalt = { } });
            context.SaveChanges();
        }

        [Fact(DisplayName ="login_proper_user_returns_that_user")]
        public async Task Test1()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "login_proper_user_returns_that_user");

            var context = new DataContext(dbContextOptions.Options);
            //InitDbContext(context);

            var repo = new AuthRepository(context);

            var userToRegister = new UserToRegister
            {
                Username = "jack",
                Password = "password"
            };
            var userToCreate = _mapper.Map<User>(userToRegister);
            await repo.Register(userToCreate, userToRegister.Password);
            var user = await repo.Login("jack", "password");


            Assert.NotNull(user);
            Assert.Equal(1, user.Id);

        }

        [Fact(DisplayName = "login_not_existing_user_returns_null")]
        public async Task Test2()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "login_not_existing_user_returns_null");

            var context = new DataContext(dbContextOptions.Options);
            //InitDbContext(context);

            var repo = new AuthRepository(context);
            var user = await repo.Login("jack", "password");


            Assert.Null(user);

        }

        [Fact(DisplayName = "login_user_with_wrong_password_returns_null")]
        public async Task Test3()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "login_user_with_wrong_password_returns_null");

            var context = new DataContext(dbContextOptions.Options);
            var repo = new AuthRepository(context);
            var user = await repo.Login("jack", "passwordddd");


            Assert.Null(user);
        }

    }
}
