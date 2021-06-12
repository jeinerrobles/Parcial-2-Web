using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Logica;
using Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using parcial2dotnet.Models;

namespace parcial2dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly CursoService _cursoService;
        private readonly InscripcionService _InscripcionService;
        public CursoController(Parcial2Context context)
        {
            _cursoService = new CursoService(context);
            _InscripcionService = new InscripcionService(context);
        }


        // GET: api/Curso
        [HttpGet]
        public IEnumerable<CursoViewModel> Gets()
        {
            var cursos = _cursoService.ConsultarTodos().Select(p => new CursoViewModel(p));
            return cursos;
        }

        [HttpGet("ConInscripciones")]
        public IEnumerable<CursoConInscripcionesViewModel> GetCursoInscripciones()
        {
            var cursos = _InscripcionService.BuscarCursoConInscripciones().Select(p => new CursoConInscripcionesViewModel(p));
            return cursos;


        }

        // POST: api/Curso
        [HttpPost]
        public ActionResult<CursoViewModel> Post(CursoInputModel cursoInput)
        {
            Curso curso = MapearCurso(cursoInput);
            var response = _cursoService.Guardar(curso);
            if (response.Error)
            {
                return BadRequest(response.Mensaje);
            }
            return Ok(response.Curso);
        }



        private Curso MapearCurso(CursoInputModel cursoInput)
        {
            var curso = new Curso
            {
                CursoId = cursoInput.CursoId,
                Nombre = cursoInput.Nombre,
                CuposDisponibles = cursoInput.CuposDisponibles
            };
            return curso;
        }
     
    }
}

