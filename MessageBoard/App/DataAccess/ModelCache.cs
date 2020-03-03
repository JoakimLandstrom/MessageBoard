using System;
using System.Collections.Generic;
using System.Linq;
using App.Models;

using Microsoft.Extensions.Caching.Memory;

namespace App.DataAccess
{
    public interface IModelCache
    {
        TValue Get<TValue>(Guid guid);
        List<TValue> GetAll<TValue>();
        TValue Update<TValue>(TValue value) where TValue : ModelBase;
        TValue Create<TValue>(TValue value) where TValue : ModelBase;
        void Delete<TValue>(Guid guid);
    }

    public class ModelCache : IModelCache
    {
        private readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public TValue Get<TValue>(Guid guid)
        {
            if (_cache.TryGetValue(typeof(TValue), out Dictionary<Guid, TValue> entry))
                return entry[guid];
            
            throw new ApplicationException($"Entry with key '{guid}' not found.");
        }

        public List<TValue> GetAll<TValue>()
            => _cache.Get<Dictionary<Guid, TValue>>(typeof(TValue))?.Values.ToList() ?? new List<TValue>();
        

        public TValue Update<TValue>(TValue value) where TValue : ModelBase
        {
            if(_cache.TryGetValue(typeof(TValue), out Dictionary<Guid, TValue> entry))
                if (entry.TryGetValue(value.Id, out _)){
                    entry[value.Id] = value;
                    
                    return entry[value.Id];
                }
            
            throw new ApplicationException($"Entry with key '{value?.Id}' not found.");
        }


        public TValue Create<TValue>(TValue value) where TValue : ModelBase
        {
            var key = Guid.NewGuid();
            value.Id = key;

            if (_cache.TryGetValue(typeof(TValue), out Dictionary<Guid, TValue> entry))
                entry.Add(key, value);
            else
                _cache.Set(typeof(TValue), new Dictionary<Guid, TValue> {{key, value}});
            
            return value;
        }

        public void Delete<TValue>(Guid guid)
        {
            if (!_cache.TryGetValue(typeof(TValue), out Dictionary<Guid, TValue> entry))
                throw new ApplicationException($"Entry with key '{guid}' not found.");
            
            entry.Remove(guid);
        }
    }
}