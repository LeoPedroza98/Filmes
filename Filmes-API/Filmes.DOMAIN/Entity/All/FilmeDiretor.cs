using Filmes.DOMAIN.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace Filmes.DOMAIN.Entity.All
{
    [Table("FilmeDiretor", Schema = "All")]
    public class FilmeDiretor : IEntity
    {
        public long Id { get; set; }
        public long FilmeId { get; set; }
        public Filme Filme { get; set; }
        public long DiretorId { get; set; }
        public Diretor Diretor { get; set; }
    }
}
