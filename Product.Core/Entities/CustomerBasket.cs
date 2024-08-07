using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Entities
{
    // Gio hang
    public class CustomerBasket
    {
        public CustomerBasket()
        {

        }
        public CustomerBasket(String Id)
        {
            Id = Id;
        }

        public String Id { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
