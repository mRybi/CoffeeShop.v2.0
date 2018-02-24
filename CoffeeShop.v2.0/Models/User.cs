using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.v2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PaswordHash { get; set; }

        public byte[] PaswordSalt { get; set; }
        //public Cart Cart { get; set; }

        //public User()
        //{
        //    Cart = new Cart();
        //}
    }
}
