namespace SportsBooking.Models;

public class SportsGround
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public decimal PricePerHour { get; set; }

    public int GroundTypeId { get; set; }

    public string ImagePath { get; set; } = string.Empty;

    public bool IsAvailable { get; set; } = true;

    public GroundType? GroundType { get; set; }

    public ICollection<Booking> Bookings { get; set; }
        = new List<Booking>();
}