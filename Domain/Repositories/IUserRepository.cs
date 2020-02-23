using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Models;

namespace CroissantApi.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task<User> FindByIdAsync(int id);
        Task AddAsync(User user);
        void Update(User user);
        void Remove(User user);
    }
}