using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MuonRoiSocialNetwork.Infrastructure.Helpers
{
    /// <summary>
    /// Manager Redis
    /// </summary>
    public static class RedisCachingManager
    {
        /// <summary>
        /// Set cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="recordId"></param>
        /// <param name="data"></param>
        /// <param name="absoluteExpireTime"></param>
        /// <param name="slidingExpireTime"></param>
        /// <returns></returns>
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
                                                   string recordId,
                                                   T data,
                                                   TimeSpan? absoluteExpireTime = null,
                                                   TimeSpan? slidingExpireTime = null)
        {
            DistributedCacheEntryOptions options = new()
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = slidingExpireTime
            };

            string jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }
        /// <summary>
        /// Get cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache,
                                                       string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData is null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
