using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{


    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository()
        {
                                
        }
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
   


        //method get  the count of data
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

       

        public async Task<IReadOnlyList<T>> GetAllListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpecification(ISpecification<T> spec)
        {

            //iQurable.firstordefaultasync
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetListWithSpecification(ISpecification<T> spec)
        {
            //iQurable.tolistasync()
            return await ApplySpecification(spec).ToListAsync();
        }

     
        //apply specification method
        private IQueryable<T> ApplySpecification(ISpecification<T> spec) {
          return  SpecificationEvaulaor<T>.GetQuery(_context.Set<T>().AsQueryable(),spec);
        
        }


        //we don't use asyync method because we say we want to delete that so track it we don't save it to database now
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        //we don't use asyync method because we say we want to updatte that so track it
        public void update(T entity)

        {
            //attach this entity to be changed

            _context.Set<T>().Attach(entity);
            //get the microsoft change tracking for the given entry
            _context.Entry(entity).State = EntityState.Modified;
        }
        //we don't use asyync method because we say we want to add that so track it
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
    }
}
