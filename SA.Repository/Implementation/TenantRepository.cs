using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SA.Data;
using SA.Data.Dto;
using SA.Data.Entity;
using SA.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Repository.Implementation
{
    public class TenantRepository : ITenantRepository
    {
        private readonly ApplicationDbContext context;

        public TenantRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Delete(Tenant tenant)
        {
            if (tenant == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Tenants.Remove(tenant);
            context.SaveChanges();

        }

        public async Task<List<Tenant>> GetAllTenants()
        {
            return await this.context.Tenants
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Tenant> GetTenant(Guid id)
        {
            var result = this.context.Tenants
                .Where(x => x.Id == id)
                .Include(x => x.ProductsInTenant)
                .ThenInclude(x => x.Product)
                .FirstOrDefault();

            return await Task.FromResult(result);
        }

        public async Task<Tenant> Insert(Tenant tenant)
        {
            context.Tenants.Add(tenant);
            context.SaveChanges();

            return await Task.FromResult(tenant);
        }

        public void Update(Tenant tenant)
        {
            if (tenant == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Tenants.Update(tenant);
            context.SaveChanges();
        }
        public async Task<List<Category>> GetAllCategories()
        {
            var users = new List<Category>();
            users = context.Categories.ToList();

            return await Task.FromResult(users);
        }

        public async Task<Guid> GetTenantCategoryByTenantId(Guid tenantId)
        {
            var res = context.Tenants.Where(x => x.Id == tenantId).FirstOrDefault();

            if (res == null)
                return await Task.FromResult(Guid.Empty);

            return await Task.FromResult(res.CategoryId);
        }

        public Task<bool> AddProductToTenant(string TenantId, string ProductId)
        {
            var tenant = context.Tenants.Where(x => x.Id.ToString() == TenantId).Include(x => x.ProductsInTenant).FirstOrDefault();
            var product = context.Products.Where(x => x.Id.ToString() == ProductId).Include(x => x.ProductsInTenant).FirstOrDefault();

            if (tenant == null || product == null)
                return Task.FromResult(false);

            ProductInTenant newProduct = new ProductInTenant()
            {
                ProductId = Guid.Parse(ProductId),
                TenantId = Guid.Parse(TenantId)
            };

            context.ProductInTenant.Add(newProduct);

            product.ProductsInTenant.Add(newProduct);
            tenant.ProductsInTenant.Add(newProduct);

            context.SaveChanges();

            return Task.FromResult(true);
        }

        public async Task<List<Product>> GetAllProductsForTenant(string TenantId)
        {
            return await context.ProductInTenant.Where(x => x.TenantId.ToString() == TenantId).Select(z => z.Product).ToListAsync();
        }

        public async Task<bool> CreateSchedule(Tenant tenant, SAUser user, ScheduleDto schedule)
        {
            Schedule newSchedule = new Schedule()
            {
                From = DateTime.Parse(schedule.From),
                To = DateTime.Parse(schedule.To),
                IsScheduled = true,
                TenantId = tenant.Id,
                UserId = user.Id
            };

            tenant.Schedules.Add(newSchedule);

            context.SaveChanges();

            return await Task.FromResult(true);


        }

        public List<Schedule> GetAllSchedulesForDate(Guid tenantId, DateTime date)
        {
            return context.Schedules.Where(x => x.TenantId == tenantId && x.From.Date == date.Date).Include(x => x.User).ToList();

        }
    }
}
