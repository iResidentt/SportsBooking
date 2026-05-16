using Xunit;
using Microsoft.EntityFrameworkCore;
using SportsBooking.Data;
using SportsBooking.Models;

namespace SportsBooking.Tests;

public class SportsGroundTests
{
    [Fact]
    public async Task Can_Add_SportsGround()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        using var context = new AppDbContext(options);

        var ground = new SportsGround
        {
            Name = "Тестовое поле",
            Description = "Описание",
            Address = "Тестовый адрес",
            PricePerHour = 1000,
            ImagePath = "test.jpg",
            IsAvailable = true
        };

        context.SportsGrounds.Add(ground);
        await context.SaveChangesAsync();

        Assert.Equal(1, context.SportsGrounds.Count());
    }
}
