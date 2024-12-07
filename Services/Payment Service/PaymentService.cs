using Hostel_Management.Models;
using Hostel_Management.Repositories.Payment_Repo;
using Hostel_Management.Repositories.Service_Repo;

namespace Hostel_Management.Services.Payment_Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IServiceRepository _serviceRepository;

        public PaymentService(IPaymentRepository paymentRepository, IReservationRepository reservationRepository, IServiceRepository serviceRepository)
        {
            _paymentRepository = paymentRepository;
            _reservationRepository = reservationRepository;
            _serviceRepository = serviceRepository;
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

        public object AddPayment(Payment payment)
        {
            // Get the reservation associated with the payment
            var reservation = _reservationRepository.GetReservationById(payment.ReservationId);

            // Check if the reservation status is "Approved"
            if (reservation != null && reservation.Status == "Approved")
            {
                // Check if a successful payment already exists for the "Booking" service
                if (payment.PaymentFor.StartsWith("Room"))
                {
                    var existingRoomPayments = _paymentRepository.GetPaymentsByReservationId(payment.ReservationId)
                                                                 .Where(p => p.PaymentFor.StartsWith("Booking") && p.Status == "Successful");
                    if (existingRoomPayments.Any())
                    {
                        return new { msg = "A successful payment has already been made for the room for this reservation." };
                    }
                }

                payment.Status = "Successful";
                _paymentRepository.AddPayment(payment);
                _paymentRepository.SaveChanges();

                // Update reservation payment status
                reservation.PaymentStatus = payment.Status;
                _reservationRepository.UpdateReservation(reservation);
                _reservationRepository.SaveChanges();

                // Add service request if payment is for food or other services
                if (payment.PaymentFor.StartsWith("Food") || payment.PaymentFor.StartsWith("Services"))
                {
                    var serviceType = payment.PaymentFor;
                    var service = new Service
                    {
                        ServiceType = serviceType,
                        GuestId = reservation.GuestId,
                        ReservationId = reservation.ReservationId,
                        RoomId = reservation.RoomId,
                        RequestTime = DateTime.Now,
                        Status = "Pending"
                    };
                    _serviceRepository.AddService(service);
                    _serviceRepository.SaveChanges();
                }

                return new { msg = "Payment processed successfully." };
            }
            else
            {
                // Handle the case where the reservation is not approved
                return new { msg = "Payment cannot be processed because the reservation is not approved." };
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
