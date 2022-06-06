using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class StatsController : ControllerBase
    {

        private readonly AppDbContext _db;

        public StatsController(AppDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public async Task<ActionResult<List<StatVowel>>> Get()
        {
            return Ok(_db.StatVowels.ToList());
        }
    }
}
