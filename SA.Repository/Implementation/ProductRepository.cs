using Microsoft.EntityFrameworkCore;
using SA.Data;
using SA.Data.Entity;
using SA.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Product>> GetAllProductsForCategory(Guid categoryId)
        {
            return await context.Products.Where(x => x.CategoryId == categoryId)
                .Include(x => x.Category).ToListAsync();
        }

        public void Delete(Product Product)
        {
            if (Product == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Products.Remove(Product);
            context.SaveChanges();

        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await this.context.Products
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var result = this.context.Products
                .Where(x => x.Id == id)
                .Include(x => x.Category)
                .FirstOrDefault();

            return await Task.FromResult(result);
        }

        public async Task<Product> Insert(Product Product)
        {
            context.Products.Add(Product);
            context.SaveChanges();

            return await Task.FromResult(Product);
        }

        public void Update(Product Product)
        {
            if (Product == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Products.Update(Product);
            context.SaveChanges();
        }
    }
}
