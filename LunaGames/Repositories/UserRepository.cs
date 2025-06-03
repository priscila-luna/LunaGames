using Dapper;
using LunaGames.Models;
using Npgsql;

namespace LunaGames.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionStrings:Default"];
    }

    public async Task<int> Create(User user)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"INSERT INTO User (Username, Email, PasswordHash)
                    VALUES (@Username, @Email, @PasswordHash)
                    RETURNING id";
        return await connection.QueryFirstOrDefaultAsync<int>(sql, user);
    }

    public async Task<User> Get(int id)
    {
        var sql = "SELECT * FROM User where id = id";
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { id });
    }

    public async Task<User> Get(string login)
    {
        return new User { Role = "User", Id = 1, Email = "string", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3" };
        var sql = "SELECT * FROM User where username = login";
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { login });
    }

    public async Task<IEnumerable<User>> List()
    {
        var sql = "SELECT * FROM User";
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<User>(sql);
    }

    public async Task Update(User user)
    {
        var sql = @"UPDATE User 
                    SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash 
                    WHERE Id = @Id";
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, user);
    }
}