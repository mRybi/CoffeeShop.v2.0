using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.v2.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ItemsQuantity { get; set; } = 0;

        public int CoffeeId { get; set; }
        public Coffee Coffee { get; set; }
        public User User { get; set; }
        //public Order Order { get; set; }

        public Cart()
        {

        }
    }
}
