namespace Common.Container
{
    using System;
    using System.Collections.Concurrent;

    public class ConcurrentStorage : BaseConcurrentDictionaryStorage
    {
        private readonly IActivator activator;

        public ConcurrentStorage(IActivator activator)
        {
            base.Bags = new ConcurrentDictionary<string, ITypeRegistration>();
            this.activator = activator;
        }

        public override ITypeRegistration Add(Type item)
        {
            if (item != null && base.Bags.ContainsKey(item.FullName))
            {
                base.Bags.Remove(item.FullName);
            }
            TypeRegistration registration = new TypeRegistration(item, this.activator);
            registration.Lifetime = Lifetime.Transient;
            registration.Name = item.FullName;
            registration.Type = item;
            base.Bags.Add(this.GetRegistrationKey(item), registration);
            return registration; 
        }

        public override ITypeRegistration Get<T>()
        {
            return this.Get(this.GetRegistrationKey(typeof(T)));
        }

        public override ITypeRegistration Get(string key)
        {
            ITypeRegistration registration;
            base.Bags.TryGetValue(key, out registration);
            return registration;
        }

        private string GetRegistrationKey(Type type)
        {
            if (type != null)
            {
                return type.FullName;
            }
            return string.Empty;
        }

        public override void Remove(Type item)
        {
            base.Bags.Remove(this.GetRegistrationKey(item));
        }

        public override System.Collections.Generic.ICollection<string> Keys
        {
            get { return base.Bags.Keys; }
        }
    }
}

