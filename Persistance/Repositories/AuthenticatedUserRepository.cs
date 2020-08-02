using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Models;
using CroissantApi.Persistence.Context;

namespace CroissantApi.Persistence.Repositories
{
    public class AuthenticatedUserRepository : BaseRepository, IAuthenticatedUserRepository
    {
        public AuthenticatedUserRepository(CroissantContext context) : base(context)
        {
        }

        public async Task<AuthenticatedUser> FindByCredentialsAsync(string username, string password)
        {
            // TODO
            throw new NotImplementedException();
            // return await _context.AuthenticatedUsers.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
        }

        public async Task<AuthenticatedUser> FindByIdAsync(int id)
        {
            return await _context.AuthenticatedUsers.FindAsync(id);
        }

        public async Task<AuthenticatedUser> FindByRefreshTokenAsync(string username, string password)
        {
            // TODO
            throw new NotImplementedException();
            // return await _context.AuthenticatedUsers.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
        }

        public async Task<IEnumerable<AuthenticatedUser>> ListAsync()
        {
            // TODO
            throw new NotImplementedException();
            // return await _context.AuthenticatedUsers
            //     .AsNoTracking()
            //     .ToListAsync();
        }

        public void Update(AuthenticatedUser authenticatedUser)
        {
            _context.AuthenticatedUsers.Update(authenticatedUser);
        }
    }
}