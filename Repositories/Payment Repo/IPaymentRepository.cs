namespace Hostel_Management.Repositories.Payment_Repo
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAllPayments();
        IEnumerable<Payment> GetPaymentsByReservationId(int reservationId);
        Payment GetPaymentById(int paymentId);
        void AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int paymentId);
        void SaveChanges();
    }

}
