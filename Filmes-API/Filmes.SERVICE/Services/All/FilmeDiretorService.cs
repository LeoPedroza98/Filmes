using Filmes.DATA.Interface.All;
using Filmes.DATA.Repository.All;
using Filmes.DOMAIN.Entity.All;
using Filmes.SERVICE.Interface.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.SERVICE.Services.All
{
    public class FilmeDiretorService : CrudService<FilmeDiretor, IFilmeDiretorRepository>, IFilmeDiretorService
    {
        public FilmeDiretorService(IFilmeDiretorRepository repository) : base(repository)
        {
        }
    }
}
