using Filmes.DOMAIN.Entity.All;
using Filmes.SERVICE.Interface;
using Filmes.SERVICE.Interface.All;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Filmes.API.Controllers.All
{
    public class FilmeDiretorController : MasterCrudController<FilmeDiretor>
    {
        private readonly IFilmeDiretorService _service;

        public FilmeDiretorController(ILogger<MasterCrudController<FilmeDiretor>> logger, IFilmeDiretorService service, string includePatch = "") : base(logger, service, includePatch)
        {
        }
    }
}
