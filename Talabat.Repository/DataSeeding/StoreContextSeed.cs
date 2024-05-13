using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Module;
using Talabat.Repository.Data;

namespace Talabat.Repository.DataSeeding
{
    public static class StoreContextSeed
    {
        public static async  Task SeedAsync(StoreDbContext _dbContext)
        {
            // Add Data Once (Brand Data)
            if (_dbContext.Brands.Count() == 0)
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/DataSeeding/brands.json");
                //***Serialize ⇒ Object to Json*** 
                //***DeSerialize ⇒ Json to Object***
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                // if there is no brands 
                if (brands?.Count() > 0)
                {
                    foreach (var brand in brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            // Add Data Once (Category Data)
            if (_dbContext.Categories.Count() == 0)
            {
                var CategoriesData = File.ReadAllText("../Talabat.Repository/DataSeeding/categories.json");
                //***Serialize ⇒ Object to Json*** 
                //***DeSerialize ⇒ Json to Object***
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

                // if there is no brands 
                if (Categories?.Count() > 0)
                {
                    foreach (var category in Categories)
                    {
                        _dbContext.Set<ProductCategory>().Add(category);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            // Add Data Once (Produts Data)
            if (_dbContext.Products.Count() == 0)
            {
                var ProductsData = File.ReadAllText("../Talabat.Repository/DataSeeding/products.json");
                //***Serialize ⇒ Object to Json*** 
                //***DeSerialize ⇒ Json to Object***
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                // if there is no brands 
                if (Products?.Count() > 0)
                {
                    foreach (var product in Products)
                    {
                        _dbContext.Set<Product>().Add(product);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            // Add Data Once (DeliveryMethod Data)
            if (_dbContext.DeliveryMethods.Count() == 0)
            {
                var DeliveryMethodData = File.ReadAllText("../Talabat.Repository/DataSeeding/delivery.json");
                //***Serialize ⇒ Object to Json*** 
                //***DeSerialize ⇒ Json to Object***
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);

                // if there is no brands 
                if (DeliveryMethods?.Count() > 0)
                {
                    foreach (var item in DeliveryMethods)
                    {
                        _dbContext.Set<DeliveryMethod>().Add(item);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }   
        }

    }
}
