namespace WC.Blog.Infrastructure.Cache
{
    public interface ICacheProvider
    {
        bool TryGetItem<T>(string key, out T item);

        T SetItem<T>(string key, T item);
    }
}
