﻿using GoodHamburguer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburguer.Data.Map
{
    public class ProductMap : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Price).IsRequired().HasPrecision(18, 4);
            builder.Property(x => x.Type);

            builder.HasOne<OrderModel>().WithMany().HasForeignKey("OrderModelId");

        }
    }
}