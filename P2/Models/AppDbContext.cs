using Microsoft.EntityFrameworkCore;

namespace P2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Voto> Votos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
