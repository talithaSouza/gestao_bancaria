using System.ComponentModel.DataAnnotations;
using System.Net;
using Domain.Entidades;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacaoService _service;

        public TransacoesController(ITransacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> RealizarTransacao([FromQuery(Name = "Tipo"), Required] TipoTransacao tipoTransacao, [FromBody] Conta conta)
        {
            try
            {
                Conta contaSaldoAtualizado = await _service.ExecutarSaquesAsync(tipoTransacao,conta.NumeroConta, conta.Saldo);

                return Ok(contaSaldoAtualizado);
            }
            catch (SaldoInsificienteException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}