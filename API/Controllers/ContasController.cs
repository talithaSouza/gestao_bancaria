using System.ComponentModel.DataAnnotations;
using Domain.Entidades;
using Domain.Exceptions;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly IContaService _service;

        public ContasController(IContaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Conta novaConta)
        {
            var contaCriada = await _service.CriarAsync(novaConta);

            return Ok(contaCriada);
        }

        [HttpGet]
        public async Task<IActionResult> RetornarConta([FromQuery(Name = "numero_conta"), Required] int numeroConta)
        {
            try
            {
                Conta contaEncontrada = await _service.RetornarAsync(numeroConta);

                return Ok(contaEncontrada);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}