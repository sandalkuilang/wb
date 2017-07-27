namespace Common.Container
{
    using System;

    public interface IActivator
    {
        object CreateInstance<T>();
        object CreateInstance(Type type);
        object CreateInstance(string assemblyName, string typeName);
        object CreateInstance(Type type, params object[] args);
    }
}

