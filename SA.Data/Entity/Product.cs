using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Entity
{
    public class Product : Base
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Image { get; set; }

        public virtual List<ProductInTenant> ProductsInTenant { get; set; }
        public Product()
        {
            ProductsInTenant = new List<ProductInTenant>();
        }

    }
}
