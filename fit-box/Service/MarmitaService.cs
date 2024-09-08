using fit_box.Data;
using fit_box.Models;
using fit_box.DTOs;
using Microsoft.EntityFrameworkCore;

namespace fit_box.Services
{
    public class MarmitaService
    {
        private readonly AppDbContext _context;

        public MarmitaService(AppDbContext context)
        {
            _context = context;
        }

        // Obter todas as marmitas
        public async Task<IEnumerable<Marmita>> GetMarmitasAsync()
        {
            return await _context.Marmitas.Include(m => m.Ingredientes).ToListAsync();
        }

        // Obter marmita por ID
        public async Task<Marmita> GetMarmitaByIdAsync(Guid id)
        {
            return await _context.Marmitas.Include(m => m.Ingredientes).FirstOrDefaultAsync(m => m.Id == id);
        }

        // Verificar se o LoginId existe
        private async Task<bool> LoginExistsAsync(Guid loginId)
        {
            return await _context.Logins.AnyAsync(l => l.Id == loginId);
        }

        // Criar nova marmita com ingredientes do DTO
        public async Task<Marmita> CreateMarmitaAsync(MarmitaDto marmitaDto)
        {
            // Verifica se o LoginId fornecido existe
            var loginExists = await _context.Logins.AnyAsync(l => l.Id == marmitaDto.LoginId);
            if (!loginExists)
            {
                throw new Exception("LoginId fornecido não existe.");
            }

            // Cria a nova marmita
            var marmita = new Marmita
            {
                Id = Guid.NewGuid(),
                NameMarmita = marmitaDto.NameMarmita,
                TamanhoMarmita = marmitaDto.TamanhoMarmita,
                LoginId = marmitaDto.LoginId,
                Ingredientes = marmitaDto.Ingredientes.Select(dto => new Ingredientes
                {
                    NameIngrediente = dto.NameIngrediente,
                    QuantidadeEmGramas = dto.QuantidadeEmGramas
                }).ToList()
            };

            _context.Marmitas.Add(marmita);
            await _context.SaveChangesAsync();
            return marmita;
        }


        // Deletar marmita
        public async Task<bool> DeleteMarmitaAsync(Guid id)
        {
            var marmita = await _context.Marmitas.FindAsync(id);
            if (marmita == null)
                return false;

            _context.Marmitas.Remove(marmita);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
