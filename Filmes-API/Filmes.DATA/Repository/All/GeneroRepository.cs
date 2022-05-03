using Filmes.DATA.Context;
using Filmes.DATA.Interface.All;
using Filmes.DOMAIN.Entity.All;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.DATA.Repository.All
{
    public class GeneroRepository : QueryRepository<Genero>, IGeneroRepository
    {
        public GeneroRepository(FilmesContext context) : base(context)
        {
        }

        public async Task<List<Genero>> AutoComplete(string q)
        {
            var query = _context.Generos
                .AsNoTracking()
                .Where(x =>
                x.Nome.ToUpper().Contains(q.ToUpper()));

            return await query.ToListAsync();
        }
    }
}
