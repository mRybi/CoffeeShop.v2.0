using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.v2.Data;
using CoffeeShop.v2.Dto;
using CoffeeShop.v2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.v2.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    public class OrderController:Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        private readonly ICartRepository _cartRepo;

        public OrderController(IMapper mapper, IOrderRepository repo, ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _repo.GetOrders();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(int userId, [FromBody] OrderToCreateDto createDto)
        {
            var carts = await _cartRepo.GetCartsForUser(userId);
            //var cartsToReturn = _mapper.Map<IEnumerable<CartDto>>(carts);

            var order = _mapper.Map<Order>(createDto);
            order.Carts = (ICollection<Cart>)carts;

            await _repo.AddUserToCart(order, userId);

            _repo.Add(order);

            var orderToReturn = _mapper.Map<OrderSummaryDto>(order);
            orderToReturn.SumToPay = await _cartRepo.GetSum(userId);

            if (await _repo.SaveAll())
                return Ok(orderToReturn);

            throw new Exception("Error adding order");
        }
    }
}
