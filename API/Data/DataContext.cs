using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{//! 1. create data context with dbcontext from entity framework
  public class DataContext : DbContext
  {
    //instiate constructor
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<AppUser> Users { get; set; }
  }
}