using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skyNetApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
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
        public async Task<ActionResult<IReadOnlyList<ProductsToReturnDto>>> GetProducts()
        {

            //var products = await _productRepo.GetAllListAsync();
            //return Ok(products);
            var spec = new ProductWithTypeAndBrandSpec();

            var Products = await _productRepo.GetListWithSpecification(spec);

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
            var productToReturn = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductsToReturnDto>>(Products);
            return Ok(productToReturn);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsToReturnDto>> GetProduct(int id)
        {
            //var product = await _conext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var spec = new ProductWithTypeAndBrandSpec(id);
            var product = await _productRepo.GetEntityWithSpecification(spec);
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
