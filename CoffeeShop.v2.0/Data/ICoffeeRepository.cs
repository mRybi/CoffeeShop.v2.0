using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.v2.Models;

namespace CoffeeShop.v2.Data
{
    public interface ICoffeeRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<Coffee> GetCoffee(int id);
        Task<IEnumerable<Coffee>> GetAll();

    }
}
