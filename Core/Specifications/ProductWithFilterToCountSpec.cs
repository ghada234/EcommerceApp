using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
  public  class ProductWithFilterToCountSpec: BaseSpecification<Product>
    {
        public ProductWithFilterToCountSpec(ProductsParams productparams) : base(

                  //if brandid has value execute what next || 
                  // case brandid has value ==> x=>x.ProductBrandId==brandId
                  // case typeid has value ==> x=>x.productTypeIdId==typeId
                  //both brandid and typesid has value  Where(thingId == thing.id && thingName == thing.name) 

                  x =>
                   (string.IsNullOrEmpty(productparams.Search) || x.Name.ToLower().Contains(productparams.Search)) &&
                  (!productparams.BrandId.HasValue || x.ProductBrandId == productparams.BrandId) &&
                  (!productparams.TypeId.HasValue || x.ProductTypeId == productparams.TypeId)

            )
        {
                    
        }
    }
}
