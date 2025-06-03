using Xunit;
using Moq;
using System.Threading.Tasks;
using LunaGames.Models;
using LunaGames.Repositories;
using LunaGames.Services;

namespace LunaGames.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task GetTest()
    {
        var id = 1;
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.Get(id)).ReturnsAsync(new User { Id = id, Name = "joao" });

        var service = new UserService(mockRepo.Object);

        var result = await service.Get(id);

        Assert.NotNull(result);
        Assert.Equal(id, result?.Id);
    }
    [Fact]
    public async Task ListTest()
    {
        var listUsers = new List<User>
        {
            new User { Id = 1, Name = "joao" },
            new User { Id = 2, Name = "maria" }
        };
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.List()).ReturnsAsync(listUsers);

        var service = new UserService(mockRepo.Object);

        var result = await service.List();

        Assert.NotNull(result);
        Assert.Collection(result,
            user => Assert.Equal("joao", user.Name),
            user => Assert.Equal("maria", user.Name)
        );
    }

    [Fact]
    public async Task CreateTest()
    {
        var newUser = new User { Name = "maria", Email = "maria@teste.com" };
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.Create(newUser)).ReturnsAsync(42);

        var service = new UserService(mockRepo.Object);

        var result = await service.Create(newUser);

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task UpdateTest()
    {
        var id = 1;
        var newUser = new User { Name = "maria", Email = "maria@teste.com", Id = id };
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.Update(newUser));

        var service = new UserService(mockRepo.Object);

        await service.Update(newUser, id);

        mockRepo.Verify(r => r.Update(newUser), Times.Once);
    }
}
