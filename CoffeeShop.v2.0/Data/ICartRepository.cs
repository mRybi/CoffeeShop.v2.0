using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.v2.Models;

namespace CoffeeShop.v2.Data
{
    public interface ICartRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<Cart> GetCart(int id);
        Task<Cart> AddItem(int idCoffe, int quantity, int userId);
        Task<IEnumerable<Cart>> GetCarts();
        Task<IEnumerable<Cart>> GetCartsForUser(int userId);
        Task<double> GetSum(int userId);
    }
}
