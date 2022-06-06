using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Models
{
    public class StatVowel
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int SingleVowelCount { get; set; }

        public int PairVowelCount { get; set; }

        public int WordCount { get; set; }
    }
}
