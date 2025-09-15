using Directorio.Api.Models;
using Directorio.Api.Services;
using Directorio.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Directorio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorioController : ControllerBase
    {
        private readonly IDirectorioService _directorioService;

        public DirectorioController(IDirectorioService directorioService)
        {
            _directorioService = directorioService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPersona([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _directorioService.CrearPersonaAsync(persona);
            return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersona(int id)
        {
            var persona = await _directorioService.BuscarPersonaPorIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            await _directorioService.EliminarPersonaAsync(id);
            return NoContent();
        }
    }
}
