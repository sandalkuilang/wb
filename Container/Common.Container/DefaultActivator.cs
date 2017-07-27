namespace Common.Container
{
    using System;

    public class DefaultActivator : IActivator
    {
        public object CreateInstance<T>()
        {
            return Activator.CreateInstance(typeof(T));
        }

        public object CreateInstance(Type type)
        {
            if (type == null)
                return null;
            return Activator.CreateInstance(type);
        }

        public object CreateInstance(string assemblyName, string typeName)
        {
            if (string.IsNullOrEmpty(assemblyName) && string.IsNullOrEmpty(typeName))
                return null;
            return Activator.CreateInstance(assemblyName, typeName);
        }

        public object CreateInstance(Type type, params object[] args)
        {
            if (type == null)
                return null;
            return Activator.CreateInstance(type, args);
        }
    }
}

