using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly pubsContext context;

        public StoreController(pubsContext context)
        {
            this.context = context;
        }


        //Get
        [HttpGet]
        public ActionResult <IEnumerable<Store>> Get()
        {
            return context.Stores.ToList();
        }

        //Get by Id

        [HttpGet("{id}")]
        public ActionResult <Store> GetbyId(string id)
        {
            Store store = (from s in context.Stores
                           where s.StorId == id
                           select s).SingleOrDefault();
            return store;
        }


        //Post
        [HttpPost]
        public ActionResult Post (Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Stores.Add(store);
            context.SaveChanges();
            return Ok();
        }


        //Put
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Store store) 
        {
            if (id != store.StorId)
            {
                return BadRequest();
            }
            context.Entry(store).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return Ok();
            }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult<Store> Delete(string id)
        {
            var store = (from s in context.Stores
                         where s.StorId == id
                         select s).SingleOrDefault();
            if (store == null)
            {
                return NotFound();
            }
            context.Stores.Remove(store);
            context.SaveChanges();
            return store;
        }


        //GetbyName
        [HttpGet("nombre/{name}")]

        public ActionResult<IEnumerable<Store>> GetbyName (string name)
        {
            List<Store> stores = (from s in context.Stores
                                  where s.StorName== name
                                  select s).ToList();
            return stores;
        }

        [HttpGet("zip/{zip}")]
        public ActionResult<IEnumerable<Store>> GetbyZip(string zip)
        {
            List<Store> stores = (from s in context.Stores
                                  where s.Zip== zip
                                  select s).ToList();
            return stores;
        }

        [HttpGet("ubicacion/{city}/{state}")]
        public ActionResult<IEnumerable<Store>> GetbyCityState(string city, string state)
        {
            List<Store> stores = (from s in context.Stores
                                  where s.City==city
                                  where s.State==state
                                  select s).ToList();
            return stores;
        }
    }
}
