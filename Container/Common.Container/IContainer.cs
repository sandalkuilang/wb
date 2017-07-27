namespace Common.Container
{
    using System;

    public interface IContainer
    {
        ITypeRegistration Register<T>(); 
        T Resolve<T>();
        void Unregister<T>(); 
    }
}

