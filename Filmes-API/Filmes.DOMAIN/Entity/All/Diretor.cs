using Filmes.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.DOMAIN.Entity.All
{
    [Table("Diretor", Schema = "All")]
    public class Diretor: IEntity
    {

        public long Id { get; set; }
        [MaxLength(45), Required] public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Nacionalidade { get; set; }
        public long GeneroId { get; set; }
        public Genero Genero { get; private set; }
        public ICollection<FilmeDiretor> Filmes { get; set; }
        public Diretor()
        {
            Filmes = new List<FilmeDiretor>();
        }
    }
}
