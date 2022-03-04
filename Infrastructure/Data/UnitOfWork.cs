using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;


        //save repositories in hashtable
        private Hashtable _repositories;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }


        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {

            //create new instance of hashtable if repositories==null
            if (_repositories == null) {
                _repositories = new Hashtable();
            }
            //if Tentity=productt so type=product

            var type = typeof(TEntity).Name;

            //create new instance of genericrepository<type> if inis't found in hashtable
            if (!_repositories.ContainsKey(type)) {
                
                //repositorytype=Nam= genericrepository
                var repositoryTpe = typeof(GenericRepository<>);

                //var repositoryinstance=new Genericrepository<product>;
       
                var repositoryInstance = Activator.CreateInstance(repositoryTpe.MakeGenericType(typeof(TEntity)),_context);
                _repositories.Add(type, repositoryInstance);
            }

            //convert object to type igenericrepository
            return (IGenericRepository<TEntity>)_repositories[type];



        }
    }
}
