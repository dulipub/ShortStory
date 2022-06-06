using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortStory.API.Dto;
using ShortStory.API.Models;
using ShortStory.API.Services;
using ShortStory.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _db;

        private readonly IStatsService _statsService;

        public PostsController(AppDbContext db, IStatsService statsService)
        {
            _db = db;
            _statsService = statsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Post>> Create(PostDto dto)
        {
            User author = _db.Users.Find(HttpContext.User.Identity.Name);
            Post newPost = new Post(dto, author.UserId);
            author.LastPostedDate = newPost.Created;

            _db.Posts.Add(newPost);
            _db.Update(author);
            _db.SaveChanges();

            _statsService.AddStatistics(dto.Text);

            return Ok(newPost);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Post>>> Get()
        {
            return _db.Posts.OrderBy(p => p.Created).ToList();
        }
    }
}
