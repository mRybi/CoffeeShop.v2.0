using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.v2.Models;
using Newtonsoft.Json;

namespace CoffeeShop.v2.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedCoffees()
        {
            var coffeeData = System.IO.File.ReadAllText("Data/CoffeeSeedData.json");
            var coffees = JsonConvert.DeserializeObject<List<Coffee>>(coffeeData);

            foreach (var coffee in coffees)
            {
                _context.Coffees.Add(coffee);
            }

            _context.SaveChanges();
        }

        public void SeedCart()
        {

            var cart = new Cart();
            

            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void DeleteCoffees() // delete repeated coffees
        {
            _context.Coffees.RemoveRange(_context.Coffees.Where(c=>c.Id>9));
            _context.SaveChanges();
        }

    }
}
