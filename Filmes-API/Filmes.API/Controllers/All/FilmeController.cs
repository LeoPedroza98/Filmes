using Filmes.DOMAIN.Entity.All;
using Filmes.SERVICE.Interface;
using Filmes.SERVICE.Interface.All;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmes.API.Controllers.All
{

    public class FilmeController : MasterCrudController<Filme>
    {
        private readonly IFilmeService _service;
        public FilmeController(ILogger<MasterCrudController<Filme>> logger, IFilmeService service, string includePatch = "") : base(logger, service, includePatch)
        {
            _service = service;
        }

        [HttpGet("AutoComplete")]
        public async Task<ActionResult<List<Filme>>> AutoComplete(string q)
        {
            if (string.IsNullOrEmpty(q))
                return BadRequest("Filtro não informado!");

            try
            {
                var lista = await _service.AutoComplete(q);

                return Ok(lista);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Algum erro ocorreu! {e.Message} - {e.InnerException?.Message}");
            }
        }
    }
}
