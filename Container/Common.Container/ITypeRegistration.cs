namespace Common.Container
{
    using System;

    public interface ITypeRegistration
    {
        void ImplementedBy<T>() where T: class;
        void ImplementedBy<T>(params object[] args) where T: class;
        void ImplementedBy<T>(Lifetime lifetime) where T: class;
        void ImplementedBy(object instance);
        void ImplementedBy(Type implementType);
        void ImplementedBy<T>(Lifetime lifetime, params object[] args) where T: class;
        void ImplementedBy(Type implementType, params object[] args);
        void ImplementedBy(Type implementType, Lifetime lifetime, params object[] args);
    }
}

