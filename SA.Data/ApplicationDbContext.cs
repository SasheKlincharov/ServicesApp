using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SA.Data.Entity;

namespace SA.Data
{
    public class ApplicationDbContext : IdentityDbContext<SAUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<SAUser> Users { get; set; }
        public virtual DbSet<ProductInTenant> ProductInTenant { get; set; } 
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Tenant>()
                .Property(k => k.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Schedule>()
                .Property(k => k.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Tenant>()
                .HasOne(k => k.Owner)
                .WithOne(k => k.TenantOwner)
                .HasForeignKey<Tenant>(k => k.OwnerId);

            builder.Entity<Schedule>()
                .HasOne(k => k.User)
                .WithMany(k => k.Schedules)
                .HasForeignKey(k => k.UserId);

            builder.Entity<Schedule>()
                .HasOne(k => k.Tenant)
                .WithMany(k => k.Schedules)
                .HasForeignKey(k => k.TenantId);

            builder.Entity<ProductInTenant>()
                         .HasOne(x => x.Product)
                         .WithMany(x => x.ProductsInTenant)
                         .HasForeignKey(x => x.ProductId);

            builder.Entity<ProductInTenant>()
                .HasOne(x => x.Tenant)
                .WithMany(x => x.ProductsInTenant)
                .HasForeignKey(x => x.TenantId);
        }
    }
}