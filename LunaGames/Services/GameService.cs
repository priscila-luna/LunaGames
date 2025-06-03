using LunaGames.Models;
using LunaGames.Repositories;

namespace LunaGames.Services;

public class GameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<int> Create(Game game)
    {
        return await _gameRepository.Create(game);
    }

    public async Task Update(Game game, int id)
    {
        game.Id = id;
        await _gameRepository.Update(game);
    }

    public async Task<IEnumerable<Game>> List(int userId)
    {
        return await _gameRepository.List(userId);
    }

    public async Task UserLink(int userId)
    {
        await _gameRepository.UserLink(userId);
    }
}