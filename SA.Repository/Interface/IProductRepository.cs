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
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(Guid id);
        Task<Product> Insert(Product Product);
        void Delete(Product Product);
        void Update(Product p);
    }
}
