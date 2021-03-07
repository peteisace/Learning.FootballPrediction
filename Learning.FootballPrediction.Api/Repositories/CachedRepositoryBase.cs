using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Learning.FootballPrediction.Api.Repositories
{
    public abstract class CachedRepositoryBase<K, V, M> where V : class where M : class
    {
        private ModelCache<K, V> _cache = new ModelCache<K, V>();
        private string _connectionString;

        protected CachedRepositoryBase(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString(this.ConnectionName);
        }

        protected virtual string ConnectionName => "default";
        protected string ConnectionString => this._connectionString;

        public async Task<V> Get(K key)
        {
            // Check cache
            var item = this._cache.GetValue(key);

            if(item == null)
            {
                // Hit the store
                item = await this.FetchFromStore(key);

                // Conceivably the item may not exist in store.
                if(item != null)
                {
                    this._cache.Add(key, item);
                }                
            }

            return item;
        }

        public async Task<V> Save(M model, K key)
        {
            // Must not exist
            if(this._cache.GetValue(key) != null)
            {
                throw new ArgumentException($"Object with key {key} already exists.", nameof(key));
            }

            V instance = await this.SaveDetailed(model);
            this._cache.Add(key, instance);

            return instance;
        }   

        protected abstract Task<V> SaveDetailed(M model);

        protected abstract Task<V> FetchFromStore(K key);
    }
}