using Xunit;
using Moq;
using System.Threading.Tasks;
using LunaGames.Models;
using LunaGames.Repositories;
using LunaGames.Services;

namespace LunaGames.Tests;

public class GameServiceTests
{
    [Fact]
    public async Task ListTest()
    {
        int userid = 1;
        var listGames = new List<Game>
        {
            new Game { Id = 1, Title = "jogo a" },
            new Game { Id = 2, Title = "jogo b" }
        };
        var mockRepo = new Mock<IGameRepository>();
        mockRepo.Setup(r => r.List(userid)).ReturnsAsync(listGames);

        var service = new GameService(mockRepo.Object);

        var result = await service.List(userid);

        Assert.NotNull(result);
        Assert.Collection(result,
            Game => Assert.Equal("jogo a", Game.Title),
            Game => Assert.Equal("jogo b", Game.Title)
        );
    }

    [Fact]
    public async Task CreateTest()
    {
        var newGame = new Game { Title = "jogo b", Description = "descricao do jogo" };
        var mockRepo = new Mock<IGameRepository>();
        mockRepo.Setup(r => r.Create(newGame)).ReturnsAsync(42);

        var service = new GameService(mockRepo.Object);

        var result = await service.Create(newGame);

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task UpdateTest()
    {
        var id = 1;
        var newGame = new Game { Title = "jogo b", Description = "descricao do jogo", Id = id };
        var mockRepo = new Mock<IGameRepository>();
        mockRepo.Setup(r => r.Update(newGame));

        var service = new GameService(mockRepo.Object);

        await service.Update(newGame, id);

        mockRepo.Verify(r => r.Update(newGame), Times.Once);
    }

    [Fact]
    public async Task UserLinkTest()
    {
        var id = 1;
        var mockRepo = new Mock<IGameRepository>();
        mockRepo.Setup(r => r.UserLink(id));

        var service = new GameService(mockRepo.Object);

        await service.UserLink(id);

        mockRepo.Verify(r => r.UserLink(id), Times.Once);
    }
}
