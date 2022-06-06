using ShortStory.API.Models;
using ShortStory.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Services
{
    public class StatsService : IStatsService
    {
        private static HashSet<char> Vowels = new HashSet<char>();

        private readonly AppDbContext _db;

        public StatsService(AppDbContext db)
        {
            _db = db;

            Vowels.Add('a');
            Vowels.Add('e'); 
            Vowels.Add('i'); 
            Vowels.Add('o'); 
            Vowels.Add('u');
        }

        public void AddStatistics(string text)
        {
            StatVowel currentDayStats = _db.StatVowels.Where(s => s.Date == DateTime.Today).FirstOrDefault();
            if (currentDayStats == null)
            {
                currentDayStats = new StatVowel();
                currentDayStats.Date = DateTime.Today;
                _db.StatVowels.Add(currentDayStats);
                _db.SaveChanges();
            }

            char[] characters = text.ToLower().ToCharArray();
            for (int i = 0; i < text.Length; i++)
            {
                char c = characters[i];
                if (Vowels.Contains(c))
                {
                    if (Vowels.Contains(characters[i+1]))
                    {
                        currentDayStats.PairVowelCount++;
                        i++;
                    }
                    else
                    {
                        currentDayStats.SingleVowelCount++;
                    }
                }
            }

            _db.StatVowels.Update(currentDayStats);
            _db.SaveChanges();
        }
    }
}
