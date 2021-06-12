using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
   public class CursoService
    {
        private readonly Parcial2Context _context;
        public CursoService(Parcial2Context context)
        {
            _context = context;

        }

        public GuardarCursoResponse Guardar(Curso curso)
        {
            try
            {
                var _curso = _context.Cursos.Find(curso.CursoId);
                if (_curso == null)
                {
                    _context.Cursos.Add(curso);
                    _context.SaveChanges();
                    return new GuardarCursoResponse(curso);
                }
                return new GuardarCursoResponse("El curso ya esta registrado");
            }
            catch (Exception e)
            {
                return new GuardarCursoResponse($"Error de la Aplicacion: {e.Message}");
            }

        }
        public List<Curso> ConsultarTodos()
        {

            List<Curso> cursos = _context.Cursos.ToList();

            return cursos;
        }

    }

    public class GuardarCursoResponse
    {
        public GuardarCursoResponse(Curso curso)
        {
            Error = false;
            Curso = curso;
        }
        public GuardarCursoResponse(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Curso Curso { get; set; }
    }
}
