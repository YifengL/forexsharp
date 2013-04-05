using System;
using System.Collections.Generic;

namespace TradePlatform.MT4.Core
{
    public abstract class Container<T>
        where T : new()
    {
        private static Dictionary<string, T> _storage;

        static Container()
        {
            _storage = new Dictionary<string, T>();
        }

        public static T GetOrCreate(string key)
        {
            T t;
            if (!_storage.ContainsKey(key))
            {
                T t1 = default(T);
                if (t1 == null)
                {
                    t = Activator.CreateInstance<T>();
                }
                else
                {
                    T t2 = default(T);
                    t = t2;
                }
                T t3 = t;
                _storage.Add(key, t3);
                return t3;
            }
            else
            {
                return _storage[key];
            }
        }
    }
}