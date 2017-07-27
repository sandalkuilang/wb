namespace Common.Container
{
    using System;
    using System.Collections.Generic;

    public interface IRegistrationStorage<T, V>
    {
        V Add(T item);
        void Clear();
        V Get<TKey>();
        V Get(string key);
        void Remove(T item); 
        int Count { get; }
    }
}

