namespace SportsBooking.Models;

public class GroundType
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<SportsGround> SportsGrounds { get; set; }
        = new List<SportsGround>();
}
