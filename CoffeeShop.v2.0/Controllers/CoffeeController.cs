using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.v2.Data;
using CoffeeShop.v2.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.v2.Controllers
{
    [Route("api/[controller]")]
    public class CoffeeController: Controller
    {
        private readonly ICoffeeRepository _repo;

        public CoffeeController(ICoffeeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoffee(int id)
        {
            var coffee = await _repo.GetCoffee(id);
            if (coffee == null)
                return  NotFound();
            return Ok(coffee);
        }

        [HttpGet]
        public async Task<IActionResult> getCoffees()
        {
            var coffees = await _repo.GetAll();
            if (coffees == null)
                return NotFound();
            return Ok(coffees);
        }
    }
}
