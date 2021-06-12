using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datos;
using Entidad;
using Microsoft.EntityFrameworkCore;

namespace Logica
{ 
    public class InscripcionService
    {
        private readonly Parcial2Context _context;
        public InscripcionService(Parcial2Context context)
        {
            _context = context;

        }

        public void ActualizarCantidadCupos(Curso curso)
        {


            foreach (Inscripcion inscripcion in curso.Inscripcions)
            {
                Curso cupoEncontrado = _context.Cursos.Find(inscripcion.CursoId);
                cupoEncontrado.CuposDisponibles -= 1;
                _context.Cursos.Update(cupoEncontrado);
                _context.SaveChanges();
            }
        }
        public GuardarInscripcionResponse GuardarInscripcion(Inscripcion inscripcion)
        {
            try
            {
                var curso = _context.Cursos.Find(inscripcion.CursoId);
                if (curso == null)
                {
                    return new GuardarInscripcionResponse("Error, este curso no se encuentra dentro de los ofertados.");

                }
                else
                {
                    if (curso != null && curso.CuposDisponibles > 0 )
                    {
                        inscripcion.Curso = curso; // busca el curso de la bd y lo reemplaza por el que estoy haciendo
                        _context.Inscripcions.Add(inscripcion);
                        _context.SaveChanges();
                        return new GuardarInscripcionResponse(inscripcion);
                    }
                    else
                    {
                        return new GuardarInscripcionResponse("No se pudo registrar porque no quedaron cupos para este curso.");
                    }
                  
                }

               
            }
            catch (Exception e)
            {
                return new GuardarInscripcionResponse("Ocurrieron algunos Errores:" + e.Message);
            }
        }

        public ConsultarInscripcionResponse ConsultarTodos()
        {
            try
            {
                var inscripciones = _context.Inscripcions.Include(p => p.Curso).ToList();
                return new ConsultarInscripcionResponse(inscripciones);
            }
            catch (Exception e)
            {
                return new ConsultarInscripcionResponse("Ocurriern algunos Errores:" + e.Message);
            }
        }

        public ConsultarCursoResponse BuscarCursoConInscripciones()
        {
            try
            {
                var tercero = _context.Cursos.Include(t => t.Inscripcions).ToList();
               
                    return new ConsultarCursoResponse(tercero);
               
            }
            catch (Exception e)
            {
                return new ConsultarCursoResponse("Ocurriern algunos Errores:" + e.Message);
            }
        }

    }




    public class GuardarInscripcionResponse
    {
        public Inscripcion Inscripcion { get; set; }
        public string Mensaje { get; set; }
        public bool Error { get; set; }


        public GuardarInscripcionResponse(Inscripcion inscripcion)
        {
            Inscripcion = inscripcion;
            Error = false;
        }

        public GuardarInscripcionResponse(string mensaje)
        {
            Mensaje = mensaje;
            Error = true;
        }

    }
    public class ConsultarInscripcionResponse
    {
        public List<Inscripcion> Inscripcions { get; set; }
        public string Mensaje { get; set; }
        public bool Error { get; set; }


        public ConsultarInscripcionResponse(List<Inscripcion> inscripcions)
        {
            Inscripcions = inscripcions;
            Error = false;
        }

        public ConsultarInscripcionResponse(string mensaje)
        {
            Mensaje = mensaje;
            Error = true;
        }

    }

    public class ConsultarCursoResponse
    {
        public List<Curso> Curso { get; set; }
        public string Mensaje { get; set; }
        public bool Error { get; set; }


        public ConsultarCursoResponse(List<Curso> curso)
        {
            Curso = curso;
            Error = false;
        }

        public ConsultarCursoResponse(string mensaje)
        {
            Mensaje = mensaje;
            Error = true;
        }

    }
}
