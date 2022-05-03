using Filmes.DOMAIN.Entity.All;
using Microsoft.EntityFrameworkCore;

namespace Filmes.DATA.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region All
            modelBuilder.Entity<PerfilUsuario>().HasData(PerfilUsuario.ObterDados());
            modelBuilder.Entity<Genero>().HasData(Genero.ObterDados());
            #endregion
        }
    }
}
