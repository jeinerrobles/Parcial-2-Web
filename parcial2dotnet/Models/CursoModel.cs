using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parcial2dotnet.Models
{
    public class CursoInputModel
    {
        public string CursoId { get; set; }
        public string Nombre { get; set; }
        public int CuposDisponibles { get; set; }

    }

    public class CursoViewModel : CursoInputModel
    {
        public CursoViewModel()
        {

        }
        public CursoViewModel(Curso curso)
        {
            CursoId = curso.CursoId;
            Nombre = curso.Nombre;
            CuposDisponibles = curso.CuposDisponibles;
            
        }
    }
}

