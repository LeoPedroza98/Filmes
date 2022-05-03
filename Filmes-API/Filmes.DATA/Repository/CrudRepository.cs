using Filmes.DATA.Context;
using Filmes.DATA.Interface;
using Filmes.DOMAIN.Interface;
using Filmes.UTIL.Exceptions;
using Filmes.UTIL.Helpers;
using System.Threading.Tasks;

namespace Filmes.DATA.Repository
{
    public class CrudRepository<TEntity> : QueryRepository<TEntity>, ICrudRepository<TEntity> where TEntity : class, IEntity
    {
        public CrudRepository(FilmesContext context) : base(context)
        {

        }

        public async Task Insert(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task Remove(long id)
        {
            var entity = await GetByIdNoIncludeAsync(id);

            if (entity == null)
                throw new NotFoundException(MensagemHelper.RegistroNaoEncontrato);

            _context.Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Remove(entity);
        }

        public void Detached(TEntity entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }

        public async Task SaveChangesAsync()
        {

            await _context.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            _context.Update(entity);
        }
    }
}
