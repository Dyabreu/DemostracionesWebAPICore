using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiLibros.Models;
using WebApiLibros.Data;

namespace WebApiLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        //Inyeccion de dependencia
        //propiedad
        private readonly DBLibrosContext context;

        //constructor
        public LibroController(DBLibrosContext context)
        {
            this.context = context;
        }
        //Inyeccion de dependencia--fin



        //Traer todos

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.ToList();
        }


        //Traer 1 por Id
        [HttpGet("{id}")]
        public ActionResult<Libro> GetbyId(int id)
        {
            Libro libro = (from l in context.Libros
                           where l.Id == id
                           select l).SingleOrDefault();
            return libro;
        }


        //Traer todos por AutorId

        [HttpGet("listado/{autorId}")]
        public ActionResult<IEnumerable<Libro>> GetEdad(int autorId)
        {
            List<Libro> libros = (from l in context.Libros
                                  where l.AutorId == autorId
                                  select l).ToList();
                                   
            return libros;
        }

        //Insertar

        [HttpPost]
        public ActionResult Post(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Libros.Add(libro);
            context.SaveChanges();
            return Ok();
        }

        //Modificar
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }
            context.Entry(libro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return NoContent();
        }


        //DELETE
        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = (from l in context.Libros
                         where l.Id == id
                         select l).SingleOrDefault();
            if (libro == null)
            {
                return NotFound();
            }
            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }


    }
}
