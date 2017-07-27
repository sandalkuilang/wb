namespace Common.Container
{
    using System;
    using System.Collections.Generic;

    public abstract class BaseConcurrentDictionaryStorage : IRegistrationStorage<Type, ITypeRegistration>
    {
        protected IDictionary<string, ITypeRegistration> Bags;

        protected BaseConcurrentDictionaryStorage()
        {
        }

        public abstract ITypeRegistration Add(Type item);
        public void Clear()
        {
            this.Bags.Clear();
        }

        public abstract ITypeRegistration Get<T>();
        public abstract ITypeRegistration Get(string key);
        public abstract void Remove(Type item);

        public int Count
        {
            get
            {
                return this.Bags.Count;
            }
        }

        public abstract ICollection<string> Keys { get; }
    }
}

