using Directorio.Api.Models;

namespace Directorio.Api.Repositories.Interfaces
{
    public interface IFacturaRepository
    {
        Task<Factura?> GetByIdAsync(int id);
        Task<IEnumerable<Factura>> GetByPersonaIdAsync(int personaId);
        Task AddAsync(Factura factura);
        Task DeleteAsync(int id);
        Task UpdateAsync(Factura factura);
        Task<bool> ExistsAsync(int id);
    }
}
