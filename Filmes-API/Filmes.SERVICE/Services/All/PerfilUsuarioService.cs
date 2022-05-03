using Filmes.DATA.Interface.All;
using Filmes.DOMAIN.Entity.All;
using Filmes.SERVICE.Interface.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.SERVICE.Services.All
{
    public class PerfilUsuarioService : QueryService<PerfilUsuario, IPerfilUsuarioRepository>, IPerfilUsuarioService
    {
        public PerfilUsuarioService(IPerfilUsuarioRepository repository) : base(repository)
        {
        }
    }
}
