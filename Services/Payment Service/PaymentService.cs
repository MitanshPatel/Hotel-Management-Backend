using Hostel_Management.Repositories.Payment_Repo;

namespace Hostel_Management.Services.Payment_Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;

        public PaymentService(IPaymentRepository paymentRepository, IReservationRepository reservationRepository)
        {
            _paymentRepository = paymentRepository;
            _reservationRepository = reservationRepository;
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _paymentRepository.GetAllPayments();
        }

        public IEnumerable<Payment> GetPaymentsByReservationId(int reservationId)
        {
            return _paymentRepository.GetPaymentsByReservationId(reservationId);
        }

        public Payment GetPaymentById(int paymentId)
        {
            return _paymentRepository.GetPaymentById(paymentId);
        }

        public string AddPayment(Payment payment)
        {
            // Get the reservation associated with the payment
            var reservation = _reservationRepository.GetReservationById(payment.ReservationId);

            // Check if the reservation status is "Approved"
            if (reservation != null && reservation.Status == "Approved")
            {
                var existingPayments = _paymentRepository.GetPaymentsByReservationId(payment.ReservationId)
                                                 .Where(p => p.PaymentFor == "Booking" && p.Status == "Successful");
                if (existingPayments.Any())
                {
                    return "Payment cannot be processed because a payment for the room has already been made.";
                }
                payment.Status = "Successful";
                _paymentRepository.AddPayment(payment);
                _paymentRepository.SaveChanges();

                // Update reservation payment status
                reservation.PaymentStatus = payment.Status;
                _reservationRepository.UpdateReservation(reservation);
                _reservationRepository.SaveChanges();
                return "Successful";
            }
            else
            {
                // Handle the case where the reservation is not approved
                return "Payment cannot be processed because the reservation is not approved.";
            }
        }


        public void UpdatePayment(Payment payment)
        {
            _paymentRepository.UpdatePayment(payment);
            _paymentRepository.SaveChanges();

            // Update reservation payment status
            var reservation = _reservationRepository.GetReservationById(payment.ReservationId);
            if (reservation != null)
            {
                reservation.PaymentStatus = payment.Status;
                _reservationRepository.UpdateReservation(reservation);
                _reservationRepository.SaveChanges();
            }
        }

        public void DeletePayment(int paymentId)
        {
            var payment = _paymentRepository.GetPaymentById(paymentId);
            if (payment != null)
            {
                _paymentRepository.DeletePayment(paymentId);
                _paymentRepository.SaveChanges();

                // Update reservation payment status
                var reservation = _reservationRepository.GetReservationById(payment.ReservationId);
                if (reservation != null)
                {
                    reservation.PaymentStatus = "Pending";
                    _reservationRepository.UpdateReservation(reservation);
                    _reservationRepository.SaveChanges();
                }
            }
        }

        public void UpdatePaymentStatus(int paymentId, string status)
        {
            var payment = _paymentRepository.GetPaymentById(paymentId);
            if (payment != null)
            {
                payment.Status = status;
                _paymentRepository.UpdatePayment(payment);
                _paymentRepository.SaveChanges();

                // Update reservation payment status
                var reservation = _reservationRepository.GetReservationById(payment.ReservationId);
                if (reservation != null)
                {
                    reservation.PaymentStatus = status;
                    _reservationRepository.UpdateReservation(reservation);
                    _reservationRepository.SaveChanges();
                }
            }
        }
    }

}
