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

        // GET: Tenant/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,LogoURL,Description,Address,PhoneNumber")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                this.tenantService.CreateNewTenant(tenant);
                return RedirectToAction(nameof(Index));
            }
            return View(tenant);
        }

        [HttpGet]
        public IActionResult getDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = tenantService.GetTenant(id);

            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }


        // GET: Products/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = this.tenantService.GetTenant(id);

            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }


    }


}
