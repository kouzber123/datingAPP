using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
  public static class ApplicationServiceExtensions
  {
    //this > extending
    public static IServiceCollection addApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddDbContext<DataContext>(opt =>
    {
      opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
    });
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      services.AddCors();
      services.AddScoped<ITokenService, TokenService>(); //advantage using interface, easier testing

      return services;
    }

  }
}