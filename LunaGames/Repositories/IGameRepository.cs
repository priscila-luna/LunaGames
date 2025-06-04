using LunaGames.Models;

namespace LunaGames.Repositories;

public interface IGameRepository
{
    public Task<int> Create(Game game);
    public Task Update(Game game);
    public Task UserLink(int gameId, int userId);
    public Task<IEnumerable<Game>> List(int userId);
    public Task<IEnumerable<Game>> List();
}