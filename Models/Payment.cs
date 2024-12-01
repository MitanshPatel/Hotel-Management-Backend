public class Payment
{
    public int PaymentId { get; set; }
    public int ReservationId { get; set; }
    public decimal Amount { get; set; }
    public required string PaymentMethod { get; set; } // e.g., "Card", "Cash", "Online"
    public required string PaymentFor { get; set; } // e.g., "Booking", "Food + {}", "Services + {}"
    public DateTime PaymentDate { get; set; }
    public required string Status { get; set; } // e.g., "Pending", "Completed", "Failed"
}
