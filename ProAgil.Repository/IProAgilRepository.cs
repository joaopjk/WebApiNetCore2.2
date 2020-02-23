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
         Task<Evento> GetAllEventoAsyncById(int eventoId, bool includePalestrante);
        //Palestrante
        Task<Palestrante[]> GetAllPalestranteAsyncByTema( bool includeEventos);   
        Task<Palestrante> GetAllPalestranteAsync( int PalestranteId,bool includeEventos);
        Task<Palestrante[]> GetAllPalestranteAsyncTodos(bool includeEventos);
        Task<Palestrante[]> GetAllPalestranteAsyncByName( string PalestranteName,bool includeEventos);
    }
}