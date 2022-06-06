using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShortStory.API.Dto;
using ShortStory.API.Enums;
using ShortStory.API.Models;
using ShortStory.API.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShortStory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;

        private readonly IConfiguration _config;

        public UserController(IConfiguration config,  AppDbContext db)
        {
            _config = config;
            _db = db;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterUserDto dto)
        {
            CreatePassword(dto.Password, out byte[] passwordHash, out byte[] salt);

            User user = new User(dto);
            user.PasswordHash = passwordHash;
            user.Salt = salt;


            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequestDto dto)
        {
            //1255376235
            User loginUser = _db.Users.Where(u => u.UserId == dto.UserId).FirstOrDefault();
            if (loginUser == null)
            {
                return BadRequest("No such user");
            }
            else if (loginUser.IsBanned == Banned.True)
            {
                return BadRequest("User Banned");
            }
            else if (!VerifyPasswordHash(dto.Password, loginUser.PasswordHash, loginUser.Salt))
            {
                return BadRequest("Wrong Password");
            }

            string token = CreateToken(loginUser);
            LoginResponseDto response = new LoginResponseDto(loginUser, token);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("getwriters")]
        public async Task<ActionResult<List<User>>> GetWriters()
        {
            IQueryable<User> users = _db.Users.Include(u => u.Followings);
            User currentUser = users.Where(u => u.UserId == HttpContext.User.Identity.Name).FirstOrDefault();

            if (currentUser.Followings?.Count == 0)
            {
                return _db.Users.OrderBy(u => u.LastPostedDate).ToList();
            }

            List<User> writers = new List<User>();
            foreach (Following f in currentUser.Followings)
            {
                writers.Add(_db.Users.Find(f.InterestedUserId));
            }

            writers.OrderBy(u => u.LastPostedDate);
            return Ok(writers);
        }


        [Authorize(Roles = "Moderator")]
        [HttpGet("GetAllNormalUsers")]
        public async Task<ActionResult<List<User>>> GetAllNormalUsers()
        {
            List<User> writers = _db.Users.Where(u => u.UserRole == UserRole.User).ToList();
            return Ok(writers);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public async Task<ActionResult<User>> Get(string userId)
        {
            User writer = _db.Users.Where(u => u.UserId == userId && u.UserRole == UserRole.User).FirstOrDefault();
            return Ok(writer);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPut("edit")]
        public async Task<ActionResult<List<User>>> Edit(EditUserDto dto)
        {
            User user = _db.Users.Find(dto.UserId);
            if (user == null || user.UserRole == UserRole.Moderator)
            {
                return BadRequest();
            }

            user.IsBanned = dto.IsBanned;
            user.IsEditor = dto.IsEditor;

            _db.Users.Update(user);
            _db.SaveChanges();

            return Ok(user);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserId));
            claims.Add(new Claim(ClaimTypes.Role, user.UserRole.ToString()));

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePassword(string passwordText, out byte[] passwordHash, out byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
            }
        }

        private bool VerifyPasswordHash(string passwordText, byte[] passwordHash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
