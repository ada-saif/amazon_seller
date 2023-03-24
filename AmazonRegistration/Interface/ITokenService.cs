using AmazonRegistration.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AmazonRegistration.Interface
{
    public interface ITokenService
    {
        public JwtSecurityToken GenerateAccessToken(List<Claim> authClaims);
        public dynamic GenerateAndSaveToken(List<Claim> claims, RegistrationModel user);
    }
}
