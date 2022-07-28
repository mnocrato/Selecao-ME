using Core.DTO;
using Core.Repository;
using IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace Teste
{
    public abstract class BaseTest
    {
        protected static Container _container;
        protected Scope _containerScope;
        protected IUnitOfWork _unitOfWork;
        protected PedidoDTO _pedidoPadrao;

        [SetUp]
        public virtual void TestInitialize()
        {
            _pedidoPadrao = new PedidoDTO
            {
                Pedido = "123456",
                Itens = new ItemDTO[]
                {
                    new ItemDTO
                        {
                            Descricao = "Item A",
                            PrecoUnitario = 10,
                            Quantidade = 1
                        },
                    new ItemDTO
                        {
                            Descricao = "Item B",
                            Quantidade = 2,
                            PrecoUnitario = 5
                        }
                }
            };

            _container = IoCBoot.Start();
            _container.Options.AllowOverridingRegistrations = true;
            _container.Options.ResolveUnregisteredConcreteTypes = true;
            _containerScope = AsyncScopedLifestyle.BeginScope(_container);

            _container.Register<IHttpContextAccessor, HttpContextAccessor>(Lifestyle.Singleton);
            _container.Register(() => new SimpleInjectorControllerActivator(_container), Lifestyle.Singleton);
            _container.Register(() => new SimpleInjectorViewComponentActivator(_container), Lifestyle.Singleton);

            _unitOfWork = _container.GetInstance<IUnitOfWork>();

            _unitOfWork.Database.EnsureCreated();
        }


        [TearDown]
        public virtual void TestCleanup()
        {
            _unitOfWork?.Rollback();
            _container.Dispose();
            _containerScope.Dispose();
            _container.Dispose();
        }

        protected (int? statusCode, T items) ValidarRetorno<T>(Task<IActionResult> result)
        {
            if (result.Result is ObjectResult resp && resp.Value is T value)
                return (resp.StatusCode, value);

            throw new NotSupportedException();
        }

        protected int? ValidarRetorno(Task<IActionResult> result)
        {
            switch (result.Result)
            {
                case StatusCodeResult resp:
                    return resp.StatusCode;
                case ConflictObjectResult resp:
                    return resp.StatusCode;
                case NotFoundObjectResult resp:
                    return resp.StatusCode;
                case OkObjectResult resp:
                    return resp.StatusCode;
                case BadRequestObjectResult resp:
                    return resp.StatusCode;
            }

            throw new NotSupportedException();
        }
    }
}
