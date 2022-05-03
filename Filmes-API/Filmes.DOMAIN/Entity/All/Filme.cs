using Filmes.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Filmes.DOMAIN.Entity.All
{
    [Table("Filme", Schema = "All")]
    public class Filme : IEntity
    {

        public long Id { get; set; }
        [MaxLength(45),Required] public string Titulo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataLancamento { get; set; }
        [MaxLength(255), Required] public string Sinopse { get; set; }
        public int Avaliacoes { get; set; }
        public string Genero { get; set; }
        public byte[] Poster { get; set; }
        public ICollection<FilmeDiretor> Diretores { get; set; }
        public Filme()
        {
            Diretores = new List<FilmeDiretor>();
        }
    }
}
