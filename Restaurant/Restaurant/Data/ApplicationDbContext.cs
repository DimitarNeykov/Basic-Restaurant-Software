namespace Restaurant.Data
{
    using System;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Restaurant.DataModels;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<DishesType> DishesTypes { get; set; }

        public DbSet<DishOrder> DishOrders { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<RestaurantInfo> Restaurants { get; set; }

        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MakeRole(modelBuilder, "Administrator", "ADMINISTRATOR");
            MakeRole(modelBuilder, "Manager", "MANAGER");
            MakeRole(modelBuilder, "Waiter", "WAITER");

        }

        private void MakeRole(ModelBuilder modelBuilder, string name, string normalizedName)
        {
            string roleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = name,
                NormalizedName = normalizedName,
                Id = roleId,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            });

            if (name == "Administrator")
            {
                string adminId = Guid.NewGuid().ToString();

                var hasher = new PasswordHasher<IdentityUser>();
                modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser()
                {
                    Id = adminId,
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@restaurant.bg",
                    NormalizedEmail = "ADMIN@RESTAURANT.BG",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123456!Admin"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                });

                modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                {
                    RoleId = roleId,
                    UserId = adminId,
                });
            }
        }
    }
}
