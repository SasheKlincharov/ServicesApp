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

    }
}
