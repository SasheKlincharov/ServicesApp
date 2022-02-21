using SA.Data.Dto;
using SA.Data.Entity;
using SA.Repository.Interface;
using SA.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Service.Implementation
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository ProductRepository;

        public ProductService(
            IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await this.ProductRepository.GetAllProducts();
        }

        public async Task<Product> GetProduct(Guid? id)
        {
            if (id == null)
                return null;

            var Product = await ProductRepository.GetProduct(id.Value);
            return await Task.FromResult(Product);
        }

        public async Task<Product> CreateNewProduct(ProductDto Product)
        {
            if (string.IsNullOrEmpty(Product.Name))
                return null;

            Product newProduct = new Product()
            {
                Name = Product.Name,
                CategoryId = Product.CategoryId,
                Price = Product.Price,
                Image = Product.Image
            };

            var result = await this.ProductRepository.Insert(newProduct);

            return await Task.FromResult(result);
        }

        public async void DeleteProduct(Guid id)
        {
            var Product = await ProductRepository.GetProduct(id);
            this.ProductRepository.Delete(Product);
        }

        public void UpdeteExistingProduct(Product p)
        {
            this.ProductRepository.Update(p);

        }

    }
}
