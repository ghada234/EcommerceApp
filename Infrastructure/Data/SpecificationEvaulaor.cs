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

        //this class is responsible ofcreate the complete query i send it _context.set<> and it complete .where.include and so on
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inpurQuery, ISpecification<TEntity> spec)
        {
            var query = inpurQuery;
            if (spec.Citeria != null) {
                query = query.Where(spec.Citeria); //where(i=>i.id==id)
            
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy); //.orderBy(x=>x.Name)

            }

            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc); //where(i=>i.id==id)

                //pagination query must execute after filtring and ordering ad itis the last stage
            }
            if (spec.IsPagedEnable) {
                query = query.Skip(spec.Skip).Take(spec.Take);
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
