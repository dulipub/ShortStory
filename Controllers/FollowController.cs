using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortStory.API.Dto;
using ShortStory.API.Models;
using ShortStory.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly AppDbContext _db;

        public FollowController(AppDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FollowDto>> Create(FollowDto dto)
        {
            Following follow = new Following();
            follow.UserId = HttpContext.User.Identity.Name;
            follow.InterestedUserId = dto.InterestedUserId;

            _db.Add(follow);
            _db.SaveChanges();

            return Ok(follow);
        }
    }
}
