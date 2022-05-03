using Filmes.DOMAIN.Models;
using Filmes.SERVICE.Interface;
using Filmes.SERVICE.Interface.All;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.SERVICE.Services
{
    public class AutenticadorService : IAutenticadorService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;

        public AutenticadorService(IConfiguration configuration, IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        private string GenerateJwtToken(long usuarioId, string usuarioNome, string role, string login)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Upn, usuarioId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuarioNome),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, login),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtExpireMinutes"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<object> Login(string login, string senha)
        {
            var usuario = await _usuarioService.Login(login, senha);

            var retorno = new
            {
                Dados = new SessionAppModel(usuario.Id, usuario.Nome, usuario.Perfil.Nome, usuario.Login)
                {
                    UsuarioFoto = usuario.Foto,
                    TemaEscuro = usuario.TemaEscuro
                },
                Token = GenerateJwtToken(usuario.Id, usuario.Nome, usuario.Perfil.Nome, usuario.Login)
            };

            return retorno;
        }
    }
}
