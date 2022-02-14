using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Dto
{
    public class CreateTenantDto
    {
        public TenantDto Tenant { get; set; }
        public List<SelectListItem> Users { get; set; }
    }
}
