﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Name).IsRequired();
            builder.Property(p=> p.Description).IsRequired();
            builder.Property(p=> p.PictureUrl).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            // Relation One-to-Many
            builder.HasOne(P => P.Brand).WithMany().HasForeignKey(P => P.BrandId);
            builder.HasOne(P => P.Category).WithMany().HasForeignKey(P => P.CategoryId);


        }
    }
}
