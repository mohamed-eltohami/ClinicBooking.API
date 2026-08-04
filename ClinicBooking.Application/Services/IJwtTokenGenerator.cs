namespace ClinicBooking.Application.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string email, string fullName, IList<string> roles);
    }
}