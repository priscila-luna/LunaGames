namespace LunaGames.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool Status { get; set; }
    public string Password { get; set; }
}