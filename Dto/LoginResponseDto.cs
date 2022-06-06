using ShortStory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Dto
{
    public class LoginResponseDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }


        public LoginResponseDto(User user, string token)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Role = user.UserRole.ToString();
            Token = token;
        }
    }
}
