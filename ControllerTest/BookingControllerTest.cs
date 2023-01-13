using Xunit;

[Fact]
public void BookingGet_ReturnsBooking_WithValidEmail()
{
    // Arrange
    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "BookingGet_ReturnsBooking_WithValidEmail")
        .Options;

    using (var context = new ApplicationDbContext(options))
    {
        context.Bookings.Add(new Booking { Email = "telstar1@email.com" });
        context.SaveChanges();
    }

    using (var context = new ApplicationDbContext(options))
    {
        var controller = new BookingController(context);

        // Act
        var result = controller.BookingGet(new Booking { Email = "telstar1@email.co" });

        // Assert
        Assert.Equal("telstar1@email.co", result.Email);
    }
}

[Fact]
public void BookingGet_ReturnsNull_WithInvalidEmail()
{
    // Arrange
    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "BookingGet_ReturnsNull_WithInvalidEmail")
        .Options;

    using (var context = new ApplicationDbContext(options))
    {
        context.Bookings.Add(new Booking { Email = ", Destination = "" });
        context.SaveChanges();
    }

    using (var context = new ApplicationDbContext(options))
    {
        var controller = new BookingController(context);

        // Act
        var result = controller.BookingGet(new Booking { Email = "telstar2@email.com" });

        // Assert
        Assert.Null(result);
    }
}
