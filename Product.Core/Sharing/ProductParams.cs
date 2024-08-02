using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core
{
    public class ProductParams
    {
        //Filter By Category
        public int? CategoryId { get; set; }

        //Sorting
        public string? Sorting { get; set; }
    }
}
