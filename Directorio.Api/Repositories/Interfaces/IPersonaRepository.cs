using Directorio.Api.Models;

namespace Directorio.Api.Repositories.Interfaces
{
    public interface IPersonaRepository
    {
        Task<Persona> GetByIdAsync(int id);
        Task<IEnumerable<Persona>> GetAllAsync();
        Task AddAsync(Persona persona);
        Task DeleteAsync(int id);
        Task UpdateAsync(Persona persona);
        Task<bool> ExistsAsync(int id);        
    }
}
