using System.Threading.Tasks;

namespace Filmes.SERVICE.Interface
{
    public interface IAutenticadorService
    {
        Task<object> Login(string login, string senha);
    }
}
