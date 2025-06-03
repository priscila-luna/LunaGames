using LunaGames.Models;

namespace LunaGames.Repositories;

public interface IUserRepository
{
    public Task<int> Create(User user);
    public Task Update(User user);
    public Task<User> Get(int id);
    public Task<User> Get(string login);
    public Task<IEnumerable<User>> List();
}