using Filmes.DOMAIN.Entity.All;
using System.Threading.Tasks;

namespace Filmes.DATA.Interface.All
{
    public interface IUsuarioRepository : ICrudRepository<Usuario>
    {
        Task<Usuario> Login(string login);
    }
}
