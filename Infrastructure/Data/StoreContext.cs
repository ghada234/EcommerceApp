using Core.Entities;
using Core.Entities.OrderAggregatte;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }



        ///oveeride onmodelcreating that called when build migrations
        protected  override void OnModelCreating(ModelBuilder modelbuilder)
        {
            //base is the class that we derved form dbcontext class
            base.OnModelCreating(modelbuilder);
            modelbuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            // we need to loopthroughtentity ang get prop in this entitiesthat is decimal and loopthrougth this prop and convert it to double
            //because sqlite can't recognize decimal 
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite") {

                foreach (var entitytype in modelbuilder.Model.GetEntityTypes())
                {
                    var properties = entitytype.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    var timeProperties = entitytype.ClrType.GetProperties().Where(tp => tp.PropertyType == typeof(DateTimeOffset));


                    foreach (var prop in properties)
                    {
                        modelbuilder.Entity(entitytype.Name).Property(prop.Name).HasConversion<double>();
                    }

                    foreach (var prop in timeProperties) {
                        modelbuilder.Entity(entitytype.Name).Property(prop.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
                    }


                }
            
            }


        }
    }
}
