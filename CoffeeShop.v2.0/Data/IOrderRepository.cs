using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.v2.Models;

namespace CoffeeShop.v2.Data
{
    public interface IOrderRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<Order> GetOrder(int id);
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> AddUserToCart(Order order, int userId);
        Task<bool> SaveAll();
    }
}
