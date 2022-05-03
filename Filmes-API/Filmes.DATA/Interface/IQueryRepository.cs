using Filmes.DOMAIN.Interface;
using Filmes.DOMAIN.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Filmes.DATA.Interface
{
    public interface IQueryRepository<TEntity> where TEntity : class,IEntity
    {
        Task<TEntity> GetByAsync(long id);
        Task<TEntity> GetByIdAsync(long id, string include);
        Task<TEntity> GetByIdTrackingAsync(long id);
        Task<TEntity> GetByIdTrackingAsync(long id, string include);
        Task<TEntity> GetByIdNoIncludeAsync(long id);
        Task<TEntity> GetByIdTrackingNoFilterAsync(long id);
        IQueryable<TEntity> GetAll(string include = "");
        IQueryable<TEntity> GetAllNoInclude();
        SessionAppModel SessionApp { get; }

        Task<TEntity> GetByIdAsync(long id);
    }
}
