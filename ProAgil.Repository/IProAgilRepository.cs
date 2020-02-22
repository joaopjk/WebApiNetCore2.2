using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;

         Task<bool> SaveChangesAsync();

         //Eventos
         Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrante);
         Task<Evento[]> GetAllEventoAsync( bool includePalestrante);
         Task<Evento[]> GetAllEventoAsyncByTema(int eventoId, bool includePalestrante);
        //Palestrante
        Task<Evento[]> GetAllPalestranteAsyncByTema( bool includePalestrante);   
        Task<Evento[]> GetAllPalestranteAsync( int PalestranteId,bool includePalestrante);
    }
}