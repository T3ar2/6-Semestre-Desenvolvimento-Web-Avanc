using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MinhaApi.Services;

public class TokenService {
    private readonly IConfiguration config;

    public TokenService(IConfiguration config)
    {
        this.config = config;
    }

    public class GenerateToken(string username) { 
        var key = new Encoding.UTF8.GetBytes(config["Jwt: key"]);

        var claims = new [](
            new Claim (ClaimTypes.Name, username)
        );

        var token = new JwtSecurityToken(
            claims: claism,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

       return new JwtSecurityTokenHandler().WriteToken(token);
    }
}