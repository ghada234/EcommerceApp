using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skyNetApp.Dto;
using skyNetApp.Errors;
using skyNetApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Controllers
{

    
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public ProductController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductType>productTypeRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IMapper mapper
            )
        {
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductsToReturnDto>>> GetProducts([FromQuery]ProductsParams productsparams )
        {

            //var products = await _productRepo.GetAllListAsync();
            //return Ok(products);
            var spec = new ProductWithTypeAndBrandSpec(productsparams);
            var specCount = new ProductWithFilterToCountSpec(productsparams);

            var Products = await _productRepo.GetListWithSpecification(spec);
            var ProductsCount = await _productRepo.CountAsync(specCount);

            //select loop throught list and change it to new shape
            //return Products.Select(product => new ProductsToReturnDto {
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    ProductBrand = product.ProductBrand.Name,
            //    ProductType = product.ProductType.Name,
            //    Price = product.Price,
            //    PicureUrl = product.PicureUrl,

            //}).ToList();
            var productData = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductsToReturnDto>>(Products);
            return Ok(new Pagination<ProductsToReturnDto>(productsparams.PageIndex,productsparams.PageSize,ProductsCount,productData));

        }


        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        //to get 404 response  with type apiresponse
        
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductsToReturnDto>> GetProduct(int id)
        {
            //var product = await _conext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var spec = new ProductWithTypeAndBrandSpec(id);
            var product = await _productRepo.GetEntityWithSpecification(spec);
            //we till swagger if productnot found return 404 error with  api response
            if (product == null) {
                return NotFound(new ApiResponse(404));
            }
            //var producttoreturn = new ProductsToReturnDto {
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    ProductBrand = product.ProductBrand.Name,
            //    ProductType=product.ProductType.Name,
            //    Price=product.Price,
            //    PicureUrl=product.PicureUrl,


            //};
            //_mapper.map<src,dest>(src)
            var productToReturn = _mapper.Map<Product, ProductsToReturnDto>(product);
            return Ok(productToReturn);

        }
        //
        //get brands
        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getProductBrands()
        {

            return Ok(await _productBrandRepo.GetAllListAsync());


        }


        //
        //get types

        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> getProductTypes()
        {

            return Ok(await _productTypeRepo.GetAllListAsync());


        }
    }
}
