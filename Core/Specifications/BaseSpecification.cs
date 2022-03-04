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
        public Expression<Func<T, bool>> Citeria { get; set; }


        //empty list
        public List<Expression<Func<T, object>>> Include { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagedEnable { get; set; }

        //add include function
        protected void AddInclude(Expression<Func<T,object>> includeExpression) {
            Include.Add(includeExpression);
        
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) {
            OrderBy = orderByExpression;
          
        
        }

        protected void AddOrderByDesc(Expression<Func<T, object>> orderByExpressionDesc)
        {
            OrderByDesc = orderByExpressionDesc;


        }

        //paging func
        protected void AddPaging(int skip, int take) {
            Skip = skip;
            Take = take;
            IsPagedEnable = true;
        }
    }
}
