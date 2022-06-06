using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStory.API.Services.Interfaces
{
    public interface IStatsService
    {
        public void AddStatistics(string text);
    }
}
