using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entidad
{
    public class Curso
    {
       
        public string CursoId { get; set; }
        public string Nombre { get; set; }
        public int CuposDisponibles { get; set; }
        public List<Inscripcion> Inscripcions { get; set; }


    }
}
