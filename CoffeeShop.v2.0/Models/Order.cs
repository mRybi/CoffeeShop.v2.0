using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.v2.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsSent { get; set; }
        //zmienic na public ICollection<Cart> Carts { get; set; } dodac migacje i zmienic w mapperze + dodac mapowanie
        //na karta
        //public ICollection<Coffee> Coffees { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public DateTime CreatedAt { get; set; }

        public Order()
        {
            // Carts = new Collection<Cart>();
            Carts = new Collection<Cart>();
            CreatedAt = DateTime.Now;
            IsSent = false;
        }
    }
}
