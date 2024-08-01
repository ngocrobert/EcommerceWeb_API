using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core
{
    public class Category : BasicEntity
    {
        //public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public virtual ICollection<Products> Products { get; set; } = new HashSet<Products>();
    }
}
