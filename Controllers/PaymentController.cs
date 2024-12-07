using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hostel_Management.Models;
using Hostel_Management.Services;
using Hostel_Management.Services.Payment_Service;

namespace Hostel_Management.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize(Roles = "Admin, Manager, Receptionist")]
        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = _paymentService.GetAllPayments();
            return Ok(payments);
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("reservation/{reservationId}")]
        public IActionResult GetPaymentsByReservationId(int reservationId)
        {
            var payments = _paymentService.GetPaymentsByReservationId(reservationId);
            return Ok(payments);
        }

        [Authorize(Roles = "Admin, Manager, Receptionist")]
        [HttpGet("{paymentId}")]
        public IActionResult GetPaymentById(int paymentId)
        {
            var payment = _paymentService.GetPaymentById(paymentId);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [Authorize(Roles = "Guest")]
        [HttpPost]
        public IActionResult AddPayment([FromBody] Payment payment)
        {
            var status = _paymentService.AddPayment(payment);
            CreatedAtAction(nameof(GetPaymentById), new { paymentId = payment.PaymentId }, payment);
            return Ok(status);
        }

        [Authorize(Roles = "Admin, Manager, Receptionist")]
        [HttpPut("{paymentId}")]
        public IActionResult UpdatePayment(int paymentId, [FromBody] Payment payment)
        {
            if (paymentId != payment.PaymentId)
            {
                return BadRequest();
            }

            _paymentService.UpdatePayment(payment);
            return Ok(new { msg = "Payment updated." });
        }

        [Authorize(Roles = "Admin, Manager, Receptionist")]
        [HttpDelete("{paymentId}")]
        public IActionResult DeletePayment(int paymentId)
        {
            _paymentService.DeletePayment(paymentId);
            return Ok(new { msg = "Payment deleted." });
        }

        [Authorize(Roles = "Admin, Manager, Receptionist")]
        [HttpPut("{paymentId}/status")]
        public IActionResult UpdatePaymentStatus(int paymentId, [FromBody] string status)
        {
            _paymentService.UpdatePaymentStatus(paymentId, status);
            return Ok(new { msg = "Payment status updated." });
        }
    }
}
