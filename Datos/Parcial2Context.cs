using Microsoft.EntityFrameworkCore;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
   public class Parcial2Context : DbContext
    {
        public Parcial2Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Inscripcion> Inscripcions { get; set; }
    }
}
