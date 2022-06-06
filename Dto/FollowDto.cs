using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Dto
{
    public class FollowDto
    {
        [MaxLength(12)]
        public string InterestedUserId { get; set; }
    }
}
