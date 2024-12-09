using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutor.Models;

namespace WebApiAutor.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public AutoresController(AplicationDbContext context)
        {
            this._context = context;
        }

        // Obtener todos los autores
        [HttpGet]
        public async Task<ActionResult<List<Autor>>> ObtenerAutores()
        {
            return await _context.Autores.Include(x => x.Libros).ToListAsync();
        }

       

        // Agregar un nuevo autor
        [HttpPost]
        public async Task<ActionResult> AgregarAutor(Autor autor)
        {
            _context.Add(autor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerAutores), new { id = autor.Id }, autor);
        }


        // Actualizar un autor existente
        [HttpPut("{id:int}")]
        public async Task<ActionResult> ActualizarAutor(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id ingresado");
            }

            // Actualiza el autor completo
            _context.Update(autor); // Esto marcará el autor como 'Modified'
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> EliminarAutor(int Id)
        {
            var existe = await _context.Autores.AnyAsync(x => x.Id == Id);
            if (!existe)
            {
                return NotFound("El id ingresado no existe");
            }
            _context.Remove(new Autor() { Id = Id });
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
