using Microsoft.EntityFrameworkCore;
using PlataformaEventosTech_API.Models;

namespace PlataformaEventosTech_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Evento> Eventos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }     
    }
}
