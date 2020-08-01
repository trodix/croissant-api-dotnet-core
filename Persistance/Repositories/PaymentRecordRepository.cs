using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Models;
using CroissantApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CroissantApi.Persistence.Repositories
{
    public class PaymentRecordRepository : BaseRepository, IPaymentRecordRepository
    {
        public PaymentRecordRepository(CroissantContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PaymentRecord>> ListAsync()
        {
            return await _context.PaymentRecords
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PaymentRecord> FindByIdAsync(int id)
        {
            return await _context.PaymentRecords.FindAsync(id);
        }

        // TODO
        // @See https://entityframeworkcore.com/knowledge-base/42063289/how-to-add-where-clause-to-theninclude
        // public async Task<IEnumerable<PaymentRecord>> FindByUserIdAsync(int userId)
        // {
        //     return await _context.PaymentRecords
        //         .AsNoTracking()
        //         .Include(pr => pr.UserRule)
        //         .ThenInclude(ur => ur.User)
        //         .ToListAsync();
        // }

        // TODO
        // public async Task<IEnumerable<PaymentRecord>> FindByRuleIdAsync(int ruleId)
        // {
        //     // return await _context.PaymentRecords
        //     //     .AsNoTracking()
        //     //     .ToListAsync();
        // }

        // TODO
        // public async Task<IEnumerable<PaymentRecord>> FindByUserRuleIdAsync(int userRuleId)
        // {
        //     // return await _context.PaymentRecords
        //     //     .AsNoTracking()
        //     //     .ToListAsync();
        // }

        public void Update(PaymentRecord paymentRecord)
        {
            _context.PaymentRecords.Update(paymentRecord);
        }

        public async Task AddAsync(PaymentRecord paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
        }

        public void Remove(PaymentRecord paymentRecord)
        {
            _context.PaymentRecords.Remove(paymentRecord);
        }
    }
}