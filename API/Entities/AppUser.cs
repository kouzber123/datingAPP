using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using API.Extensions;

namespace API.Entities
{
  public class AppUser
  {
    //enity framework recognises *id automatically
    public int Id { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string Gender { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<Photo> Photos { get; set; } = new(); //user can have many photos one to many

    // public int GetAge() //get very important naming
    // {
    //   return DateOfBirth.CalculateAge();
    // }

  }

}

