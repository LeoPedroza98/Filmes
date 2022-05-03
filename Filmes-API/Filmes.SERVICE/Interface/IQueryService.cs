using Filmes.DOMAIN.Interface;
using Filmes.DOMAIN.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Filmes.SERVICE.Interface
{
    public interface IQueryService<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> Get(long id);
        Task<TEntity> Get(long id, string include);
        Task<TEntity> GetTracking(long id);
        Task<TEntity> GetTracking(long id, string include);
        Task<TEntity> GetTrackingNoFilter(long id);
        IQueryable<TEntity> Get(string include = "");
        IQueryable<TEntity> GetNoInclude();
        Task<TEntity> GetNoInclude(long id);
        SessionAppModel SessionApp { get; }
    }
}
