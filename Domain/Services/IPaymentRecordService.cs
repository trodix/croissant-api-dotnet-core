using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Services.Communication;
using CroissantApi.Models;

namespace CroissantApi.Domain.Services
{
    public interface IPaymentRecordService
    {
         Task<IEnumerable<PaymentRecord>> ListAsync();
         Task<PaymentRecord> FindAsync(int id);
         Task<PaymentRecordResponse> SaveAsync(PaymentRecord paymentRecord);
         Task<PaymentRecordResponse> UpdateAsync(int id, PaymentRecord paymentRecord);
         Task<PaymentRecordResponse> DeleteAsync(int id);
    }
}