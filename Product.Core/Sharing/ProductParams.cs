using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core
{
    public class ProductParams
    {
        //Page size
        public int maxpagesize { get; set; } = 5;
        private int pagesize = 3;
        public int Pagesize { 
            get => pagesize;
            set => pagesize = value > maxpagesize ? maxpagesize : value; }
        public int PageNumber { get; set; } = 1;

        //Filter By Category
        public int? CategoryId { get; set; }

        //Sorting
        public string? Sorting { get; set; }

        //Search
        private string? _Search;
        public string? Search
        {
            get => _Search;
            set => _Search = value.ToLower();
        }
        
    }
}
