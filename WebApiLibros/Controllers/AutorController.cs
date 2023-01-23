using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiLibros.Data;
using WebApiLibros.Models;

namespace WebApiLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        //Inyeccion de dependencia
        //propiedad
        private readonly DBLibrosContext context;
        //constructor
        public AutorController(DBLibrosContext context)
        {
            this.context = context;
        }

        //Inyeccion de dependencia--fin
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get() 
        {
            return context.Autores.ToList();
        }

        //GET api/autor/5
        [HttpGet("{id}")]
        public ActionResult<Autor> GetbyId(int id) 
        {
            Autor autor = (from a in context.Autores
                           where a.IdAutor == id
                           select a).SingleOrDefault();
            return autor;
        }

        [HttpGet("listado/{edad}")]
        public ActionResult<IEnumerable<Autor>> GetEdad(int edad)
        {
            List<Autor> autores = (from a in context.Autores
                                   where a.Edad == edad
                                   select a).ToList();
            return autores;
        }



        //Insertar
        //POst api/autor

        [HttpPost]
        public ActionResult Post(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Autores.Add(autor);
            context.SaveChanges();
            return Ok();
        }

        //Update
        //PUt api/autor/2
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor autor)
        {
            if (id!=autor.IdAutor) 
            {
                return BadRequest();
            }
            context.Entry(autor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();  
            return Ok();
        }

        //DELETE api/autor/1
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = (from a in context.Autores
                         where a.IdAutor == id
                         select a).SingleOrDefault();
            if (autor == null) 
            { 
                return NotFound();
            }
            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }


    }
}
