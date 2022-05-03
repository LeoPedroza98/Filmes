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
    [Table("Genero", Schema = "All")]
    public class Genero : IEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)] public long Id { get; set; }

        [MaxLength(60)] public string Nome { get; set; }

        [NotMapped] public static Genero Masculino => new Genero(1, "Masculino");
        [NotMapped] public static Genero Feminimo => new Genero(2, "Feminimo");

        public Genero(long id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        public static Genero[] ObterDados()
        {
            return new Genero[]
            {
                Masculino,
                Feminimo
            };
        }
    }
}
