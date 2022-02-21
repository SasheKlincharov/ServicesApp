using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Dto
{
    public class CreateProductDto
    {
        public ProductDto Product { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
