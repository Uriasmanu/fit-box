using fit_box.DTOs;
using fit_box.Models;
using fit_box.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fit_box.Controllers
{
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
        public async Task<ActionResult> AddIngrediente([FromBody] IngredienteDto ingredienteDto)
        {
            await _ingredientesService.AddIngredienteAsync(ingredienteDto);
            return StatusCode(201); // Retorna o status 201 Created sem o ID
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateIngrediente(Guid id, [FromBody] IngredienteDto ingredienteDto)
        {
            try
            {
                await _ingredientesService.UpdateIngredienteAsync(id, ingredienteDto);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
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
