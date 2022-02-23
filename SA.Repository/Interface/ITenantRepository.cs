using SA.Data.Dto;
using SA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Repository.Interface
{
    public interface ITenantRepository
    {
        Task<List<Tenant>> GetAllTenants();
        Task<Tenant> GetTenant(Guid id);
        Task<Tenant> Insert(Tenant tenant);
        void Delete(Tenant tenant);
        void Update(Tenant p);

        Task<List<Category>> GetAllCategories();
        Task<Guid> GetTenantCategoryByTenantId(Guid tenantId);
        Task<bool> AddProductToTenant(string TenantId, string ProductId);
        Task<List<Product>> GetAllProductsForTenant(string TenantId);
        Task<bool> CreateSchedule(Tenant tenant, SAUser user, ScheduleDto schedule);
    }


}
