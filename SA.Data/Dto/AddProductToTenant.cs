using Microsoft.AspNetCore.Mvc.Rendering;
using SA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Dto
{
    public class AddProductToTenant
    {
        public string TenantId { get; set; }
        public string ProductId { get; set; }
        public List<SelectListItem> AllProducts { get; set; }
    }
}
