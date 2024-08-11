using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Dto
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodID { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}
