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
    public class FilmeRepository : CrudRepository<Filme>, IFilmeRepository
    {
        public FilmeRepository(FilmesContext context) : base(context)
        {
        }

        public async Task<List<Filme>> AutoComplete(string q)
        {
            var query = _context.Filmes
                .AsNoTracking()
                .Where(x =>
                x.Titulo.ToUpper().Contains(q.ToUpper()));

            return await query.ToListAsync();
        }
    }
}
