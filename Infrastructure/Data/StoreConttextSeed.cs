using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
   public class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {

            try
            {
                if (!context.ProductBrands.Any())
                {
                    //the code executte in programs.cs in skynet project so we have to enter infrasttrucure project
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brandList = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brandList)
                    {
                        context.ProductBrands.Add(brand);
                    }
                    //save changes in database

                    await context.SaveChangesAsync();

                }

                //


                if (!context.ProductTypes.Any())
                {
                    //the code executte in programs.cs in skynet project so we have to enter infrasttrucure project
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var typeList = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var type in typeList)
                    {
                        context.ProductTypes.Add(type);
                    }
                    //save changes in database

                    await context.SaveChangesAsync();

                }

                //

                if (!context.Products.Any())
                {
                    //the code executte in programs.cs in skynet project so we have to enter infrasttrucure project
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var productList = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in productList)
                    {
                        context.Products.Add(product);
                    }
                    //save changes in database

                    await context.SaveChangesAsync();

                }



            }

            catch (Exception ex)
            {

                //create newilooger instance
                var ILogger = loggerFactory.CreateLogger<StoreContextSeed>();

                ILogger.LogError(ex.Message);

            }
        }
    }
}
