using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using API.DTO;
using AutoMapper;
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
        private IMapper _mapper;

        public ContasController(IContaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] ContaDTO novaContaDTO)
        {
            try
            {
                var novaContaEntity = _mapper.Map<Conta>(novaContaDTO);

                var contaCriada = await _service.CriarAsync(novaContaEntity);

                return Created(new Uri(Url.Link("RetornarContaPorNumero", new { numero_conta = contaCriada.NumeroConta })), _mapper.Map<ContaDTO>(contaCriada));
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ConflitoContaException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet(Name = "RetornarContaPorNumero")]
        public async Task<IActionResult> RetornarConta([FromQuery(Name = "numero_conta"), Required] int numeroConta)
        {
            try
            {
                Conta contaEncontrada = await _service.RetornarAsync(numeroConta);

                return Ok(_mapper.Map<ContaDTO>(contaEncontrada));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}