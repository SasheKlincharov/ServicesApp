using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;

        public TenantService(ITenantRepository tenantRepository,
            IProductRepository productRepository, IUserRepository userRepository)
        {
            this.tenantRepository = tenantRepository;
            this.productRepository = productRepository;
            this.userRepository = userRepository;
        }

        public async Task<List<Tenant>> GetAllTenants()
        {
            return await this.tenantRepository.GetAllTenants();
        }

        public async Task<Tenant> GetTenant(Guid? id)
        {
            if (id == null)
                return null;

            var tenant = await tenantRepository.GetTenant(id.Value);
            return await Task.FromResult(tenant);
        }

        public async Task<Tenant> CreateNewTenant(TenantDto tenant)
        {
            if (string.IsNullOrEmpty(tenant.Name))
                return null;

            Tenant newTenant = new Tenant()
            {
                Name = tenant.Name,
                Address = tenant.Address,
                Color = tenant.Color,
                Latitude = tenant.Latitude,
                Longitude = tenant.Longitude,
                Description = tenant.Description,
                Email = tenant.Email,
                LogoURL = tenant.LogoURL,
                PhoneNumber = tenant.PhoneNumber,
                OwnerId = tenant.OwnerId,
                FacebookLink = tenant.FacebookLink,
                InstagramLink = tenant.InstagramLink,
                StartingHour = tenant.StartingHour,
                EndHour = tenant.EndHour,
                CategoryId = Guid.Parse(tenant.CategoryId),
                ScheduleTime = tenant.ScheduleTime,
                Schedules = new List<Schedule>()
            };

            var result = await this.tenantRepository.Insert(newTenant);

            return await Task.FromResult(result);
        }

        public async void DeleteTenant(Guid id)
        {
            var tenant = await tenantRepository.GetTenant(id);
            this.tenantRepository.Delete(tenant);
        }

        public void UpdeteExistingTenant(Tenant p)
        {
            this.tenantRepository.Update(p);

        }


        public async Task<List<SelectListItem>> GetAllCategories()
        {
            var allCategories = await this.tenantRepository.GetAllCategories();

            var result = new List<SelectListItem>();

            foreach (var cat in allCategories)
            {
                result.Add(new SelectListItem() { Text = cat.Name, Value = cat.Id.ToString() });
            }

            return await Task.FromResult(result);
        }

        public async Task<List<Product>> GetAllProductsForTenantCategory(Guid tenandId)
        {
            var tenantCategory = await this.tenantRepository.GetTenantCategoryByTenantId(tenandId);

            if (tenantCategory == null)
            {
                return null;
            }

            var allProductsForCategory = await this.productRepository.GetAllProductsForCategory(tenantCategory);

            return allProductsForCategory;
        }

        public async Task<bool> AddProductToTenant(AddProductToTenant addProductToTenant)
        {
            if (string.IsNullOrEmpty(addProductToTenant.TenantId) || string.IsNullOrEmpty(addProductToTenant.ProductId))
                return await Task.FromResult(false);

            var allProductsForTenant = await this.tenantRepository.GetAllProductsForTenant(addProductToTenant.TenantId);

            if (allProductsForTenant.Any(x => x.Id.ToString() == addProductToTenant.ProductId))
                return await Task.FromResult(false);

            var res = await this.tenantRepository.AddProductToTenant(addProductToTenant.TenantId, addProductToTenant.ProductId);

            return res;
        }

        public async Task<bool> Schedule(ScheduleDto schedule)
        {

            var users = await this.userRepository.GetAllUsers();

            var user = users.Where(x => x.UserName == schedule.UserId).FirstOrDefault();

            var tenant = await this.tenantRepository.GetTenant(Guid.Parse(schedule.TenantId));

            if (user == null || tenant == null)
                return await Task.FromResult(false);

            var result = await this.tenantRepository.CreateSchedule(tenant, user, schedule);


            return await Task.FromResult(result);

        }

        public List<Schedule> GetAllSchedulesForDate(Guid tenantId, DateTime date)
        {
            return this.tenantRepository.GetAllSchedulesForDate(tenantId, date);
        }
    }
}