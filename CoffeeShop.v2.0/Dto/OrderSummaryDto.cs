using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.v2.Models;

namespace CoffeeShop.v2.Dto
{
    public class OrderSummaryDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsSent { get; set; }
        public ICollection<CartDto> Carts { get; set; }
        public DateTime CreatedAt { get; set; }
        public double SumToPay { get; set; }
    }
}
