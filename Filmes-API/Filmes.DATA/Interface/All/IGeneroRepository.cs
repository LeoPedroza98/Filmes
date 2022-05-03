using Filmes.DOMAIN.Entity.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.DATA.Interface.All
{
    public interface IGeneroRepository : IQueryRepository<Genero>
    {
        Task<List<Genero>> AutoComplete(string q);
    }
}
