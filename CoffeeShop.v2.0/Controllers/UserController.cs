using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.v2.Data;
using CoffeeShop.v2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.v2.Controllers
{
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
