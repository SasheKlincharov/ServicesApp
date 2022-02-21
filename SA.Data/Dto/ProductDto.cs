using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public Guid CategoryId { get; set; }
        public string Image { get; set; }



    }
}
