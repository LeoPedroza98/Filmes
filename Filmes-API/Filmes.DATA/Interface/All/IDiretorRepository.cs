using Filmes.DOMAIN.Entity.All;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmes.DATA.Interface.All
{
    public interface IDiretorRepository : ICrudRepository<Diretor>
    {
        Task<List<Diretor>> AutoComplete(string q);
    }
}
