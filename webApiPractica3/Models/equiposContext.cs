using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using practicaMVC05.Models;
using webApiPractica3.Models;
namespace webApiPractica3.Models
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions<equiposContext> options) :base(options) 
        { 
        }

        public DbSet<equipos> equipos { get; set; }

        public DbSet<practicaMVC05.Models.carreras>? carreras { get; set; }

        public DbSet<practicaMVC05.Models.estados_reserva>? estados_reserva { get; set; }

        public DbSet<practicaMVC05.Models.estados_equipo>? estados_equipo { get; set; }

        public DbSet<practicaMVC05.Models.facultades>? facultades { get; set; }

        public DbSet<practicaMVC05.Models.marcas>? marcas { get; set; }

        public DbSet<webApiPractica3.Models.reservas>? reservas { get; set; }

        public DbSet<practicaMVC05.Models.tipo_equipo>? tipo_equipo { get; set; }

        public DbSet<practicaMVC05.Models.usuarios>? usuarios { get; set; }
    }
}
