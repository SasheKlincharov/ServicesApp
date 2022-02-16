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
    }
}
