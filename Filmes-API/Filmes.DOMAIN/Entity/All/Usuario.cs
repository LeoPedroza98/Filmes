using Filmes.DOMAIN.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Filmes.DOMAIN.Entity.All
{
    [Table("Usuario", Schema = "All")]
    public class Usuario : IEntity
    {
        private string _senha;
        public long Id { get; set; }
        [MaxLength(120), Required] public string Login { get; set; }
        public string Senha { get { return null; } set { _senha = value; } }
        public DateTime DataHoraCriacao { get; set; }
        [Required] public string Nome { get; set; }
        public byte[] Foto { get; set; }
        public long PerfilId { get; set; }
        public PerfilUsuario Perfil { get; set; }
        [Required] public bool Ativo { get; set; }
        public bool TemaEscuro { get; set; }
        
        public Usuario()
        {
            DataHoraCriacao = DateTime.Now;
            Ativo = true;
        }

        public string GetSenha()
        {
            return _senha;
        }
    }
}
