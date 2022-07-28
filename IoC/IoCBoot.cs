using Core.Repository;
using Data.Repository.Context;
using MediatR;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Reflection;

namespace IoC
{
    public static class IoCBoot
    {
        private static Container _container;

        public static Container Start()
        {
            InitContainer();
            return _container;
        }

        private static void InitContainer()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            BuildMediator(_container);

            _container.Register(typeof(IPedidoRepository), typeof(PedidoRepository), Lifestyle.Scoped);
            _container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Scoped);

            _container.Register<IUnitOfWork, MEContext>(Lifestyle.Scoped);
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
            yield return typeof(IRepository<>).GetTypeInfo().Assembly;
        }

        private static void BuildMediator(Container container)
        {
            var assemblies = GetAssemblies().ToArray();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IRequestHandler<>), assemblies);
            container.Collection.Register(typeof(INotificationHandler<>), assemblies);
            container.Collection.Register(typeof(IPipelineBehavior<,>), assemblies);
            container.RegisterInstance(new ServiceFactory(container.GetInstance));
        }
    }
}
