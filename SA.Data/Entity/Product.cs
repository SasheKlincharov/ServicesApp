using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Entity
{
    public class Product:Base 
    {

        public Product() { ProductsInTenant = new List<ProductInTenant>(); }

        public string Name { get; set; }
        public string Price { get; set; }

        public virtual List<ProductInTenant> ProductsInTenant { get; set; }




    }
}
