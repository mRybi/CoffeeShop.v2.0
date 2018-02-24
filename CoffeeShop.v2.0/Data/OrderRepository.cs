using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.v2.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.v2.Data
{
    public class OrderRepository:IOrderRepository
    {
        private readonly DataContext _context;


        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<Order> AddUserToCart(Order order, int userId)
        {
            var list = order.Carts;
            foreach (var item in list)
            {
                item.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            }

            return order;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var ordersList = await _context.Orders.ToListAsync();
            var cartsList = await _context.Carts.ToListAsync();

            foreach (var item in ordersList)
            {
                item.Carts = cartsList;
            }
            return ordersList;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
