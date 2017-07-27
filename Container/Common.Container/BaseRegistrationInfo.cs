namespace Common.Container
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class BaseRegistrationInfo : IRegistration
    {  
        private const string interfacesDisposable = "System.IDisposable";

        public IActivator Activator { get; set; }
        public Lifetime Lifetime { get; set; }
        public Type Type { get; set; } 
        public string Name { get; set; }

        protected BaseRegistrationInfo()
        {
        }

        public virtual void Dispose()
        {
            object implementation = this.GetImplementation();
            this.DisposeImplementation(implementation);
        }

        private void DisposeImplementation(object instance)
        {
            if (instance != null)
            {
                System.Type type = instance.GetType();
                if (type.GetInterface("System.IDisposable", true) != null)
                {
                    type.InvokeMember("Dispose", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, instance, null);
                }
            }
        }

        internal abstract object GetImplementation();
        internal abstract object GetImplementation<T>();
          
    }
}

