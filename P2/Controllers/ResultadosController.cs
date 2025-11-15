using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P2.Models;

namespace P2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResultadosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Ganador")]
        public async Task<IActionResult> GetWinner()
        {
            var ganador = await _context.Candidatos.Select(p => new CandidatoDto
            {
                Nombre = p.Nombre,
                Id = p.Id,
                TotalVotos = _context.Votos.Where(a => a.CandidatoId == p.Id).Count(),
            })
                .OrderByDescending(q => q.TotalVotos).FirstOrDefaultAsync();

            if ( ganador == null)
            {
                return Ok("No hay votos registrados");
            }
            
            return Ok(ganador);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> Get(int id)
        {
            var candidato = await _context.Candidatos.Where(e => e.Id == id).Select(p => new CandidatoDto
            {
                Nombre = p.Nombre,
                Id = p.Id,
                TotalVotos = _context.Votos.Count(a => a.CandidatoId == p.Id)
            })
                .FirstOrDefaultAsync();

            if (candidato == null)
            {
                return NotFound("Candidato no encontrado.");
            }
                

            return Ok(candidato);

        }
        
            
            


    }
}
