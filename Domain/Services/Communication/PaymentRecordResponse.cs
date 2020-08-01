using CroissantApi.Models;

namespace CroissantApi.Domain.Services.Communication
{
    public class PaymentRecordResponse : BaseResponse
    {
        public PaymentRecord PaymentRecord { get; private set; }

        private PaymentRecordResponse(bool success, string message, PaymentRecord paymentRecord) : base(success, message)
        {
            PaymentRecord = paymentRecord;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="paymentRecord">Saved paymentRecord.</param>
        /// <returns>Response.</returns>
        public PaymentRecordResponse(PaymentRecord paymentRecord) : this(true, string.Empty, paymentRecord)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public PaymentRecordResponse(string message) : this(false, message, null)
        { }
    }
}