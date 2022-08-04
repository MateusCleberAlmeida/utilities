using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Teste.Services
{
    public static class TokenService
    {
        public static string GenerateToken(UserService user)
        {
            // Instanciar a classe responsável por gerar o token
            var tokenHandler = new JwtSecurityTokenHandler();

            // transforma a chave secreta em array de bytes
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            // descrever todas as informações contidas no token
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                // Lista de Claims (afirmações) - Pode ser colocado as informações que quiser aqui
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),  // User.Identity.Name - para saber em qualquer parte do sistema (.net já mapeia)
                    new Claim(ClaimTypes.Role, user.Role),      // User.IsInRole - para saber em qualquer parte do sistema
                    //new Claim("Teste", "Testando"),      // exemplo se quiser criar outros
                }),

                // validade do token
                Expires = DateTime.UtcNow.AddHours(2),
                // objeto responsável por encriptar e desemcriptar o token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // gera o token
            var token = tokenHandler.CreateToken(TokenDescriptor);

            // retorna o token gerado
            return tokenHandler.WriteToken(token);
        }
    }
}