using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShortStory.API.Models;

namespace ShortStory.API.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Following> Followings { get; set; }

        public DbSet<StatVowel> StatVowels { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
