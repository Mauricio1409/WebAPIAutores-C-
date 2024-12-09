using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using WebAPIAutores.Models;

namespace WebAPIAutores.Controllers
{
    [ApiController]
    [Route("api/Autores")]
    public class AutoresController : Controller
    {
        [HttpGet]
        public ActionResult<List<Autor>> Get()
        {
            return new List<Autor>
            {
                new Autor() {Id = 1, Nombre = "Mauricio"},
                new Autor() {Id = 2, Nombre = "Ivan" }
            };
        }
        
        
    }
}
