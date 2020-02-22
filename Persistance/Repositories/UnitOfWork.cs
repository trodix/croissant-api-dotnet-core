using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Persistence.Context;

namespace CroissantApi.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CroissantContext _context;

        public UnitOfWork(CroissantContext context)
        {
            _context = context;     
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}