using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Services.Communication;
using CroissantApi.Models;

namespace CroissantApi.Domain.Services
{
    public interface IUserService
    {
         Task<IEnumerable<User>> ListAsync();
         Task<User> FindAsync(int id);
         Task<UserResponse> SaveAsync(User user);
         Task<UserResponse> UpdateAsync(int id, User user);
         Task<UserResponse> IncrementCoinQuantityAsync(int id, int ruleId);
         Task<UserResponse> UpdateNextPaymentDateAsync(int id, int ruleId, DateTime nextPaymentDate);
         Task<UserResponse> DeleteAsync(int id);
    }
}