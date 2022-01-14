using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    class SpecificationEvaulaor<TEntity> where TEntity : BaseEntity
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inpurQuery, ISpecification<TEntity> spec)
        {
            var query = inpurQuery;
            if (spec.Citeria != null) {
                query = query.Where(spec.Citeria); //where(i=>i.id==id)
            
            }
            //currnrtt is current entity and include is include expression
            //curren ==>product
            //include ==>list of include and we loop it
            //and the resultt ==>product.include(x=>x.productype)
                               //==>productt.include(x=>x.productbrand)


            query = spec.Include.Aggregate(query, (current, include) =>  current.Include(include) );

            return query;
        }

       
    }
}
