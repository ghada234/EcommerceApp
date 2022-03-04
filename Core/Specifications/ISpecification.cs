using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
   public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Citeria { get; }
       List<Expression<Func<T,object>>> Include { get; }

        Expression<Func<T,object>> OrderBy { get; }

        Expression<Func<T, object>> OrderByDesc { get; }


        //for pagination props
        int Skip { get; set; }
        int Take { get; set; }

        bool IsPagedEnable{ get; set; }


    }
}
