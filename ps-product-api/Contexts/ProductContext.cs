using Microsoft.EntityFrameworkCore;
using ps_product_api.Entities;

namespace ps_product_api.Contexts
{
    public class ProductContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        // https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
        public ProductContext(DbContextOptions<ProductContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Email = "psherchan1@gmail.com",
                    FirstName = "Pankaj",
                    LastName = "One"
                },
                 new User()
                 {
                     Id = 2,
                     Email = "psherchan2@gmail.com",
                     FirstName = "Pankaj",
                     LastName = "Two"
                 },
                 new User()
                 {
                     Id = 3,
                     Email = "psherchan3@gmail.com",
                     FirstName = "Pankaj",
                     LastName = "Three"
                 }
            );
        }
    }
}