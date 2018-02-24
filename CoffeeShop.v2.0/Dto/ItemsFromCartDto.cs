using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.v2.Dto
{
    public class ItemsFromCartDto
    {
        public int Id { get; set; }
        public int ItemsQuantity { get; set; } = 0;

        public int CoffeeId { get; set; }
        public CoffeeDto Coffee { get; set; }
    }
}
