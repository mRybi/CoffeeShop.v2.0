using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.v2.Data;
using CoffeeShop.v2.Dto;
using CoffeeShop.v2.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.v2.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    public class CartController:Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly ICoffeeRepository _coffeeRepo;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepo, ICoffeeRepository coffeeRepo, IMapper mapper)
        {
            _cartRepo = cartRepo;
            _coffeeRepo = coffeeRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int userId, [FromBody] AddItemDto itemDto)
        {
            if (userId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                return Unauthorized();

            var newItem = await _cartRepo.AddItem(itemDto.IdItem, itemDto.Quantity, userId);

            if (await _cartRepo.SaveAll())
                return Ok(newItem);

            throw new Exception("Failed to add item");
        }

        [HttpPost("clear")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            if (userId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                return Unauthorized();

            var items = await _cartRepo.GetCartsForUser(userId);
            foreach (var item in items)
            {
                _cartRepo.Delete(item);
            }

            if (await _cartRepo.SaveAll())
                return NoContent();

            throw new Exception("error deleting cart items");
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteItem(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                return Unauthorized();

            var item = await _cartRepo.GetCart(id);
            if (item == null)
                return BadRequest();

             _cartRepo.Delete(item);
            if (await _cartRepo.SaveAll())
                return NoContent();

            throw new Exception("there was a problem deleting item");
        }


        [HttpGet]
        public async Task<IActionResult> GetCartsForUser(int userId)
        {
            if (userId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                return Unauthorized();

            var carts = await _cartRepo.GetCartsForUser(userId);
            var cartsToReturn = _mapper.Map<IEnumerable<ItemsFromCartDto>>(carts);

            return Ok(cartsToReturn);
        }

        [HttpGet("sum")]
        public async Task<IActionResult> GetSum(int userId)
        {
            if (userId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                return Unauthorized();

            var sum = await _cartRepo.GetSum(userId);

            return Ok(sum);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetCart(int id)
        //{
        //    var cartFromRepo =await _cartRepo.GetCart(id);

        //    if (cartFromRepo == null)
        //        return BadRequest();

        //    var cartToReturn = _mapper.Map<ItemsFromCartDto>(cartFromRepo);
        //    return Ok(cartToReturn);
        //}
       

    }
}
