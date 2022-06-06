using ShortStory.API.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public DateTime Created { get; set; }

        [MaxLength(500)]
        public string PostText { get; set; }

        public Post()
        {

        }

        public Post(PostDto dto, string userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Created = DateTime.Now;
            PostText = dto.Text;
        }
    }
}
