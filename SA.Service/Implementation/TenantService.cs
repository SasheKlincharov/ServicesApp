using SA.Data.Dto;
using SA.Data.Entity;
using SA.Repository.Implementation;
using SA.Repository.Interface;
using SA.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Service.Implementation
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository tenantRepository;

        public TenantService(ITenantRepository tenantRepository)
        {
            this.tenantRepository = tenantRepository;
        }

        public async Task<List<Tenant>> GetAllTenants()
        {
            return await this.tenantRepository.GetAllTenants();
        }

        public Tenant GetTenant(Guid? id)
        {
            var tenant = tenantRepository.GetTenant(id);
            return tenant;           
        }

        public void CreateNewProduct(Tenant tenant)
        {
            this.tenantRepository.Insert(tenant);
        }
    }
}