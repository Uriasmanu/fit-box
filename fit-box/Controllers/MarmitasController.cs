using fit_box.DTOs;
using fit_box.Services;
using fit_box.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace fit_box.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MarmitasController : ControllerBase
    {
        private readonly MarmitaService _marmitaService;

        public MarmitasController(MarmitaService marmitaService)
        {
            _marmitaService = marmitaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marmita>>> GetMarmitas()
        {
            var marmitas = await _marmitaService.GetMarmitasAsync();
            return Ok(marmitas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Marmita>> GetMarmita(Guid id)
        {
            var marmita = await _marmitaService.GetMarmitaByIdAsync(id);

            if (marmita == null)
            {
                return NotFound(new { Message = "Marmita não encontrada." });
            }

            return Ok(marmita);
        }

        [HttpPost]
        public async Task<ActionResult<Marmita>> CreateMarmita([FromBody] MarmitaDto marmitaDto)
        {
            var token = Request.Headers["Authorization"].ToString();
            Console.WriteLine("Received Token: " + token);
            try
            {
                var createdMarmita = await _marmitaService.CreateMarmitaAsync(marmitaDto);
                return CreatedAtAction(nameof(GetMarmita), new { id = createdMarmita.Id }, createdMarmita);
            }
            catch (DbUpdateException dbEx)
            {
                // Captura detalhes específicos de problemas de banco de dados
                var innerException = dbEx.InnerException?.Message ?? "Erro desconhecido no banco de dados.";
                return StatusCode(500, new { Message = "Erro ao criar marmita.", Details = innerException });
            }
            catch (Exception ex)
            {
                // Verifica se a exceção é sobre o LoginId não encontrado
                if (ex.Message.Contains("LoginId fornecido não existe"))
                {
                    return BadRequest(new { Message = "O LoginId fornecido não existe." });
                }

                // Para outras exceções, retornar uma resposta genérica
                return StatusCode(500, new { Message = "Erro ao criar marmita.", Details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarmita(Guid id)
        {
            var result = await _marmitaService.DeleteMarmitaAsync(id);
            if (!result)
            {
                return NotFound(new { Message = "Marmita não encontrada." });
            }

            return NoContent();
        }
    }
}
