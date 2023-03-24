using AmazonRegistration.Interface;
using AmazonRegistration.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmazonRegistration.Repo
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext db;

        public TokenService(ApplicationDbContext db, IConfiguration _configuration)
        {
            this._configuration = _configuration;
            this.db = db;
        }


        public JwtSecurityToken GenerateAccessToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            _ = int.TryParse(_configuration["Jwt:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            try
            {

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false, 
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"])),
                    ValidateLifetime = false,
                    RequireExpirationTime = true

                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
                return principal;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Invalid token");
            }
        }
        public dynamic GenerateAndSaveToken(List<Claim> claims, RegistrationModel user)
        {
            var accessToken = GenerateAccessToken(claims);
            
            return (new
            {

                Token = new JwtSecurityTokenHandler().WriteToken(accessToken),
            });
        }


    }
}
