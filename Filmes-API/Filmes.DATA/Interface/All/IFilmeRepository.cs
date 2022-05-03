using Filmes.DOMAIN.Entity.All;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmes.DATA.Interface.All
{
    public interface IFilmeRepository: ICrudRepository<Filme>
    {
        Task<List<Filme>> AutoComplete(string q);
    }
}
