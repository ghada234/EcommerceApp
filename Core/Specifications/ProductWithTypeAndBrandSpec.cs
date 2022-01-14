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

        public ProductWithTypeAndBrandSpec()
        {

            //addinclude method in basespecification class
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }

        //base is basespecification constructor
        public ProductWithTypeAndBrandSpec(int id) : base(x=>x.Id==id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}
