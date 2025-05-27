using Domain.Entidades;
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
            System.Console.WriteLine("entrou na controller");
            var contaCriada = await _service.CriarAsync(novaConta);

            return Ok(contaCriada);
        }
    }
}