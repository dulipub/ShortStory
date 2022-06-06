using ShortStory.API.Enums;
using ShortStory.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Dto
{
    public class EditUserDto
    {
        [Required]
        [MaxLength(12)]
        public string UserId { get; set; }

        public EditorRole? IsEditor { get; set; } = EditorRole.False;

        public Banned? IsBanned { get; set; } = Banned.False;

        public EditUserDto()
        {

        }

        public EditUserDto(User user)
        {
            UserId = user.UserId;
            IsEditor = user.IsEditor;
            IsBanned = user.IsBanned;
        }
    }
}
