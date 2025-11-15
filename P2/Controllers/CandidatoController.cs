using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using P2.Models;

namespace P2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CandidatoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Candidatos.ToListAsync());

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var candidato = await _context.Candidatos.FindAsync(id);

            if (candidato == null)
            {
                return NotFound();
            }
            return Ok(candidato);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Candidato candidato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();
            return Ok(candidato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Candidato candidato)
        {
            if (id != candidato.Id)
            {
                return BadRequest();
            }
            if ( !await _context.Candidatos.AnyAsync(x => x.Id == id))
            {
                return NotFound();
            }

            _context.Candidatos.Update(candidato);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            var candidato = await _context.Candidatos.FindAsync(id);

            if (candidato == null)
            {
                return NotFound();
            }

            _context.Candidatos.Remove(candidato);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
