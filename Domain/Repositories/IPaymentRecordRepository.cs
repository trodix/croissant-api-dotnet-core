using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Models;

namespace CroissantApi.Domain.Repositories
{
    public interface IPaymentRecordRepository
    {
        Task<IEnumerable<PaymentRecord>> ListAsync();
        Task<PaymentRecord> FindByIdAsync(int id);
        // TODO
        // Task<IEnumerable<PaymentRecord>> FindByUserIdAsync(int userId);
        // Task<IEnumerable<PaymentRecord>> FindByRuleIdAsync(int ruleId);
        // Task<IEnumerable<PaymentRecord>> FindByUserRuleIdAsync(int userRuleId);
        Task AddAsync(PaymentRecord paymentRecord);
        void Update(PaymentRecord paymentRecord);
        void Remove(PaymentRecord paymentRecord);
    }
}