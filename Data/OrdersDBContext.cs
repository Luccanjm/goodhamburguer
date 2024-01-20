using GoodHamburguer.Data.Map;
using GoodHamburguer.Enums;
using GoodHamburguer.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.Data
{
    public class OrdersDBContext : DbContext
    {
        public OrdersDBContext(DbContextOptions<OrdersDBContext> options)

        : base(options)
        {
        }

        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        

            var product1 = new ProductModel { Id = Guid.NewGuid(), Title = "X Burguer", Price = 5.00m, Type = (ProductTypes)1};
            var product2 = new ProductModel { Id = Guid.NewGuid(), Title = "X Egg", Price = 4.5m, Type = (ProductTypes)1 };
            var product3 = new ProductModel { Id = Guid.NewGuid(), Title = "X Bacon", Price = 7.0m, Type = (ProductTypes)1 };
            var product4 = new ProductModel { Id = Guid.NewGuid(), Title = "Fries", Price = 2.0m, Type = (ProductTypes)3 };
            var product5 = new ProductModel { Id = Guid.NewGuid(), Title = "Soft Drink", Price = 2.5m, Type = (ProductTypes)2 };
            var product6 = new ProductModel { Id = Guid.NewGuid(), Title = "Food Packaging", Price = 1.0m, Type = (ProductTypes)4};


            var user1 = new UserModel { Id = Guid.NewGuid(), Name = "Lucca", Email = "lucca@example.com" };
            var user2 = new UserModel { Id = Guid.NewGuid(), Name = "Renata", Email = "renata@example.com" };

            var order1 = new OrderModel { Id = Guid.NewGuid(), ProductIds = [ product1.Id, product5.Id ], UserId = user1.Id, Total=7.5m };

            modelBuilder.Entity<ProductModel>()
                .HasData(product1, product2, product3, product4, product5, product6);

            modelBuilder.Entity<UserModel>()
                .HasData(user1, user2);

            modelBuilder.Entity<OrderModel>()
                .HasData(order1);


            modelBuilder.ApplyConfiguration(new OrderMap());
            base.OnModelCreating(modelBuilder);



        }

    }
}
