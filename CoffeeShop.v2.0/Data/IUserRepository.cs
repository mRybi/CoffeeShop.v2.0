using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.v2.Models;

namespace CoffeeShop.v2.Data
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<User> GetUser(int id);
        Task<User> GetUser(string username);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> SaveAll();
    }
}
