﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {

        Task<Product> GetProductByIdAsync(int id);


        //user readonly list to be specific of what we want
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    }
}