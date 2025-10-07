using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using School.Models;

namespace School.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user, IList<string> roles);
    }
}