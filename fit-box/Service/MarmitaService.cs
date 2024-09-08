using fit_box.Data;
using fit_box.Models;
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

        // Criar nova marmita
        public async Task<Marmita> CreateMarmitaAsync(Marmita marmita)
        {
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
