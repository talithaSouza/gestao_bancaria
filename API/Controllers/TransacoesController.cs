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

        /// <summary>
        /// <para>Realiza transações de saldo de acordo com o tipo.</para>
        /// <para>Transações do tipo Débito adicionam 3% de juros sobre a operação.</para>
        /// <para>Transações do tipo Crédito adicionam 5% de juros sobre a operação.</para>
        /// </summary>
        /// <param name="tipoTransacao">P: PIX; D: Débito; C: Credito</param>
        /// <param name="conta">Dados da conta com numero da conta e o valor que irá retirar</param>
        /// <returns>Dados da conta com saldo atualiado</returns>
        /// <response code="201">Transação realizada com sucesso</response>
        /// <response code="404">Conta não encontrada ou saldo insuficiente</response>
        /// <response code="500">Erro interno do servidor</response>
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