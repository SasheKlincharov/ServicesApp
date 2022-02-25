using Microsoft.AspNetCore.Mvc.Rendering;
using SA.Data.Dto;
using SA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Service.Interface
{
    public interface ITenantService
    {
        Task<List<Tenant>> GetAllTenants();

        Task<Tenant> GetTenant(Guid? id);

        Task<Tenant> CreateNewTenant(TenantDto tenant);

        void DeleteTenant(Guid id);

        void UpdeteExistingTenant(Tenant p);
        Task<List<SelectListItem>> GetAllCategories();
        Task<List<Product>> GetAllProductsForTenantCategory(Guid tenandId);
        Task<bool> AddProductToTenant(AddProductToTenant addProductToTenant);

        Task<bool> Schedule(ScheduleDto schedule);
        List<Schedule> GetAllSchedulesForDate(Guid tenantId, DateTime date);

    }
}
