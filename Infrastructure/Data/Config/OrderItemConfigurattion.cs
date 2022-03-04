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
    public class OrderItemConfigurattion : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            //oredrittem has own productitemordered  entity
            builder.OwnsOne(x => x.ItemOrdered, a => { a.WithOwner(); });
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        }
    }
}
