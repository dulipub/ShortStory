using ShortStory.API.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Dto
{
    public class RegisterUserDto
    {
        [Required]
        [MaxLength(12)]
        public string UserId { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public EditorRole IsEditor { get; set; } = EditorRole.False;

        public Banned IsBanned { get; set; } = Banned.False;
    }
}
