using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.v2.Dto
{
    public class CoffeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
    }
}
