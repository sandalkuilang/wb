namespace Common.Container
{
    using System;
    using System.Threading;

    public class TypeRegistration : BaseRegistrationInfo, ITypeRegistration
    {
        private object[] args;
        private Type implementation;
        private object instance;
        private Type registerType;
        private static readonly object syncLock = new object();

        public TypeRegistration(Type type, IActivator activator)
        {
            this.registerType = type;
            base.Activator = activator;
        }
         
        internal override object GetImplementation()
        {
            return this.GetImplementation(null);
        }

        internal override object GetImplementation<T>()
        {
            return this.GetImplementation(typeof(T));
        }

        internal object GetImplementation(Type type)
        {   
            if ((type != null) && this.implementation.IsAssignableFrom(type))
            {
                if (this.Lifetime == Lifetime.Transient)
                {
                    return base.Activator.CreateInstance(this.implementation, this.args);
                }
                if ((this.Lifetime == Lifetime.Singleton) && (this.instance != null))
                {
                    return this.instance;
                }
            } 
            else if(this.instance != null)
            {
                return this.instance;
            }
            return base.Activator.CreateInstance(this.implementation);  
        }

        public void ImplementedBy<T>() where T: class
        {
            this.ImplementedBy(typeof(T), Lifetime.Singleton, null);
        }

        public void ImplementedBy<T>(Lifetime lifetime) where T: class
        {
            this.ImplementedBy(typeof(T), lifetime, null);
        }

        public void ImplementedBy<T>(params object[] args) where T: class
        {
            this.ImplementedBy(typeof(T), Lifetime.Singleton, args);
        }

        public void ImplementedBy(object instance)
        {
            if (this.registerType.IsAssignableFrom(instance.GetType()))
            {
                this.instance = instance;
                base.Lifetime = Lifetime.Singleton;
                this.implementation = instance.GetType();
            }
        }

        public void ImplementedBy(Type implementType)
        {
            this.ImplementedBy(implementType, Lifetime.Singleton, null);
        }

        public void ImplementedBy<T>(Lifetime lifetime, params object[] args) where T: class
        {
            this.ImplementedBy(typeof(T), lifetime, args);
        }

        public void ImplementedBy(Type implementType, params object[] args)
        {
            this.ImplementedBy(implementType, Lifetime.Singleton, args);
        }

        public void ImplementedBy(Type implementType, Lifetime lifetime, params object[] args)
        {
            if (this.registerType.IsAssignableFrom(implementType))
            {
                this.args = args;
                base.Lifetime = lifetime;
                this.implementation = implementType;
            }
        }
         
    }
}

