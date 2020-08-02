using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Models;
using CroissantApi.Resources;

namespace CroissantApi.Services
{
    public interface IAuthenticatedUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        bool RevokeToken(string token, string ipAddress);
        IEnumerable<AuthenticatedUser> List();
        AuthenticatedUser Find(int id);
    }
}

    