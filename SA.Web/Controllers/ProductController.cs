using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SA.Data.Dto;
using SA.Data.Entity;
using SA.Service.Implementation;
using SA.Service.Interface;

namespace SA.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService ProductService;
        private readonly IUserService userService;
        private readonly ITenantService TenantService;

        public ProductController(IProductService ProductService,
            IUserService userService, ITenantService tenantService)
        {
            this.ProductService = ProductService;
            this.userService = userService;
            this.TenantService = tenantService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = new List<Product>();
            model = await this.ProductService.GetAllProducts();

            return View(model);
        }

        // GET: Product/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new CreateProductDto()
            {
                Product = new ProductDto(),
                Categories = new List<SelectListItem>()
            };

            var allCategories = await this.TenantService.GetAllCategories();

            model.Categories = allCategories;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(ProductDto Product)
        {
            if (ModelState.IsValid)
            {
                var result = this.ProductService.CreateNewProduct(Product);

                if (result == null)
                    return BadRequest("Error!");

                return RedirectToAction(nameof(Index));
            }
            return View(Product);
        }
        /*
                [HttpGet(Name = "GetDetails")]
                public async Task<IActionResult> GetDetails([FromQuery] Guid? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var Product = await ProductService.GetProduct(id);

                    if (Product == null)
                    {
                        return NotFound();
                    }

                    return View(Product);
                }
        */
        [HttpGet(Name = "Delete")]
        public IActionResult Delete([FromQuery] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductService.DeleteProduct(id.Value);

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

            var Product = this.ProductService.GetProduct(id).Result;

            if (Product == null)
            {
                return NotFound();
            }


            var ProductDto = new EditProductDto()
            {
                Product = Product,
                Categories = new List<SelectListItem>()
            };

            var allUsers = await this.userService.GetAllUsers();
            var allCategories = await this.TenantService.GetAllCategories();

            ProductDto.Categories = allCategories;


            return View(ProductDto);
        }

        [HttpPost(Name = "Edit")]
        public async Task<IActionResult> Edit(Product Product)
        {

            this.ProductService.UpdeteExistingProduct(Product);

            return RedirectToAction(nameof(Index));

        }
    }


}
