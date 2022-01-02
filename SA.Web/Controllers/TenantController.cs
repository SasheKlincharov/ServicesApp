using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SA.Data.Dto;
using SA.Data.Entity;
using SA.Service.Implementation;
using SA.Service.Interface;

namespace SA.Web.Controllers
{
    public class TenantController : Controller
    {
        private readonly ITenantService tenantService;

        public TenantController(ITenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = new List<Tenant>();
            model = await this.tenantService.GetAllTenants();

            return View(model);
        }
    }
}
