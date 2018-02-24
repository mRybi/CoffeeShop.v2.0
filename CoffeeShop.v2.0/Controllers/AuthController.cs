using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using CoffeeShop.v2.Data.Auth;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.v2.Dto;
using CoffeeShop.v2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CoffeeShop.v2.Controllers
{
    [Route("api/[controller]")]
    public class AuthController: Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;



        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {        
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserToRegister userToRegister)
        {
            //validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!string.IsNullOrEmpty(userToRegister.Username))
            {
                userToRegister.Username = userToRegister.Username.ToLower();
            }

            if (await _repo.UserExists(userToRegister.Username))
            {
                ModelState.AddModelError("Username", "Username already exists");
                return BadRequest(ModelState);
            }
               

            var userToCreate = _mapper.Map<User>(userToRegister);

            var createdUser = await _repo.Register(userToCreate, userToRegister.Password);


            return Ok(createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserToRegister userToLogin)
        {
            var userFromRepo = await _repo.Login(userToLogin.Username.ToLower(), userToLogin.Password);

            if (userFromRepo == null)
                return Unauthorized();
            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var key = Encoding.ASCII.GetBytes("super secret key"); //zmiana bo testowy hostserver nie radzi sobie z czytaniem appsettings

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var user = _mapper.Map<UserToReturnDto>(userFromRepo);
            var toReturn = new ReturnTokenAndUser
            {
                TokenString = tokenString,
                User = user
            };


            return Ok(toReturn);
        }
    }
}
