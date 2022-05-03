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
    public class UsuarioRepository : CrudRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(FilmesContext context) : base(context)
        {
        }

        public override void Update(Usuario entity)
        {
            _context.Entry(entity).Property(x => x.Login).IsModified = false;

            base.Update(entity);
        }

        public async Task<Usuario> Login(string login)
        {
            var usuario = await _context.Usuarios
                    .Include(x => x.Perfil)
                    .IgnoreQueryFilters()
                    .SingleOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper());

            return usuario;
        }
    }
}
