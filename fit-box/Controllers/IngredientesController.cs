using fit_box.DTOs;
using fit_box.Models;
using fit_box.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fit_box.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientesController : ControllerBase
    {
        private readonly IIngredientesService _ingredientesService;

        public IngredientesController(IIngredientesService ingredientesService)
        {
            _ingredientesService = ingredientesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredientes>>> GetAllIngredientes()
        {
            var ingredientes = await _ingredientesService.GetAllIngredientesAsync();
            return Ok(ingredientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredientes>> GetIngredienteById(Guid id)
        {
            var ingrediente = await _ingredientesService.GetIngredienteByIdAsync(id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return Ok(ingrediente);
        }

        [HttpPost]
        public async Task<IActionResult> AddIngrediente(IngredienteDto ingredienteDto)
        {
            try
            {
                // Convertendo todas as propriedades para minúsculas
                ingredienteDto.NameIngrediente = ingredienteDto.NameIngrediente.ToLower();

                await _ingredientesService.AddIngredienteAsync(ingredienteDto);
                return Ok();
            }
            catch (Exception ex)
            {
                // Retorne uma resposta com status 400 (Bad Request) e a mensagem de erro
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngrediente(Guid id, IngredienteDto ingredienteDto)
        {
            try
            {
                // Convertendo todas as propriedades para minúsculas
                ingredienteDto.NameIngrediente = ingredienteDto.NameIngrediente.ToLower();

                await _ingredientesService.UpdateIngredienteAsync(id, ingredienteDto);
                return Ok();
            }
            catch (Exception ex)
            {
                // Retorne uma resposta com status 400 (Bad Request) e a mensagem de erro
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIngrediente(Guid id)
        {
            try
            {
                await _ingredientesService.DeleteIngredienteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
