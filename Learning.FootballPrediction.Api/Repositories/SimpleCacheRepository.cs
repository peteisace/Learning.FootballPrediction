using Microsoft.Extensions.Configuration;

namespace Learning.FootballPrediction.Api.Repositories
{
    public abstract class SimpleCacheRepository<K, V> : CachedRepositoryBase<K, V, V> where V : class
    {
        protected SimpleCacheRepository(IConfiguration configuration) : base(configuration)
        {            
        }
    }
}