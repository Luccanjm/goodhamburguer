
using GoodHamburguer.Data;
using GoodHamburguer.Repositories;
using GoodHamburguer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<OrdersDBContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );

            builder.Services.AddScoped<IOrdersRepositorie, OrderRepositorie>();
            builder.Services.AddScoped<IUsersRepositorie, UserRepositorie>();
            builder.Services.AddScoped<IProductsRepositorie, ProductRepositorie>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

    }
}
