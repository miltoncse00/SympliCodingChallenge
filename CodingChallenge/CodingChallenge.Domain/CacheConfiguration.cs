using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallenge.Domain
{
    public class CacheConfiguration
    {
        public const string Setting = "CacheOptions";
        public int MaxTimeMins { get; set; }
    }
}
