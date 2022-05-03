using Filmes.DATA.Extensions;
using Filmes.DATA.Interface;
using Filmes.DOMAIN.Entity.All;
using Filmes.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmes.DATA.Context
{
    public class FilmesContext : DbContext
    {
        public SessionAppModel SessionApp { get; }
        
        public FilmesContext( DbContextOptions<FilmesContext> options, IFilmeProvider filmeProvider) : base(options)
        {
            SessionApp = filmeProvider.SessionApp;
        }

        public FilmesContext(DbContextOptions<FilmesContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            modelBuilder.Entity<FilmeDiretor>().HasIndex(f => new { f.DiretorId, f.FilmeId }).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(x => x.Login).IsUnique();
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).IsRequired();
        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Diretor> Diretores { get; set; }
        public DbSet<FilmeDiretor> FilmeDiretores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Genero> Generos { get; set; }
    }
}
