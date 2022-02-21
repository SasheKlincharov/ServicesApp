using Microsoft.AspNetCore.Mvc.Rendering;
using SA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Dto
{
    public class EditProductDto
    {
        public Product Product { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
