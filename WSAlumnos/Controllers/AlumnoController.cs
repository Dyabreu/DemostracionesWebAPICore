using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WSAlumnos.Models;

namespace WSAlumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private List<Alumno> Listado()
        {
            List<Alumno> alumnos = new List<Alumno>()
            {
                new Alumno() { Id = 1, Nombre = "Robert", Apellido = "Lopez" },
                new Alumno() { Id = 2, Nombre = "Pedro", Apellido = "Rubio "},
                new Alumno() {Id = 3, Nombre = "Roberto", Apellido = "Pardo" }
            };
            return alumnos;
        }

        //GEET api/Alumno

        [HttpGet]
        public IEnumerable<Alumno> Get() 
        { 
            return Listado();
        }

        //GET api/Alumno/3
        [HttpGet ("{id}")]
        public ActionResult<Alumno> GetById(int id)
        {
            Alumno alumno = (from a in Listado()
                             where a.Id == id
                             select a).SingleOrDefault();

            return alumno;
        }

    }
}
