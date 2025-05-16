using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Senai_Notas.Services
{
    public class TokenService
    {
        private readonly string _secretKey;
        private readonly int _expirationMinutes;

        /// <summary>
        /// Inicializa o TokenService com a chave secreta e tempo de expiração.
        /// </summary>
        public TokenService(string secretKey, int expirationMinutes = 60)
        {
            _secretKey = secretKey;
            _expirationMinutes = expirationMinutes;
        }

        /// <summary>
        /// Gera um token JWT para o usuário informado.
        /// </summary>
        public string GenerateToken(int userId, string email, string nome)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, nome)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
