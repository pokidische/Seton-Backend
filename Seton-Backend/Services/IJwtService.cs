namespace Seton_Backend.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
