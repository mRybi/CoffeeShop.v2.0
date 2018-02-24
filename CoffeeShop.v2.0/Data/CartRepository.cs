using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.v2.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.v2.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;

        public CartRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Cart> GetCart(int id)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cart> AddItem(int idCoffee, int quantity, int userId)
        {

            var coffee = await _context.Coffees.FirstOrDefaultAsync(x => x.Id == idCoffee);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new Exception("There is no user with that id");

            var cart = new Cart
            {
                ItemsQuantity = quantity,
                Coffee = coffee,
                User = user
            };
            

            await _context.Carts.AddAsync(cart);
            return cart;

        }

        public async Task<IEnumerable<Cart>> GetCarts()
        {
            var list = await _context.Carts.ToListAsync();

            foreach (var item in list)
            {
                item.Coffee = await _context.Coffees.FirstOrDefaultAsync(c => c.Id == item.CoffeeId);
            }
            return list;
        }


        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<double> GetSum(int userId)
        {
            var items = await GetCartsForUser(userId);
            var sum = 0.0;

            foreach (var item in items)
            {
                sum += item.ItemsQuantity * item.Coffee.Price;
            }

            return sum;

        }

        public async Task<IEnumerable<Cart>> GetCartsForUser(int id)
        {
            var lists = await _context.Carts.Where(u => u.User.Id == id).ToListAsync();

            foreach (var item in lists)
            {
                item.Coffee = await _context.Coffees.FirstOrDefaultAsync(c => c.Id == item.CoffeeId);
            }
            return lists;
        }
    }
}
