using SA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Repository.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsForCategory(Guid categoryId);
    }
}
