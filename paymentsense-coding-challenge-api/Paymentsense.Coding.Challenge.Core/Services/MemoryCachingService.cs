using Microsoft.Extensions.Caching.Memory;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Services
{
    public class MemoryCachingService : ICachingService
    {
        private readonly IMemoryCache _cache;

        public MemoryCachingService(IMemoryCache cache)
        {
            _cache = cache;
        }

        //abstracted away caching technique
        //same caching policy can be re-used in future
        //it will also be easier to switch out memory caching for another technique by implementing ICachingService
        //also mocking is much easier, as memory cache uses extension methods which cant be mocked,
        //the underlying methods can but it becomes more complex to set up, we dont really need to test IMemoryCache specifically
        //and integration tests can test that it works functionally
        public async Task<T> GetOrAddAsync<T>(string cacheKey, Func<Task<T>> func)
        {
            return await _cache.GetOrCreateAsync<T>(
                cacheKey,
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return func.Invoke();
                }).ConfigureAwait(false);
        }
    }
}
