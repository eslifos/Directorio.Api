using Directorio.Api.Models;
using Directorio.Api.Repositories.Interfaces;
using Directorio.Api.Services.Interfaces;

namespace Directorio.Api.Services
{
    public class DirectorioService : IDirectorioService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly ILogger<DirectorioService> _logger;

        public DirectorioService(IPersonaRepository personaRepository, ILogger<DirectorioService> logger)
        {
            _personaRepository = personaRepository;
            _logger = logger;
        }

        public async Task CrearPersonaAsync(Persona persona)
        {
            _logger.LogInformation("Intentando crear una nueva persona con identificación: {Identificacion}", persona.Identificacion);
            if (await _personaRepository.ExistsAsync(persona.Id))
            {
                _logger.LogWarning("La persona con identificación {Identificacion} ya existe.", persona.Identificacion);
                throw new InvalidOperationException($"La persona con identificación {persona.Identificacion} ya existe.");
            }
            await _personaRepository.AddAsync(persona);
            _logger.LogInformation("Persona creada exitosamente.");
        }

        public async Task<Persona> BuscarPersonaPorIdAsync(int id)
        {
            return await _personaRepository.GetByIdAsync(id);
        }

        public async Task EliminarPersonaAsync(int id)
        {
            _logger.LogWarning("Iniciando proceso de borrado para la persona con Id: {Id}", id);
            await _personaRepository.DeleteAsync(id);
            _logger.LogInformation("Borrado de persona Id: {Id} completado.", id);
        }
    }
}
