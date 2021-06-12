using System.Linq;
using System.Reflection;
using System.Resources;
using Microsoft.AspNetCore.Mvc;
using Logica;
using Datos;
using Entidad;

using System.Collections.Generic;
using parcial2dotnet.Models;

namespace PagoTercero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsncripcionController : ControllerBase
    {
        private InscripcionService inscripcionService;
        public InsncripcionController(Parcial2Context context)
        {
            inscripcionService = new InscripcionService(context);
        }

        [HttpPost]
        public ActionResult<InscripcionViewModel> PostInscripcion(InscripcionInputModel inscripcionInput)
        {
            var inscripcion = MapearInscripcion(inscripcionInput);
            var response = inscripcionService.GuardarInscripcion(inscripcion);
            if (!response.Error)
            {
                var InscripcionViewModel = new InscripcionViewModel(inscripcion);
                return Ok(InscripcionViewModel);
            }
            return BadRequest(response.Mensaje);
        }


        [HttpGet]
        public ActionResult<IEnumerable<InscripcionViewModel>> GetInscripciones()
        {
            var response = inscripcionService.ConsultarTodos();
            if (!response.Error)
            {
                var InscripcionViewModel = response.Inscripcions.Select(p => new InscripcionViewModel(p));
                return Ok(InscripcionViewModel);
            }
            return BadRequest(response.Mensaje);
        }

        private Inscripcion MapearInscripcion(InscripcionInputModel inscripcionInput)
        {
            var inscripcion = new Inscripcion()
            {
                InscripcionId = inscripcionInput.InscripcionId,
                CursoId = inscripcionInput.Curso.CursoId,
                TipoIdentificacion = inscripcionInput.TipoIdentificacion,
                NumeroIdentificacion = inscripcionInput.NumeroIdentificacion,
                Curso = new Curso() { CursoId = inscripcionInput.Curso.CursoId, Nombre = inscripcionInput.Curso.Nombre, CuposDisponibles = inscripcionInput.Curso.CuposDisponibles },
                Nombre = inscripcionInput.Nombre,
                FechaNacimiento = inscripcionInput.FechaNacimiento,
            };
            return inscripcion;
        }



    }
}