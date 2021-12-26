using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SA.Data.Entity;

namespace SA.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Tenant>()
                .HasOne(k => k.Owner)
                .WithOne(k => k.TenantOwner)
                .HasForeignKey<Tenant>(k => k.OwnerId);

            builder.Entity<Tenant>()
                .Property(k => k.Id)
                .ValueGeneratedOnAdd();


            builder.Entity<Schedule>()
                .Property(k => k.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<User>()
                .HasMany(k => k.Schedules)
                .WithOne(k => k.User);



            builder.Entity<Schedule>()
                .HasOne(k => k.User)
                .WithMany(k => k.Schedules)
                .HasForeignKey(k => k.UserId);

            builder.Entity<Tenant>()
                .HasMany(k => k.Schedules)
                .WithOne(k => k.Tenant);
                

            builder.Entity<Schedule>()
                .HasOne(k => k.Tenant)
                .WithMany(k => k.Schedules)
                .HasForeignKey(k => k.TenantId);






        }
    }
}