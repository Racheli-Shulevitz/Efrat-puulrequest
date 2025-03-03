using System.Net;
using System.Net.Http.Json;
using TestOurStore;
using Repositories;
using Entities;
using Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestOurStore;
public class IntegrationTests : IClassFixture<DatabaseFixture>
{
    private readonly OurStoreContext _context;
    private readonly Mock<ILogger<OrderService>> _loggerMock;

    public IntegrationTests(DatabaseFixture fixture)
    {
        _context = fixture.Context;
        _loggerMock = new Mock<ILogger<OrderService>>();
    }
    [Fact]
    public async Task LogIn_ValidCredentials_ReturnsUser()
    {
        var user = new User { Email = "test@example.com", Password = "pass123@" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var userRepository = new UserRepository(_context);

        // Act
        var retrievedUser = await userRepository.login(user.Email,user.Password);

        // Assert
        Assert.NotNull(retrievedUser);
        Assert.Equal(user.Email, retrievedUser.Email);
    }

    [Fact]
    public async Task LogIn_InvalidPassword_ReturnsNoContent()
    {
        var user = new User { Email = "test@example.com", Password = "pass123@" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var userRepository = new UserRepository(_context);

        // Act
        var retrievedUser = await userRepository.login(user.Email, "wrongpassword");

        // Assert
        Assert.Null(retrievedUser);
    }

    [Fact]
    public async Task LogIn_NonExistentUser_ReturnsNoContent()
    {
        var userRepository = new UserRepository(_context);

        // Act
        var retrievedUser = await userRepository.login("nonexistent@example.com", "somepassword");

        // Assert
        Assert.Null(retrievedUser);
    }
    [Fact]
    public async Task CreateOrder_checkOrderSum_ReturnsOrder()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var productRepository = new ProductsRepository(_context);
        var orderService = new OrderService(orderRepository, productRepository, _loggerMock.Object);

        var orderItems = new List<OrderItem>() { new() { ProductId = 1 } };
        var order = new Order { OrderSum = 6, OrderItems = orderItems };

        // Act
        var result = await orderService.addOrder(order);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(order, result);
        _loggerMock.VerifyNoOtherCalls();  // לא אמור להיות רישום לוג במקרה הזה
    }
    [Fact]
    public async Task CreateOrder_checkOrderSum_ReturnsNull()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var productRepository = new ProductsRepository(_context);
        var orderService = new OrderService(orderRepository, productRepository, _loggerMock.Object);

        // יצירת פריטי הזמנה עם מוצר קיים במסד הנתונים
        var orderItems = new List<OrderItem>()
            {
                new OrderItem { ProductId = 1, Quantity = 1 },
                new OrderItem { ProductId = 2, Quantity = 1 }
            };

        // כאן נניח שסכום ההזמנה המצופה הוא 6, אבל אנחנו נכנס סכום שגוי
        var order = new Order
        {
            OrderSum = 15.0,  // סכום שגוי
            OrderItems = orderItems
        };

        // Act: קריאה לפונקציה addOrder
        var result = await orderService.addOrder(order);

        // Assert: 
        Assert.Null(result);

        _loggerMock.VerifyNoOtherCalls();
    }

}
    