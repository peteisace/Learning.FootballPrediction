using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.FootballPrediction.Api.Repositories
{
    public class ModelCache<K, V> where V : class
    {
        private Dictionary<K, V> _storage = new Dictionary<K, V>();

        public V this[K key] => this.GetValue(key);

        public V GetValue(K key)
        {
            // Do we have it?
            if(!this._storage.ContainsKey(key))
            {
                return null;
            }

            return this._storage[key];
        }

        public void Add(K key, V value)
        {
            var sbn = this.GetValue(key);

            if(sbn != null)
            {
                throw new ArgumentException($"Already cached item with key {key}.", nameof(key));
            }

            this._storage.Add(key, value);
        }
    }
}