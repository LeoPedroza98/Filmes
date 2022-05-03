using Filmes.DOMAIN.Entity.All;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmes.SERVICE.Interface.All
{
    public interface IGeneroService: IQueryService<Genero>
    {
        Task<List<Genero>> AutoComplete(string q);
    }
}
