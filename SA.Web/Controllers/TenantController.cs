using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SA.Data.Dto;
using SA.Data.Entity;
using SA.Service.Implementation;
using SA.Service.Interface;

namespace SA.Web.Controllers
{
    public class TenantController : Controller
    {
        private readonly ITenantService tenantService;
        private readonly IUserService userService;

        public TenantController(ITenantService tenantService, 
            IUserService userService)
        {
            this.tenantService = tenantService;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = new List<Tenant>();
            model = await this.tenantService.GetAllTenants();

            return View(model);
        }

        // GET: Tenant/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new CreateTenantDto()
            {
                Tenant = new TenantDto(),
                Users = new List<SelectListItem>(),
                Categories = new List<SelectListItem>()
            };

            var allUsers = await this.userService.GetAllUsers();
            var allCategories = await this.tenantService.GetAllCategories();

            model.Users = allUsers;
            model.Categories = allCategories;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(TenantDto tenant)
        {
            if (ModelState.IsValid)
            {
                var result = this.tenantService.CreateNewTenant(tenant);

                if (result == null)
                    return BadRequest("Error!");

                return RedirectToAction(nameof(Index));
            }
            return View(tenant);
        }

        [HttpGet(Name ="GetDetails")]
        public async Task<IActionResult> GetDetails([FromQuery] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var  tenant = await tenantService.GetTenant(id);

            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        [HttpGet(Name = "Delete")]
        public IActionResult Delete([FromQuery] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            tenantService.DeleteTenant(id.Value);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet(Name = "AddProductToTenant")]
        public async Task<IActionResult> AddProductToTenant([FromQuery] Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var allProducts = await this.tenantService.GetAllProductsForTenantCategory(id.Value);

            var allProductsList = allProducts.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();

            if (allProducts == null || !allProducts.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var productToTenantmodel = new AddProductToTenant()
            {
                TenantId = id.Value.ToString(),
                ProductId = "",
                AllProducts = allProductsList
            };

            return View(productToTenantmodel);
        }

        [HttpPost(Name ="AddProductToTenant")]
        public async Task<IActionResult> AddProductToTenant(AddProductToTenant addProductToTenant)
        {
            if (addProductToTenant == null)
                return null;

           var res = await tenantService.AddProductToTenant(addProductToTenant);

            if (!res)
                return NotFound();


            //return to tenant details
            return RedirectToAction(nameof(Index));
        }
    }


}
