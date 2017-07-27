namespace Common.Container
{
    using System;
    using System.Runtime.CompilerServices;

    public class Container : IContainer
    { 
        private IActivator activator; 
        private string name; 
        private IRegistrationStorage<Type, ITypeRegistration> registration;

        public Container() : this("Container")
        {
            this.activator = new DefaultActivator();
            this.registration = new ConcurrentStorage(this.activator);
        }

        public Container(string name)  
        {
            this.name = name; 
            this.activator = new DefaultActivator();
            this.registration = new ConcurrentStorage(this.activator);
        }

        public IContainer Build()
        {
            return this;
        }

        internal void Dispose(Type type)
        {
            ITypeRegistration reg = this.registration.Get(type.FullName);
            if (reg == null)
                return;
            
            (reg as BaseRegistrationInfo).Dispose();
        }

        public ITypeRegistration Register<T>()
        {
            return this.registration.Add(typeof(T));
        }

        public T Resolve<T>()
        {
            ITypeRegistration typeRegistration = this.registration.Get<T>();
            if (typeRegistration != null)
            {
                return (T)(typeRegistration as BaseRegistrationInfo).GetImplementation();
            }
            return default(T);
        }

        public void Unregister<T>()
        {
            Type type = typeof(T);
            this.Dispose(type);
            this.registration.Remove(type);
        } 
         
    }
}

