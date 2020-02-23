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
    public class PalestranteController : ControllerBase
    {
        public IProAgilRepository _repo { get; }
        public PalestranteController(IProAgilRepository repo)
        {
            this._repo = repo;
        }
        
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var results = await _repo.GetAllPalestranteAsyncTodos(false);
            return Ok(results);
        }
        catch (System.Exception)
        {
            return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
        }
    }
      [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
           {
               var results = await _repo.GetAllPalestranteAsync(Id,true);
               return Ok (results);
           }
           catch (System.Exception)
           {
                return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
           }
        }

         [HttpGet("getByNome/{Nome}")]
        public async Task<IActionResult> Get(string Nome)
        {
            try
           {
               var results = await _repo.GetAllPalestranteAsyncByName(Nome,true);
               return Ok (results);
           }
           catch (System.Exception)
           {
                return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
           }
        }
          [HttpPost]
        public  async Task<IActionResult> Post(Palestrante model)
        {
           try
           {
               _repo.Add(model);
               if(await _repo.SaveChangesAsync())
               {
                   return Created($"/api/palestrante/{model.Id}",model);
               }
           }
           catch (System.Exception)
           {
                return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
           }
           return BadRequest();
        }

           [HttpPut]
        public  async Task<IActionResult> Put(int PalestranteId,Palestrante model)
        {
           try
           {
               var palestrante = await _repo.GetAllPalestranteAsync(PalestranteId,false);
               if(palestrante == null)
               {
                    return NotFound();
               }

               _repo.Update(model);

               if(await _repo.SaveChangesAsync())
               {
                   return Created($"/api/palestrante/{model.Id}",model);
               }
           }
           catch (System.Exception)
           {
                return this.StatusCode(StatusCodes.Status501NotImplemented,Constantes.BancoDadosFalhou);
           }
           return BadRequest();
        }
           [HttpDelete]
        public  async Task<IActionResult> Delete(int PalestranteId)
        {
           try
           {
               var palestrante = await _repo.GetAllPalestranteAsync(PalestranteId,false);
               if(palestrante == null)
               {
                    return NotFound();
               }

               _repo.Delete(palestrante);

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