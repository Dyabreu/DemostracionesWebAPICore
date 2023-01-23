using Microsoft.EntityFrameworkCore;
using WebApiLibros.Models;

namespace WebApiLibros.Data
{
    public class DBLibrosContext:DbContext
    {
        //constructor
        public DBLibrosContext(DbContextOptions<DBLibrosContext> options) : base(options) { }
        //Propiedades

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
