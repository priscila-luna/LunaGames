namespace LunaGames.Auth;

public interface IToken
{
    public int GetId();
    public string GetRole();
}