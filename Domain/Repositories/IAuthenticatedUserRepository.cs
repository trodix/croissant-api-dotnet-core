using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Models;

namespace CroissantApi.Domain.Repositories
{
    public interface IAuthenticatedUserRepository
    {
        Task<IEnumerable<AuthenticatedUser>> ListAsync();
        Task<AuthenticatedUser> FindByCredentialsAsync(string username, string password);
        Task<AuthenticatedUser> FindByRefreshTokenAsync(string username, string password);
        Task<AuthenticatedUser> FindByIdAsync(int id);
        void Update(AuthenticatedUser authenticatedUser);
    }
}