using CroissantApi.Persistence.Context;

namespace CroissantApi.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly CroissantContext _context;

        public BaseRepository(CroissantContext context)
        {
            _context = context;
        }
    }
}