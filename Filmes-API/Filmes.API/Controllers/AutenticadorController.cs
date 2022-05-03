using Filmes.API.Models.Shared;
using Filmes.DOMAIN.Models;
using Filmes.SERVICE.Interface;
using Filmes.SERVICE.Interface.All;
using Filmes.UTIL.Exceptions;
using Filmes.UTIL.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.API.Controllers
{
    public class AutenticadorController : MasterBaseController
    {
        private readonly IAutenticadorService _autenticadorService;
        public AutenticadorController(IAutenticadorService autenticadorService)
        {
            _autenticadorService = autenticadorService;
        }

        [HttpPost("Usuario")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var retorno = await _autenticadorService.Login(model.Login, model.Senha);
                return Ok(retorno);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"{MensagemHelper.AlgumErroOcorreu} {e.Message} - {e.InnerException?.Message}");
            }
        }
    }
}
