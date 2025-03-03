using Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Services;
namespace TestOurStore;

public class UnitTest
{
    [Fact]
    public async Task GetUser_ValidCredentials_ReturnsUser()
    {
        var user = new User { Email = "Efart@gmail.com", Password = "bg742tq7ubehuifuihfdshu" };
        var mockContext = new Mock<OurStoreContext>();
        var users = new List<User>() { user };
        mockContext.Setup(x => x.Users).ReturnsDbSet(users);
        var userRepository = new UserRepository(mockContext.Object);
        var result = await userRepository.login(user.Email, user.Password);

        Assert.Equal(user, result);
    }
    [Fact]
    public async Task GetUser_InvalidPassword_ReturnsNull()
    {
        // Arrange
        var user = new User { Email = "Efart@gmail.com", Password = "correctpassword" };
        var mockContext = new Mock<OurStoreContext>();
        var users = new List<User>() { user };
        mockContext.Setup(x => x.Users).ReturnsDbSet(users); // הגדרת המוק למסד הנתונים

        var userRepository = new UserRepository(mockContext.Object);
        var result = await userRepository.login(user.Email, "wrongpassword");  // סיסמה שגויה

        // Assert
        Assert.Null(result);  // אנחנו מצפים שהתשובה תהיה Null אם הסיסמה לא נכונה
    }
    [Fact]
    public async Task GetUser_InvalidEmail_ReturnsNull()
    {
        // Arrange
        var user = new User { Email = "Efart@gmail.com", Password = "bg742tq7ubehuifuihfdshu" };
        var mockContext = new Mock<OurStoreContext>();
        var users = new List<User>() { user };
        mockContext.Setup(x => x.Users).ReturnsDbSet(users); // הגדרת המוק למסד הנתונים

        var userRepository = new UserRepository(mockContext.Object);
        var result = await userRepository.login("nonexistentuser@gmail.com", "bg742tq7ubehuifuihfdshu");

        // Assert
        Assert.Null(result);  // אנחנו מצפים שהתשובה תהיה Null אם האימייל לא נמצא
    }

    [Fact]
    public async Task CreateOrder_checkOrderSum_ReturnsOrder()
    {
        var orderItems = new List<OrderItem>() { new() { ProductId = 1 } };
        var order = new Order { OrderSum = 6, OrderItems = orderItems };

        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockProductRepository = new Mock<IProductsRepository>();
        var mockLogger = new Mock<ILogger<OrderService>>();

        var products = new List<Product> { new Product { ProductId = 1, Price = 6 } };
        mockProductRepository.Setup(x => x.GetProducts(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?[]>()))
                             .ReturnsAsync(products);

        mockOrderRepository.Setup(x => x.addOrder(It.IsAny<Order>()))
                           .ReturnsAsync(order);

        var orderService = new OrderService(mockOrderRepository.Object, mockProductRepository.Object, mockLogger.Object);

        // Act
        var result = await orderService.addOrder(order);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(order, result);
    }

    [Fact]
    public async Task AddOrder_OrderSumNotMatching_UpdatesOrderSum()
    {
        // Arrange
        var order = new Order { OrderSum = 100, OrderItems = new List<OrderItem> { new OrderItem { ProductId = 1 } } };

        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockProductsRepository = new Mock<IProductsRepository>();
        var mockLogger = new Mock<ILogger<OrderService>>();

        // מוצר לדוגמה עם מחיר
        var product = new Product { ProductId = 1, Price = 120 };
        mockProductsRepository.Setup(x => x.GetProducts(It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?[]>()))
                             .ReturnsAsync(new List<Product> { product });

        // mock של ה-Repository שמחזיר את ההזמנה כפי שהיא
        mockOrderRepository.Setup(x => x.addOrder(It.IsAny<Order>()))
                           .ReturnsAsync(order);

        var orderService = new OrderService(mockOrderRepository.Object, mockProductsRepository.Object, mockLogger.Object);

        // Act
        var result = await orderService.addOrder(order);

        // Assert
        Assert.Equal(120, result.OrderSum);  // ודא שהסכום עודכן ל-120
        //mockLogger.Verify(x => x.LogCritical(It.IsAny<string>()), Times.Once);  // ווידוא שהופק לוג קריטי
    }
}
