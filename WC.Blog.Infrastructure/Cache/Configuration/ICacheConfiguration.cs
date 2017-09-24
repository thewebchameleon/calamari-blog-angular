using System;

namespace WC.Blog.Infrastructure.Cache.Configuration
{
    public interface ICacheConfiguration
    {
        TimeSpan ExpiryTime { get; }
    }
}
