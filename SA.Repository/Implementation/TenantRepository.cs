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

        public  Tenant GetTenant(Guid? id)
        {

            return this.context.Tenants.SingleOrDefault(s => s.Id == id);
            
        }

       public void Insert(Tenant tenant)
        {
            if (tenant == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Tenants.Add(tenant);
            context.SaveChanges();

        }



    }
}
