using GoodHamburguer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburguer.Data.Map
{
    public class OrderMap : IEntityTypeConfiguration<OrderModel>
    {
        public void Configure(EntityTypeBuilder<OrderModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductIds).IsRequired();
            builder.Property(x => x.UserId);
            builder.Property(x => x.Total).HasPrecision(18, 4);

            builder.HasOne(x => x.User);

            builder.HasMany(x => x.Products);


        }
    }
}
