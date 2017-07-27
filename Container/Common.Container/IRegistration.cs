namespace Common.Container
{
    using System;

    public interface IRegistration
    {
        Lifetime Lifetime { get; set; }

        string Name { get; set; }

        System.Type Type { get; set; }
    }
}

