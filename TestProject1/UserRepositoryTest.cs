using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
namespace TestProject1;

public class UserRepositoryTest
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
}
