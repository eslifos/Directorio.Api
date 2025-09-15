using Directorio.Api.Data;
using Directorio.Api.Models;
using Directorio.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Directorio.Api.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly DirectorioDbContext _context;

        public FacturaRepository(DirectorioDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Factura factura)
        {
            await _context.Facturas.AddAsync(factura);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Factura?> GetByIdAsync(int id)
        {            
            return await _context.Facturas
                .Include(f => f.Persona)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Factura>> GetByPersonaIdAsync(int personaId)
        {            
            return await _context.Facturas
                .Where(f => f.PersonaId == personaId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Factura factura)
        {
            _context.Facturas.Update(factura);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Facturas.AnyAsync(f => f.Id == id);
        }
    }
}
