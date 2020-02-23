using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Util;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo ;
        public EventoController(IProAgilRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public  async Task<IActionResult> Get()
        {
           try
           {
               var results = await _repo.GetAllEventoAsync(true);
               return Ok (results);
           }
           catch (System.Exception)
           {
                return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
           }
        }

        // GET api/values/5
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
           {
               var results = await _repo.GetAllEventoAsyncById(Id,true);
               return Ok (results);
           }
           catch (System.Exception)
           {
                return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
           }
        }
         [HttpGet("getByTema/{Tema}")]
        public async Task<IActionResult> Get(string Tema)
        {
            try
           {
               var results = await _repo.GetAllEventoAsyncByTema(Tema,true);
               return Ok (results);
           }
           catch (System.Exception)
           {
                return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
           }
        }
         [HttpPost]
        public  async Task<IActionResult> Post(Evento model)
        {
           try
           {
               _repo.Add(model);
               if(await _repo.SaveChangesAsync())
               {
                   return Created($"/api/evento/{model.Id}",model);
               }
           }
           catch (System.Exception)
           {
                return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
           }
           return BadRequest();
        }

          [HttpPut]
        public  async Task<IActionResult> Put(int EventoId,Evento model)
        {
           try
           {
               var evento = await _repo.GetAllEventoAsyncById(EventoId,false);
               if(evento == null)
               {
                    return NotFound();
               }

               _repo.Update(model);

               if(await _repo.SaveChangesAsync())
               {
                   return Created($"/api/evento/{model.Id}",model);
               }
           }
           catch (System.Exception)
           {
                return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
           }
           return BadRequest();
        }
           [HttpDelete]
        public  async Task<IActionResult> Delete(int EventoId)
        {
           try
           {
               var evento = await _repo.GetAllEventoAsyncById(EventoId,false);
               if(evento == null)
               {
                    return NotFound();
               }

               _repo.Delete(evento);

               if(await _repo.SaveChangesAsync())
               {
                   return Ok();
               }
           }
           catch (System.Exception)
           {
                return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
           }
           return BadRequest();
        }
    }
    
}