using SA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data
{
    public static class InitData
    {
        public static void Seed(this ApplicationDbContext dbContext)
        {

            //seed tenant categories
            var tenantCategories = dbContext.Categories.ToList();

            if (!tenantCategories.Any(x => x.Name == "Barber"))
                dbContext.Categories.Add(new Category() { Name = "Barber" });

            if (!tenantCategories.Any(x => x.Name == "Massage"))
                dbContext.Categories.Add(new Category() { Name = "Massage" });

            if (!tenantCategories.Any(x => x.Name == "Make up artist"))
                dbContext.Categories.Add(new Category() { Name = "Make up artist" });

            dbContext.SaveChanges();
        }
    }
}
