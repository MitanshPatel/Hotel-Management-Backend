using Hostel_Management.Data;

namespace Hostel_Management.Repositories.Payment_Repo
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public IEnumerable<Payment> GetPaymentsByReservationId(int reservationId)
        {
            return _context.Payments.Where(p => p.ReservationId == reservationId).ToList();
        }

        public Payment GetPaymentById(int paymentId)
        {
            return _context.Payments.SingleOrDefault(p => p.PaymentId == paymentId);
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
        }

        public void UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
        }

        public void DeletePayment(int paymentId)
        {
            var payment = _context.Payments.SingleOrDefault(p => p.PaymentId == paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

}
