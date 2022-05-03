using Filmes.DOMAIN.Entity.All;
using Filmes.SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Filmes.API.Controllers.All
{
    public class PerfilUsuarioController : MasterQueryController<PerfilUsuario>
    {
        public PerfilUsuarioController(ILogger<MasterQueryController<PerfilUsuario>> logger, IQueryService<PerfilUsuario> service) : base(logger, service)
        {
        }
    }
}
