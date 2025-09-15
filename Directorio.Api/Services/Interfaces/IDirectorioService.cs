using Directorio.Api.Models;

namespace Directorio.Api.Services.Interfaces
{
    public interface IDirectorioService
    {
        Task CrearPersonaAsync(Persona persona);
        Task<Persona> BuscarPersonaPorIdAsync(int id);
        Task EliminarPersonaAsync(int id);        
    }
}
