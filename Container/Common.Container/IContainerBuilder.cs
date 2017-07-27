namespace Common.Container
{
    using System;

    public interface IContainerBuilder
    {
        IContainer Build();
        ITypeRegistration Register<T>();
        void Unregister<T>();
    }
}

