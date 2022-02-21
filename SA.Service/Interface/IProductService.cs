using SA.Data.Dto;
using SA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Service.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();

        Task<Product> GetProduct(Guid? id);

        Task<Product> CreateNewProduct(ProductDto Product);

        void DeleteProduct(Guid id);

        void UpdeteExistingProduct(Product p);
    }
}
