using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CoffeeShop.Tests.Fixtures;
using CoffeeShop.v2.Dto;
using CoffeeShop.v2.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace CoffeeShop.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly TestContext _sut;
        public AuthControllerTests()
        {
            _sut = new TestContext();
        }

        private StringContent GetRequestBody(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        [Fact(DisplayName = "login_proper_user_returns_token_and_ok")]
        public async Task login_proper_user_returns_token_and_ok()
        {
            var userToLogin = new UserToRegister
            {
                Username = "John",
                Password = "password"
            };
            var logUser = await _sut.Client.PostAsync($"api/auth/login", GetRequestBody(userToLogin));
            logUser.EnsureSuccessStatusCode();
            var responseBody = await logUser.Content.ReadAsStringAsync();
            var tokenString = JsonConvert.DeserializeObject<ReturnTokenAndUser>(responseBody);

            var token = tokenString.TokenString;

            Assert.NotNull(token);
            Assert.Equal(HttpStatusCode.OK, logUser.StatusCode);
            Assert.IsType<string>(token);   
        }

        [Fact(DisplayName = "login_user_that_not_exists_returns_unauthorized")]
        public async Task login_user_that_not_exists_returns_unauthorized()
        {
            var userToLogin = new UserToRegister
            {
                Username = "qwerty",
                Password = "asdfgh"
            };

            var response =await _sut.Client.PostAsync($"api/auth/login", GetRequestBody(userToLogin));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact(DisplayName = "giving_wrong_user_to_register_returns_badrequest_and_modelstate")]
        public async Task giving_wrong_user_to_register_returns_badrequest_and_modelstate()
        {
            var userToLogin = new UserToRegister
            {
                Username = "qwerty",
                Password = ""
            };

            var response = await _sut.Client.PostAsync($"api/auth/register", GetRequestBody(userToLogin));

            var responseString = await response.Content.ReadAsStringAsync();
            var modelState = JsonConvert.DeserializeObject<ModelState>(responseString);

            Assert.NotNull(modelState.Errors.Count);
        }

        [Fact(DisplayName = "giving_username_that_already_exists_returns_badrequest_and_modelstate")]
        public async Task giving_username_that_already_exists_returns_badrequest_and_modelstate()
        {
            var userToRegister = new UserToRegister
            {
                Username = "Nick",
                Password = "password"
            };

            var response = await _sut.Client.PostAsync($"api/auth/register", GetRequestBody(userToRegister));

            var responseString = await response.Content.ReadAsStringAsync();
            var modelState = JsonConvert.DeserializeObject<ModelState>(responseString);

            Assert.NotNull(modelState.Errors.Count);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "registering_valid_user_succeeds_returns_ok")]
        public async Task registering_valid_user_succeeds_returns_ok()
        {
            var userToRegister = new UserToRegister
            {
                Username = "Andy",
                Password = "password"
            };

            var response =await _sut.Client.PostAsync("api/auth/register", GetRequestBody(userToRegister));
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<User>(responseString);

            Assert.NotNull(createdUser);
            Assert.Equal(userToRegister.Username.ToLower(), createdUser.Username);
            Assert.NotNull(createdUser.PaswordHash);
        }
    }
}
