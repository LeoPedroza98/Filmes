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
    public class DiretorRepository : CrudRepository<Diretor>, IDiretorRepository
    {
        public DiretorRepository(FilmesContext context) : base(context)
        {
        }

        public async Task<List<Diretor>> AutoComplete(string q)
        {
            var query = _context.Diretores
                .AsNoTracking()
                .Where(x =>
                x.Nome.ToUpper().Contains(q.ToUpper()));

            return await query.ToListAsync();
        }
    }
}
