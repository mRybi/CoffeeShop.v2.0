using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.v2.Dto
{
    public class ReturnTokenAndUser
    {
        public string TokenString { get; set; }
        public UserToReturnDto User { get; set; }
    }
}
