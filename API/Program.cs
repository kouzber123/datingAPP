using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.addApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

//above this are services
var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
// Configure the HTTP request pipeline.
app.UseAuthentication(); // night club under 18 > with 16 y/o papers this says yup its valid id
app.UseAuthorization(); // to allowed to nightclub > valid id but does not auhtorise to go

app.MapControllers();

app.Run();
