using API.Entities;

namespace API.Interfaces
{
  public interface ITokenService
  {
    //this method is a contract and other has to accept this conditions
    string CreateToken(AppUser user);
  }
}