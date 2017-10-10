namespace CB.Infrastructure.Cache
{
    public class CacheSettings
    {
        public int ExpiryTimeMinutes { get; set; }

        public bool UseMemoryCache { get; set; }
    }
}
