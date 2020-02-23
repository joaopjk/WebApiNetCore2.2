using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
         public DataContext _dataContext { get; }
        public ProAgilRepository(DataContext _dataContext) 
        {
            this._dataContext = _dataContext;
               
        }
        public void Add<T>(T entity) where T : class
        {
            _dataContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dataContext.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _dataContext.Update(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await( _dataContext.SaveChangesAsync()) > 0;
        }
        //Eventos        
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false )
        {
           IQueryable<Evento> query = _dataContext.Eventos
           .Include(c => c.Lotes)
           .Include(c => c.RedeSociais);
           if(includePalestrante == true)
           {
            query.Include(p => p.PalestranteEvento)
            .ThenInclude(p => p.Palestrante);
           }
           query = query.AsNoTracking()
           .OrderByDescending(c => c.DataEvento);
           return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _dataContext.Eventos
           .Include(c => c.Lotes)
           .Include(c => c.RedeSociais);
           if(includePalestrante)
           {
            query.Include(p => p.PalestranteEvento)
            .ThenInclude(p => p.Palestrante);
           }
           query = query.OrderByDescending(c => c.DataEvento)
           .Where( c => c.Tema.ToLower().Contains(tema.ToLower()));
           return await query.ToArrayAsync();
        }
        public async Task<Evento> GetAllEventoAsyncById(int eventoId, bool includePalestrante = false )
        {
            IQueryable<Evento> query = _dataContext.Eventos
           .Include(c => c.Lotes)
           .Include(c => c.RedeSociais);
           if(includePalestrante)
           {
            query.Include(p => p.PalestranteEvento)
                .ThenInclude(p => p.Palestrante);
           }
           query = query.OrderByDescending(c => c.DataEvento)
           .Where( c => c.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        //Palestrantes
        public async Task<Palestrante[]> GetAllPalestranteAsyncByTema(bool includeEventos = false )
        {
            IQueryable<Palestrante> query = _dataContext.Palestrantes
           .Include(c => c.RedesSociais);
           if(includeEventos)
           {
            query.Include(p => p.PalestrantesEvento)
                .ThenInclude(p => p.Evento);
           }
           query = query.OrderBy(c => c.Nome);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestranteAsync(int PalestranteId, bool includeEventos = false )
        {
         IQueryable<Palestrante> query = _dataContext.Palestrantes
           .Include(c => c.RedesSociais);
           if(includeEventos)
           {
            query.Include(p => p.PalestrantesEvento)
                .ThenInclude(p => p.Evento);
           }
           query = query.OrderBy(c => c.Nome)
           .Where(p => p.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async  Task<Palestrante[]> GetAllPalestranteAsyncByName(string PalestranteName, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _dataContext.Palestrantes
           .Include(c => c.RedesSociais);
           if(includeEventos)
           {
            query.Include(p => p.PalestrantesEvento)
                .ThenInclude(p => p.Evento);
           }
           query = query.Where(p => p.Nome.ToLower().Contains(PalestranteName.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncTodos( bool includeEventos)
        {
             IQueryable<Palestrante> query = _dataContext.Palestrantes
           .Include(c => c.RedesSociais);
           if(includeEventos)
           {
            query.Include(p => p.PalestrantesEvento)
                .ThenInclude(p => p.Evento);
           }
           return await query.ToArrayAsync();
        }
    }
}