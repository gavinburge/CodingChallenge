using System;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Interfaces
{
    public interface ICachingService
    {
        Task<T> GetOrAddAsync<T>(string cacheKey, Func<Task<T>> func);
    }
}