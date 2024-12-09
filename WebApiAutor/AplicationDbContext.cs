using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiAutor.Models;

namespace WebApiAutor
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
