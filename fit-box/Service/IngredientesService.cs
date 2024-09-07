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
            return await _context.Ingrediente.ToListAsync();
        }

        public async Task<Ingredientes> GetIngredienteByIdAsync(Guid id)
        {
            return await _context.Ingrediente.FindAsync(id);
        }

        public async Task AddIngredienteAsync(IngredienteDto ingredienteDto)
        {
            var loginExists = await _context.Logins.AnyAsync(l => l.Id == ingredienteDto.LoginId);
            if (!loginExists)
            {
                throw new Exception("LoginId fornecido não existe");
            }

            var ingredienteExists = await _context.Ingrediente
                .AnyAsync(i => i.NameIngrediente == ingredienteDto.NameIngrediente && i.LoginId == ingredienteDto.LoginId);

            if (ingredienteExists)
            {
                throw new Exception("Já existe um ingrediente com esse nome cadastrado.");
            }

            var ingrediente = new Ingredientes
            {
                Id = Guid.NewGuid(),
                NameIngrediente = ingredienteDto.NameIngrediente,
                QuantidadeEmEstoque = ingredienteDto.QuantidadeEmGramas,
                LoginId = ingredienteDto.LoginId
            };

            _context.Ingrediente.Add(ingrediente);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateIngredienteAsync(Guid id, IngredienteDto ingredienteDto)
        {
            // Verificar se o ingrediente a ser atualizado existe
            var ingrediente = await _context.Ingrediente.FindAsync(id);
            if (ingrediente == null)
            {
                throw new Exception("Ingrediente não encontrado.");
            }

            // Verificar se já existe outro ingrediente com o mesmo nome para o mesmo LoginId
            var ingredienteExists = await _context.Ingrediente
                .AnyAsync(i => i.NameIngrediente == ingredienteDto.NameIngrediente
                            && i.LoginId == ingredienteDto.LoginId
                            && i.Id != id);

            if (ingredienteExists)
            {
                throw new Exception("Já existe um ingrediente com esse nome cadastrado.");
            }

            // Atualizar o ingrediente
            ingrediente.NameIngrediente = ingredienteDto.NameIngrediente;
            ingrediente.QuantidadeEmEstoque = ingredienteDto.QuantidadeEmGramas;

            _context.Ingrediente.Update(ingrediente);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteIngredienteAsync(Guid id)
        {
            var ingrediente = await _context.Ingrediente.FindAsync(id);
            if (ingrediente == null)
            {
                throw new Exception("Ingrediente não encontrado");
            }

            _context.Ingrediente.Remove(ingrediente);
            await _context.SaveChangesAsync();
        }
    }
}
