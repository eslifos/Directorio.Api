using Directorio.Api.Data;
using Directorio.Api.Models;
using Directorio.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Directorio.Api.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly DirectorioDbContext _context;

        public PersonaRepository(DirectorioDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            return await _context.Personas.Include(p => p.Facturas).ToListAsync();
        }

        public async Task<Persona> GetByIdAsync(int id)
        {
            return await _context.Personas.Include(p => p.Facturas).FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new InvalidOperationException("No se encontró la persona");
        }

        public async Task UpdateAsync(Persona persona)
        {
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Personas.AnyAsync(p => p.Id == id);
        }
    }
}
