using LunaGames.Models;
using LunaGames.Repositories;

namespace LunaGames.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> Create(User user)
    {
        return await _userRepository.Create(user);
    }

    public async Task Update(User user, int id)
    {
        user.Id = id;
        await _userRepository.Update(user);
    }

    public async Task<IEnumerable<User>> List()
    {
        return await _userRepository.List();
    }

    public async Task<User> Get(int id)
    {
        return await _userRepository.Get(id);
    }
}