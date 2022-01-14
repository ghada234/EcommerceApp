using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
   public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification()
        {
                                                            
        }
        public BaseSpecification(Expression<Func<T,bool>> citeria)
        {
            Citeria = citeria;
        }
        public Expression<Func<T, bool>> Citeria { get; }


        //empty list
        public List<Expression<Func<T, object>>> Include { get; } = new List<Expression<Func<T, object>>>();

        //add include function
        protected void AddInclude(Expression<Func<T,object>> includeExpression) {
            Include.Add(includeExpression);
        
        }
    }
}
