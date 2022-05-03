using Filmes.DATA.Context;
using Filmes.DATA.Interface.All;
using Filmes.DOMAIN.Entity.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.DATA.Repository.All
{
    public class FilmeDiretorRepository : CrudRepository<FilmeDiretor>, IFilmeDiretorRepository
    {
        public FilmeDiretorRepository(FilmesContext context) : base(context)
        {
        }
    }
}
