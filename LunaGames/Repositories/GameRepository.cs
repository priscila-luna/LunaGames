using System.Data;
using Dapper;
using LunaGames.Models;
using Npgsql;

namespace LunaGames.Repositories;

public class GameRepository : IGameRepository
{
    private readonly string _connectionString;

    public GameRepository(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionStrings:Default"];
    }

    public async Task<int> Create(Game game)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"INSERT INTO Game (Title, Type, Description)
                    VALUES (@Title, @Type, @Description);
                    RETURNING id";
        return await connection.QueryFirstOrDefaultAsync<int>(sql, game);
    }

    public async Task<IEnumerable<Game>> List(int userId)
    {
        var sql = @"SELECT g.* FROM Game g 
                JOIN UserGame ug ON g.Id = ug.GameId
                WHERE userid = @userId";
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<Game>(sql, new { userId });
    }

    public async Task Update(Game game)
    {
        var sql = @"UPDATE Game 
                    SET Title = @Title, Type = @Type, Description = @Description 
                    WHERE Id = @Id";
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, game);
    }

    public async Task UserLink(int userId)
    {
        var sql = @"UPDATE Game 
                    SET Title = @Title, Type = @Type, Description = @Description 
                    WHERE Id = @Id";
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, userId);
    }
}