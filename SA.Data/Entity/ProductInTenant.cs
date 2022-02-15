using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Entity
{
    public class ProductInTenant :Base
    {
        public Guid TenantId { get; set; }
        public Guid  ProductId{ get; set; }
        public Tenant Tenant{ get; set; }
        public Product Product { get; set; }



    }
}
