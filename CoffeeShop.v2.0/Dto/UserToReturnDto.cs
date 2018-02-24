using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.v2.Dto
{
    public class UserToReturnDto
    {
        public string Username { get; set; }
        public CartDto Cart { get; set; }
    }
}