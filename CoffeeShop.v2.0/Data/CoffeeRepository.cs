using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.v2.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.v2.Data
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly DataContext _context;

        public CoffeeRepository(DataContext context)
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

        public async Task<IEnumerable<Coffee>> GetAll()
        {
            return await _context.Coffees.ToListAsync();
        }

        public async Task<Coffee> GetCoffee(int id)
        {
            return await _context.Coffees.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
