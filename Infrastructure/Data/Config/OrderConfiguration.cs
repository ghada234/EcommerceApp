using Core.Entities.OrderAggregatte;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        //builder has address as own entity
        {
            builder.OwnsOne(x => x.ShipToAddress, a => { a.WithOwner(); });
            builder.Property(x => x.Status)
                .HasConversion(x => x.ToString()
                , x => (OrderStatus)Enum.Parse(typeof(OrderStatus), x));

            //one to many relation bettween order and order items on delete oredre oredre item will be deletted to
            builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
