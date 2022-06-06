using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Dto
{
    public class PostDto
    {
        [MaxLength(500)]
        public string Text { get; set; }
    }
}
