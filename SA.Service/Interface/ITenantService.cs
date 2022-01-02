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
        public Task<List<Tenant>> GetAllTenants();
    }
}
