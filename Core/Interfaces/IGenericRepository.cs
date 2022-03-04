using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
   public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllListAsync();


        //using specificaion in generic repository
        Task<T> GetEntityWithSpecification(ISpecification<T> spec);

        Task<IReadOnlyList<T>> GetListWithSpecification(ISpecification<T> spec);

        //get the count of data before paginations

        Task<int> CountAsync(ISpecification<T> spec);

        //add delete update
        void Add(T entity);
        void update(T entity);
        void Delete (T entity);


    }
}
