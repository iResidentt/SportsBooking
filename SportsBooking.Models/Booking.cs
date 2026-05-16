namespace SportsBooking.Models;

public class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SportsGroundId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int StatusId { get; set; }

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

    public User? User { get; set; }
    public SportsGround? SportsGround { get; set; }
    public BookingStatus? Status { get; set; }
}
