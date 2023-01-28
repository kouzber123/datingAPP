using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Controllers
{
  public class AccountController : BaseApiController
  {
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    public AccountController(DataContext context, ITokenService tokenService)
    {
      _tokenService = tokenService;
      _context = context;
    }
    //! THIS IS REGISTER DTO THIS CHECKS IF USER EXIST OR NOT
    [HttpPost("register")] //api account / register, // post: api/account/register?username=tom&password=pwd
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
      using var hmac = new HMACSHA512();
      //add this to the users db
      var user = new AppUser
      {
        UserName = registerDto.Username.ToLower(),
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        PasswordSalt = hmac.Key
      };
      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return new UserDto
      {
        Username = user.UserName,
        Token = _tokenService.CreateToken(user)
      };
    }
    //! --------------REGISTERING USER USE REGISTOR DTO TO COMPARE AND FIND FROM DB
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {// use singledefault to compare dto return invalid if null 
      var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

      if (user == null) return Unauthorized("invalid username");

      // compare passwords we are checking if hash [i] spot == dto pwr
      using var hmac = new HMACSHA512(user.PasswordSalt);
      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

      for (int i = 0; i < computedHash.Length; i++)
      {

        if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
      }// if satisfied then return the correct user
      return new UserDto
      {
        Username = user.UserName,
        Token = _tokenService.CreateToken(user)
      };
    }
    private async Task<bool> UserExists(string username)
    {
      return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
  }
}