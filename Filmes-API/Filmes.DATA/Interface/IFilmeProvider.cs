using Filmes.DOMAIN.Models;

namespace Filmes.DATA.Interface
{
    public interface IFilmeProvider
    {
        SessionAppModel SessionApp { get; }
    }
}
