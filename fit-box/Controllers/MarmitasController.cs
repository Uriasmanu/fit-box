using fit_box.Services;
using fit_box.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
                return NotFound();
            }

            return Ok(marmita);
        }

        [HttpPost]
        public async Task<ActionResult<Marmita>> CreateMarmita(Marmita marmita)
        {
            var createdMarmita = await _marmitaService.CreateMarmitaAsync(marmita);
            return CreatedAtAction(nameof(GetMarmitas), new { id = createdMarmita.Id }, createdMarmita);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarmita(Guid id)
        {
            var result = await _marmitaService.DeleteMarmitaAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
