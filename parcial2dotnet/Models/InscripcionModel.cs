using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parcial2dotnet.Models
{
    public class InscripcionInputModel
    {
        public int InscripcionId { get; set; }
        public CursoInputModel Curso { get; set; } // se hace referencia al atributo de llave foranea
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }

    public class InscripcionViewModel
    {
        public int InscripcionId { get; set; }
        public string CursoId { get; set; }
        public Curso Curso { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public InscripcionViewModel(Inscripcion inscripcion)
        {
            InscripcionId = inscripcion.InscripcionId;
            CursoId = inscripcion.Curso.CursoId;
            TipoIdentificacion = inscripcion.TipoIdentificacion;
            NumeroIdentificacion = inscripcion.NumeroIdentificacion;
            Curso = new Curso() { CursoId = inscripcion.Curso.CursoId, Nombre = inscripcion.Curso.Nombre, CuposDisponibles = inscripcion.Curso.CuposDisponibles };
            Nombre = inscripcion.Nombre;
            FechaNacimiento = inscripcion.FechaNacimiento;

        }
    }

    public class InscripcionCursoViewModel : InscripcionInputModel
    {


        public InscripcionCursoViewModel(Inscripcion inscripcion)
        {
            InscripcionId = inscripcion.InscripcionId;
            TipoIdentificacion = inscripcion.TipoIdentificacion;
            NumeroIdentificacion = inscripcion.NumeroIdentificacion;
            Nombre = inscripcion.Nombre;
            FechaNacimiento = inscripcion.FechaNacimiento;
        }

    }
}
