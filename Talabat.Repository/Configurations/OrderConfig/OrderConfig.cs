using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Module;

namespace Talabat.Repository.Configurations.OrderConfig
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {   /*
         In Entity Framework Core (EF Core), the HasConversion method is used to configure how value
        types are mapped between your C# model and the underlying database provider. It allows you to
        define custom conversion logic for properties, enabling you to store data in a format that's different
        from the way it's represented in your application code.
         */
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.Status).HasConversion(OStatus => OStatus.ToString() // Convert enum to string , OStatus is a value enum

            , OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));// Convert string back to enum

            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");

            //Address [Not Table ] => Order + Address =>OneTable
            builder.OwnsOne(O => O.ShippingAddress, X => X.WithOwner());

            // Order - DeliveryMethod => one to many 
            //NoAction: إذا حاولت تعديل بيانات طالب مرتبط بأحد الاختبارات، ترفض قاعدة البيانات التعديل لحماية سلامة البيانات
            builder.HasOne(O=>O.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
