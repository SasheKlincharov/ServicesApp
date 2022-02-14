using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Service.Interface
{
    public interface IUserService
    {
        Task<List<SelectListItem>> GetAllUsers();
    }
}
