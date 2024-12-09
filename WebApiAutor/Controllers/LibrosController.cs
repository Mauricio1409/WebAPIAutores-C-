using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutor.Models;

namespace WebApiAutor.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public LibrosController(AplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todos los Libros
        [HttpGet]
        public async Task<ActionResult<List<Libro>>> ObtenerLibros()
        {
            return await _context.Libros.ToListAsync();
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Libro>> BuscarLibro(int Id)
        {
            return await _context.Libros.Include(x=>x.Autor).FirstOrDefaultAsync(x => x.Id == Id);
        }

        // Agregar un nuevo Libro
        [HttpPost]
        public async Task<ActionResult> AgregarLibro(Libro libro)
        {
            var existeAutor = await _context.Autores.AnyAsync(x => x.Id == libro.AutorID);

            if (!existeAutor)
            {
                return BadRequest("El id de autor ingresado no existe");
            }

            _context.Add(libro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerLibros), new { id = libro.Id }, libro);
        }


        // Actualizar un Libro existente
        [HttpPut("{id:int}")]
        public async Task<ActionResult> ActualizarLibro(Libro libro, int id)
        {
            if (libro.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id ingresado");
            }

            // Actualiza el libro completo
            _context.Update(libro); // Esto marcará el libro como 'Modified'
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> EliminarLibro(int Id)
        {
            var existe = await _context.Libros.AnyAsync(x => x.Id == Id);
            if (!existe)
            {
                return NotFound("El id ingresado no existe");
            }
            _context.Remove(new Libro() { Id = Id });
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
