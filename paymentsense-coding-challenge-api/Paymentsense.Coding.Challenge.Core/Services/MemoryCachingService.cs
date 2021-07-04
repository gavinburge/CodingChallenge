using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Services
{
    public class MemoryCachingService : ICachingService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<MemoryCachingService> _logger;
        private readonly SemaphoreSlim _cacheLock = new SemaphoreSlim(1);

        public MemoryCachingService(IMemoryCache cache, ILogger<MemoryCachingService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        //abstracted away caching technique
        //same caching policy can be re-used in future
        //it will also be easier to switch out memory caching for another technique by implementing ICachingService
        //also mocking is much easier, as memory cache uses extension methods which cant be mocked,
        //the underlying methods can but it becomes more complex to set up, we dont really need to test IMemoryCache specifically
        //and integration tests can test that it works functionally
        public async Task<T> GetOrAddAsync<T>(string cacheKey, Func<Task<T>> func)
        {
            try
            {
                //whilst memorycache is thread safe, if two threads were to call Get for the first time, then both could end up missing the cached result of the other,
                //and both would make a call to the external service, this was proven with country spec test.
                //using semphore slim to lock means only once will the service be called
                await _cacheLock.WaitAsync();

                var cachedData = await _cache.GetOrCreateAsync(
                                                cacheKey,
                                                entry =>
                                                {
                                                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                                                    _logger.LogInformation($"Retrieving data for cache key {cacheKey}");
                                                    return func.Invoke();
                                                });

                return cachedData;
            }
            finally
            {
                _cacheLock.Release();
            }
        }
    }
}
