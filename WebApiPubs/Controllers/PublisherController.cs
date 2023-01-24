using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPubs.Models;
using System.Linq;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        //Inyeccion de dependencia
        //propiedad
        private readonly pubsContext context;

        //constructor
        public PublisherController(pubsContext context)
        {
            this.context = context;
        }
        //Inyeccion de dependencia--fin

        //Traer todos

        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> Get()
        {
            return context.Publishers.ToList();
        }


        //Traer 1 por Id
        [HttpGet("{id}")]
        public ActionResult<Publisher> GetbyId(string id)
        {
            Publisher publisher = (from p in context.Publishers
                                    where p.PubId == id
                                    select p).SingleOrDefault();
            return publisher;
        }

        //Insertar

        [HttpPost]
        public ActionResult Post(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Publishers.Add(publisher);
            context.SaveChanges();
            return Ok();
        }

        //Modificar
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }
            context.Entry(publisher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return NoContent();
        }


        //DELETE
        [HttpDelete("{id}")]
        public ActionResult<Publisher> Delete(string id)
        {
            var habitacion = (from h in context.Publishers
                              where h.PubId == id
                              select h).SingleOrDefault();
            if (habitacion == null)
            {
                return NotFound();
            }
            context.Publishers.Remove(habitacion);
            context.SaveChanges();
            return habitacion;
        }

    }
}