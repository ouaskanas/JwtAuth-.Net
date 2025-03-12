using jwt.Models;

namespace jwt.Services
{
    public interface IJwtService
    {
        public Task<string> GenerateToken(User user);
    }
}
