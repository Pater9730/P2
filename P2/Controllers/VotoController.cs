using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P2.Models;

namespace P2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VotoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] Voto voto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _context.Candidatos.AnyAsync(j => j.Id == voto.CandidatoId))
            {
                return BadRequest("El candidato no existe");
            }

            _context.Votos.Add(voto);
            await _context.SaveChangesAsync();
            return Ok(voto);    
        }
    }
}
