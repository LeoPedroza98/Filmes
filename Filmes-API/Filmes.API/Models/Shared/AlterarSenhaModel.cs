namespace Filmes.API.Models.Shared
{
    public class AlterarSenhaModel
    {
        public long UsuarioId { get; set; }
        public string SenhaAntiga { get; set; }
        public string SenhaNova { get; set; }
    }
}
