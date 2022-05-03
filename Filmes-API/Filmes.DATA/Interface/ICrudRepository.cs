using Filmes.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.DATA.Interface
{
    public interface ICrudRepository<TEntity>: IQueryRepository<TEntity> where TEntity : class, IEntity
    {
        Task Insert(TEntity entity);
        void Update(TEntity entity);
        Task Remove(long id);
        void Remove(TEntity entity);
        void Detached(TEntity entity);
        Task SaveChangesAsync();
    }
}
