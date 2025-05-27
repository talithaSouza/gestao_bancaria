using System.ComponentModel.DataAnnotations;
using System.Net;
using API.DTO;
using AutoMapper;
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
        private readonly IMovimentacaoService _movimentacaoService;
        private readonly IMapper _mapper;

        public TransacoesController(ITransacaoService service, IMovimentacaoService movimentacaoService, IMapper mapper)
        {
            _service = service;
            _movimentacaoService = movimentacaoService;
            _mapper = mapper;
        }

        /// <summary>
        /// <para>Realiza transações de saldo de acordo com o tipo.</para>
        /// <para>Transações do tipo Débito adicionam 3% de juros sobre a operação.</para>
        /// <para>Transações do tipo Crédito adicionam 5% de juros sobre a operação.</para>
        /// </summary>
        /// <param name="tipoTransacao">P: PIX; D: Débito; C: Credito</param>
        /// <param name="contaDTO">Dados da conta com numero da conta e o valor que irá retirar</param>
        /// <returns>Dados da conta com saldo atualiado</returns>
        /// <response code="201">Transação realizada com sucesso</response>
        /// <response code="404">Conta não encontrada ou saldo insuficiente</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost]
        public async Task<IActionResult> RealizarTransacao([FromQuery(Name = "Tipo"), Required] TipoTransacao tipoTransacao, [FromBody] ContaDTO contaDTO)
        {
            try
            {
                Conta contaSaldoAtualizado = await _service.ExecutarSaquesAsync(tipoTransacao, contaDTO.NumeroConta, contaDTO.Saldo);

                var uri = new Uri(Url.Link("GetMovimentacoesPorConta", new { numeroConta = contaSaldoAtualizado.NumeroConta }));

                return Created(uri, _mapper.Map<ContaDTO>(contaSaldoAtualizado));
            }
            catch (SaldoInsificienteException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotFoundException ex)
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

        [HttpGet("conta/{numeroConta}/movimentacoes", Name = "GetMovimentacoesPorConta")]
        public async Task<IActionResult> BuscarMovimentacoesDaConta(int numeroConta)
        {
            try
            {
                List<Movimentacao> movimentacoesDaConta = await _movimentacaoService.RetornarMovimentacoesPorConta(numeroConta);

                return Ok(_mapper.Map<List<MovimentacaoDTO>>(movimentacoesDaConta));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}