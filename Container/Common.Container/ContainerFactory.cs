namespace Common.Container
{
    using System;

    public class ContainerFactory : IContainerBuilder
    {
        private readonly Common.Container.Container container = new Common.Container.Container();

        private ContainerFactory()
        {
        }

        public IContainer Build()
        {
            return this.container.Build();
        }

        public static IContainerBuilder CreateContainer()
        {
            return new ContainerFactory();
        }

        public ITypeRegistration Register<T>()
        {
            return this.container.Register<T>();
        }

        public void Unregister<T>()
        {
            this.container.Unregister<T>();
        }
    }
}

