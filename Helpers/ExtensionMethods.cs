using System.Collections.Generic;
using System.Linq;
using CroissantApi.Models;

namespace CroissantApi.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<AuthenticatedUser> WithoutPasswords(this IEnumerable<AuthenticatedUser> users) 
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static AuthenticatedUser WithoutPassword(this AuthenticatedUser user) 
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}