using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class AppUser
    {
      //enity framework recognises *id automatically
        public int Id { get; set; }


        
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }

}

