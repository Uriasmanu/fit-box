using fit_box.Data;
using fit_box.DTOs;
using fit_box.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fit_box.Services
{
    public interface IIngredientesService
    {
        Task<IEnumerable<Ingredientes>> GetAllIngredientesAsync();
        Task<Ingredientes> GetIngredienteByIdAsync(Guid id);
        Task AddIngredienteAsync(IngredienteDto ingredienteDto);
        Task UpdateIngredienteAsync(Guid id, IngredienteDto ingredienteDto);
        Task DeleteIngredienteAsync(Guid id);
    }

    public class IngredientesService : IIngredientesService
    {
        private readonly AppDbContext _context;

        public IngredientesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredientes>> GetAllIngredientesAsync()
        {
            return await _context.Ingredientes.ToListAsync();
        }

        public async Task<Ingredientes> GetIngredienteByIdAsync(Guid id)
        {
            return await _context.Ingredientes.FindAsync(id);
        }

        public async Task AddIngredienteAsync(IngredienteDto ingredienteDto)
        {
            var ingrediente = new Ingredientes
            {
                Id = Guid.NewGuid(),
                NameIngrediente = ingredienteDto.NameIngrediente,
                QuantidadeEmEstoque = ingredienteDto.QuantidadeEmGramas,
                // Defina o LoginId conforme necessário
            };

            _context.Ingredientes.Add(ingrediente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIngredienteAsync(Guid id, IngredienteDto ingredienteDto)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente == null)
            {
                throw new Exception("Ingrediente não encontrado");
            }

            ingrediente.NameIngrediente = ingredienteDto.NameIngrediente;
            ingrediente.QuantidadeEmEstoque = ingredienteDto.QuantidadeEmGramas;

            _context.Ingredientes.Update(ingrediente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngredienteAsync(Guid id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente == null)
            {
                throw new Exception("Ingrediente não encontrado");
            }

            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
        }
    }
}
