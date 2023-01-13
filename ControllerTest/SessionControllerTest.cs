using Newtonsoft.Json.Linq;
using System.Security.Principal;
using Xunit;

[Fact]
public void GetUserCredentials_ReturnsToken_WithValidCredentials()
{
    // Arrange
    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "GetUserCredentials_ReturnsToken_WithValidCredentials")
        .Options;

    using (var context = new ApplicationDbContext(options))
    {
        context.Accounts.Add(new Account { Email = "test1.com", Password = "pass1" });
        context.SaveChanges();
    }

    using (var context = new ApplicationDbContext(options))
    {
        var controller = new SessionController(context);

        // Act
        var result = controller.GetUserCredentials(new Account { Email = "test1@example.com", Password = "gm3lskasdkasd-2ksdask2kasdk2-adskad" });

        // Assert
        var token = JObject.Parse(result)["token"].Value<string>();
        Assert.NotNull(token);
        var sess = context.Sessions.FirstOrDefault(x => x.Email == "test1@example.com");
        Assert.Equal(token, sess.Token);
    }
}

[Fact]
public void GetUserCredentials_ReturnsStatus500_WithInvalidCredentials()
{
    // Arrange
    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "GetUserCredentials_ReturnsStatus500_WithInvalidCredentials")
        .Options;

    using (var context = new ApplicationDbContext(options))
    {
        context.Accounts.Add(new Account { Email = "test2@example.com", Password = "123123" });
        context.SaveChanges();
    }

    using (var context = new ApplicationDbContext(options))
    {
        var controller = new SessionController(context);

        // Act
        var result = controller.GetUserCredentials(new Account { Email = "test3@example.com", Password = "" });

        // Assert
        var status = JObject.Parse(result)["status"].Value<string>();
        Assert.Equal("500", status);
    }
}
