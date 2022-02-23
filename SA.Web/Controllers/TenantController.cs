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

        [HttpGet(Name = "GetDetails")]
        public async Task<IActionResult> GetDetails([FromQuery] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await tenantService.GetTenant(id);

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

        [HttpPost(Name = "AddProductToTenant")]
        public async Task<IActionResult> AddProductToTenant(AddProductToTenant addProductToTenant)
        {
            if (addProductToTenant == null)
                return null;

            var res = await tenantService.AddProductToTenant(addProductToTenant);

            //return to tenant details
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        [HttpGet(Name = "Edit")]
        public async Task<IActionResult> Edit([FromQuery] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = this.tenantService.GetTenant(id).Result;

            if (tenant == null)
            {
                return NotFound();
            }


            var tenantDto = new EditTenantDto()
            {
                Tenant = tenant,
                Users = new List<SelectListItem>(),
                Categories = new List<SelectListItem>()
            };

            var allUsers = await this.userService.GetAllUsers();
            var allCategories = await this.tenantService.GetAllCategories();
            tenantDto.Users = allUsers;
            tenantDto.Categories = allCategories;


            return View(tenantDto);
        }

        [HttpPost(Name = "Edit")]
        public async Task<IActionResult> Edit(Tenant tenant)
        {

            this.tenantService.UpdeteExistingTenant(tenant);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet(Name = "Calendar")]
        public async Task<IActionResult> Calendar([FromQuery] Guid? tenantId)
        {
            if (tenantId == null)
                return RedirectToAction(nameof(Index));

            CalendarDto calendar = new CalendarDto();

            calendar.ForDay = DateTime.Now;
            calendar.TenantId = tenantId.Value;

            var tenant = await this.tenantService.GetTenant(tenantId);

            if (tenant == null)
                return RedirectToAction(nameof(Index));

            calendar.Tenant = tenant;

            List<Schedule> allSchedules = new List<Schedule>();

            var startDate = tenant.StartingHour;
            var endHour = tenant.EndHour;

            DateTime momStartHour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startDate.Hour, startDate.Minute, 0);

            DateTime momEndHour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startDate.Hour, startDate.Minute + tenant.ScheduleTime, 0);


            while (momStartHour.Hour < endHour.Hour)
            {
                var temp = 60 / tenant.ScheduleTime;
                while (temp > 0)
                {
                    var momSchedule = new Schedule()
                    {
                        From = momStartHour,
                        To = momEndHour,
                        TenantId = tenant.Id,
                        Tenant = tenant,
                        IsScheduled = false,
                        User = null
                    };

                    momStartHour = momEndHour;

                    momEndHour = momStartHour.AddMinutes(tenant.ScheduleTime);

                    temp--;

                    allSchedules.Add(momSchedule);
                }
            }

            calendar.Schedules = allSchedules;


            //zemi gi site zakazani termini od Schedules za denesnito den
            // ili odbranito den i filtriraj gi


            return View(calendar);
        }


        [HttpPost(Name = "Schedule")]
        public async Task<IActionResult> Schedule([FromBody] ScheduleDto input)
        {
            //ne stasue datata vo input pravilno


            var result = await this.tenantService.Schedule(input);

            if (result)
            {
                return RedirectToAction("Calendar", new { input.TenantId });
            }

            return RedirectToAction(nameof(Index));
        }
    }


}
