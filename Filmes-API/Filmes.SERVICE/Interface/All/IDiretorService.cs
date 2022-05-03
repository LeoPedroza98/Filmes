using Filmes.DOMAIN.Entity.All;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmes.SERVICE.Interface.All
{
    public interface IDiretorService : ICrudService<Diretor>
    {
        Task<List<Diretor>> AutoComplete(string q);
    }
}
