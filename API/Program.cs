using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.addApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

//above this are services
var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
// Configure the HTTP request pipeline.
app.UseAuthentication(); // night club under 18 > with 16 y/o papers this says yup its valid id
app.UseAuthorization(); // to allowed to nightclub > valid id but does not auhtorise to go

app.MapControllers();

using var scope = app.Services.CreateAsyncScope();
var services = scope.ServiceProvider;
try{
  var context = services.GetRequiredService<DataContext>();
  await context.Database.MigrateAsync();
  await Seed.SeedUsers(context);
}
catch (Exception ex)
{
  var logger = services.GetService<ILogger<Program>>();
  logger.LogError(ex, "An Error occured during migration");
}

app.Run();
