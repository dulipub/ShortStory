using ShortStory.API.Dto;
using ShortStory.API.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Models
{
    public class User
    {
        [Key]
        [MaxLength(12)]
        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] Salt { get; set; }

        [Required]
        public UserRole UserRole { get; set; }

        public EditorRole? IsEditor { get; set; }

        public Banned? IsBanned { get; set; }

        public List<Post> Posts { get; set; }

        public DateTime? LastPostedDate { get; set; }

        public List<Following> Followings { get; set; }

        public User() { }

        public User(RegisterUserDto dto)
        {
            UserId = dto.UserId;
            FirstName = dto.Firstname;
            LastName = dto.LastName;
            Email = dto.Email;
            IsBanned = Banned.False;
            IsEditor = EditorRole.False;
            UserRole = UserRole.User;
        }
    }
}
