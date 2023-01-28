using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
  public class TokenService : ITokenService
  {
    //implentation class
    private readonly SymmetricSecurityKey _key; //crypts and decrypts the key
    public TokenService(IConfiguration config)
    {
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }
    public string CreateToken(AppUser user)
    {
      var claims = new List<Claim>
      { //set of claims that user claims e.g. username "tom"
        new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
    };
      var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims), //list of claims
        Expires = DateTime.Now.AddDays(7),  //life cycle time
        SigningCredentials = creds
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}