using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
   public interface IUnitOfWork:IDisposable
    {
        //generate method repository that return igenericrepository<TEntity>
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        //save change in database and return numbers of changes
        Task<int> Complete();

    }
}
