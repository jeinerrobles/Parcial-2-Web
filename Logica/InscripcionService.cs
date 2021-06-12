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


        public GuardarInscripcionResponse GuardarInscripcion(Inscripcion inscripcion)
        {
            try
            {
                var curso = _context.Cursos.Find(inscripcion.CursoId);
                Curso cupoEncontrado = _context.Cursos.Find(inscripcion.CursoId);
                int edad = CalcularEdad(inscripcion.FechaNacimiento);
                var estudiante = _context.Inscripcions.Find(inscripcion.NumeroIdentificacion);

                if (curso == null)
                {
                    return new GuardarInscripcionResponse("Error, este curso no se encuentra dentro de los ofertados.");

                }
                else
                {
                    if (curso != null && curso.CuposDisponibles > 0 )
                    {
                        if (estudiante == null)
                        {


                            if (edad >= 12 && edad <= 16)
                            {
                                inscripcion.Curso = curso;
                                cupoEncontrado.CuposDisponibles -= 1;
                                _context.Inscripcions.Add(inscripcion);
                                _context.SaveChanges();
                                return new GuardarInscripcionResponse(inscripcion);
                            }
                            else
                            {
                                return new GuardarInscripcionResponse("No se pudo registrar porque no tiene la edad requerida.");
                            }
                        }
                        else
                        {
                            return new GuardarInscripcionResponse("El estudiante ya esta inscrito en este curso.");
                        }
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

        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            
            DateTime fechaActual = DateTime.Today;

         
            if (fechaNacimiento > fechaActual)
            {
                Console.WriteLine("La fecha de nacimiento es mayor que la actual.");
                return -1;
            }
            else
            {
                int edad = fechaActual.Year - fechaNacimiento.Year;

               
                if (fechaNacimiento.Month > fechaActual.Month)
                {
                    --edad;
                }

                return edad;
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
                return new ConsultarInscripcionResponse("Ocurrieron algunos Errores:" + e.Message);
            }
        }

        public List<Curso> BuscarCursoConInscripciones()
        {
            
                List<Curso> cursos = _context.Cursos.Include(t => t.Inscripcions).ToList();
                return cursos;
          
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
