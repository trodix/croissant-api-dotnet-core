using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Domain.Services;
using CroissantApi.Domain.Services.Communication;
using CroissantApi.Models;

namespace CroissantApi.Services
{
    public class PaymentRecordService : IPaymentRecordService
    {
        private readonly IPaymentRecordRepository _paymentRecordRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentRecordService(IPaymentRecordRepository paymentRecordRepository, IUnitOfWork unitOfWork)
        {
            this._paymentRecordRepository = paymentRecordRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PaymentRecord>> ListAsync()
        {
            return await _paymentRecordRepository.ListAsync();
        }

        public async Task<PaymentRecord> FindAsync(int id)
        {
            return await _paymentRecordRepository.FindByIdAsync(id);
        }

        public async Task<PaymentRecordResponse> SaveAsync(PaymentRecord paymentRecord)
        {
            try
            {
                await _paymentRecordRepository.AddAsync(paymentRecord);
                await _unitOfWork.CompleteAsync();

                return new PaymentRecordResponse(paymentRecord);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PaymentRecordResponse($"An error occurred when saving the paymentRecord: {ex.Message}");
            }
        }

        public async Task<PaymentRecordResponse> UpdateAsync(int id, PaymentRecord paymentRecord)
        {
            var existingPaymentRecord = await _paymentRecordRepository.FindByIdAsync(id);

            if (existingPaymentRecord == null)
            {
                return new PaymentRecordResponse("PaymentRecord not found.");
            }

            existingPaymentRecord.PayedAt = paymentRecord.PayedAt;

            try
            {
                _paymentRecordRepository.Update(existingPaymentRecord);
                await _unitOfWork.CompleteAsync();

                return new PaymentRecordResponse(existingPaymentRecord);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PaymentRecordResponse($"An error occurred when updating the paymentRecord: {ex.Message}");
            }
        }

        public async Task<PaymentRecordResponse> DeleteAsync(int id)
        {
            var existingPaymentRecord = await _paymentRecordRepository.FindByIdAsync(id);

            if (existingPaymentRecord == null)
                return new PaymentRecordResponse("PaymentRecord not found.");

            try
            {
                _paymentRecordRepository.Remove(existingPaymentRecord);
                await _unitOfWork.CompleteAsync();

                return new PaymentRecordResponse(existingPaymentRecord);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PaymentRecordResponse($"An error occurred when deleting the paymentRecord: {ex.Message}");
            }
        }
    }
}