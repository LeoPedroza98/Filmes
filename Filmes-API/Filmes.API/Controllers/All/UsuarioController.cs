using Filmes.API.Helpers;
using Filmes.API.Models.Shared;
using Filmes.DOMAIN.Entity.All;
using Filmes.SERVICE.Interface.All;
using Filmes.UTIL.Exceptions;
using Filmes.UTIL.Helpers;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmes.API.Controllers.All
{
    public class UsuarioController : MasterCrudController<Usuario>
    {
        private readonly IUsuarioService _service;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService service, string includePatch = "") : base(logger, service, includePatch)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public override async Task<ActionResult<Usuario>> Post([FromBody] Usuario model)
        {
            return await base.Post(model);
        }

        [HttpPatch("{id}")]
        public override async Task<IActionResult> Patch(long id, [FromBody] JsonPatchDocument<Usuario> model)
        {
            try
            {
                model.Operations.RemoveAll(x => x.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Test);

                await _service.Patch(id, model, _includePatch);

                return Ok(MensagemHelper.OperacaoSucesso);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("{0} - {1}", e.Message, e.InnerException?.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"{MensagemHelper.AlgumErroOcorreu} {e.Message} - {e.InnerException?.Message}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [CustomAuthorize("Master")]
        public override ActionResult<List<Usuario>> Get(ODataQueryOptions<Usuario> options, [FromHeader] string include)
        {
            try
            {
                var pageResult = GetPageResult(_service.Get(include), options);

                return Ok(pageResult);
            }
            catch (Exception e)
            {
                _logger.LogError("{0} - {1}", e.Message, e.InnerException?.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"{MensagemHelper.AlgumErroOcorreu} {e.Message} - {e.InnerException?.Message}");
            }
        }

        [HttpPut("{id}/MudarTema")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> MudarTema(long id)
        {
            try
            {
                var temaEscuro = await _service.MudarTema(id);

                return Ok(temaEscuro);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("{0} - {1}", e.Message, e.InnerException?.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"{MensagemHelper.AlgumErroOcorreu} {e.Message} - {e.InnerException?.Message}");
            }

        }

        [HttpPost("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaModel model)
        {
            try
            {
                await _service.AlterarSenha(model.UsuarioId, model.SenhaAntiga, model.SenhaNova);

                return Ok(MensagemHelper.OperacaoSucesso);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("{0} - {1}", e.Message, e.InnerException?.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"{MensagemHelper.AlgumErroOcorreu} {e.Message} - {e.InnerException?.Message}");
            }
        }
    }
}
