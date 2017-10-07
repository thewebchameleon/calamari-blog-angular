using Microsoft.Extensions.Configuration;
using System;

namespace CB.Infrastructure.Cache.Configuration
{
    public class CacheConfiguration : ICacheConfiguration
    {
        IConfiguration Base { get; set; }

        public CacheConfiguration(IConfiguration root)
        {
            Base = root.GetSection("Cache");
        }

        public TimeSpan ExpiryTime
        {
            get
            {
                return TimeSpan.FromMinutes(Convert.ToInt32(Base["ExpiryTimeMinutes"]));
            }
        }
    }
}
