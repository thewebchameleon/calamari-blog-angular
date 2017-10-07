using System;

namespace CB.Infrastructure.Cache.Configuration
{
    public interface ICacheConfiguration
    {
        TimeSpan ExpiryTime { get; }
    }
}
