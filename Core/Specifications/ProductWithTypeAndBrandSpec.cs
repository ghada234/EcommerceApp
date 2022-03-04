using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpec : BaseSpecification<Product>
    {

        public ProductWithTypeAndBrandSpec(ProductsParams productParams) 
            : base(

                  //if brandid has value execute what next || 
                  // case brandid has value ==> x=>x.ProductBrandId==brandId
                  // case typeid has value ==> x=>x.productTypeIdId==typeId
                  //both brandid and typesid has value  Where(thingId == thing.id && thingName == thing.name) 

                  x => 
                  
                  //search
                  (string.IsNullOrEmpty(productParams.Search)||x.Name.ToLower().Contains(productParams.Search))&&
                  (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
                  (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)

            )


        {

            //addinclude method in basespecification class
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            AddOrderBy(X => X.Name);
            if (!String.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {

                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;

                }
            }
            AddPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

        }

        //base is basespecification constructor
        public ProductWithTypeAndBrandSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}
