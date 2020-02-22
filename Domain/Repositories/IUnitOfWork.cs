using System.Threading.Tasks;

namespace CroissantApi.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}