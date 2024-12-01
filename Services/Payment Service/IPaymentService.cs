namespace Hostel_Management.Services.Payment_Service
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetAllPayments();
        IEnumerable<Payment> GetPaymentsByReservationId(int reservationId);
        Payment GetPaymentById(int paymentId);
        string AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int paymentId);
        void UpdatePaymentStatus(int paymentId, string status);
    }

}
