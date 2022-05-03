using Filmes.DOMAIN.Entity.All;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmes.SERVICE.Interface.All
{
    public interface IFilmeService: ICrudService<Filme>
    {
        Task<List<Filme>> AutoComplete(string q);
    }
}
