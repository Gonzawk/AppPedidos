using AppPedidos.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppPedidos.API.Helpers
{
    public static class JwtHelper
    {
        public static string GenerarToken(Usuario usuario, JwtSettings settings)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
   {
    new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
    new Claim("role", usuario.Rol.Nombre), 
    new Claim(JwtRegisteredClaimNames.Email, usuario.Email)
};


            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(settings.ExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
