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
        var sql = @"INSERT INTO USERS (Name, Email, Status, Password, Role)
                    VALUES (@Name, @Email, @Status, @Password, @Role);
                    RETURNING id";
        return await connection.QueryFirstOrDefaultAsync<int>(sql, user);
    }

    public async Task<User> Get(int id)
    {
        var sql = "SELECT * FROM Users where id = @id";
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { id });
    }

    public async Task<User> Get(string login)
    {
        var sql = "SELECT * FROM Users where Email = @login";
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { login });
    }

    public async Task<IEnumerable<User>> List()
    {
        var sql = "SELECT * FROM Users";
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<User>(sql);
    }

    public async Task Update(User user)
    {
        var sql = @"UPDATE Users 
                    SET Name = @Name, Email = @Email, Status = @Status, Password = @Password 
                    WHERE Id = @Id";
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, user);
    }
}