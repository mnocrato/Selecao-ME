using Core.DTO;
using Core.Repository;
using Newtonsoft.Json;
using Selecao_ME.Controllers;

namespace Teste
{
    class PedidoControllerTest : BaseTest
    {
        private PedidoController _controller;

        [SetUp]
        public void ConfiguracaoInicial() => _controller = _container.GetInstance<PedidoController>();

        [Test]
        public void InserirPedidoTest()
        {
            //Ação
            var respApi = _controller.NovoPedido(_pedidoPadrao);

            //Assertivas
            var status = ValidarRetorno(respApi);
            Assert.That(status, Is.EqualTo(200));

            var pedidoPersistido = _container.GetInstance<IPedidoRepository>().RetornarPorNomePedido(_pedidoPadrao.Pedido);
            Assert.That(pedidoPersistido, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(pedidoPersistido.Nome, Is.EqualTo(_pedidoPadrao.Pedido));
                Assert.That(JsonConvert.DeserializeObject<ItemDTO[]>(pedidoPersistido.Itens), Is.EqualTo(_pedidoPadrao.Itens));
            });
        }

        [Test]
        public void InserirPedidoDuplicadoTest()
        {
            var pedidoRepository = _container.GetInstance<IPedidoRepository>();

            //Ambiente
            pedidoRepository.Inserir(new Core.Entidades.Pedido(_pedidoPadrao.Pedido, JsonConvert.SerializeObject(_pedidoPadrao.Itens)));

            //Ação
            var respApi = _controller.NovoPedido(_pedidoPadrao);

            //Assertivas
            var (status, resp) = ValidarRetorno<string>(respApi);
            Assert.That(status, Is.EqualTo(400));
            Assert.Multiple(() =>
            {
                Assert.That(status, Is.EqualTo(400));
                Assert.That(resp, Is.EqualTo("Pedido Duplicado"));
            });
        }

        [Test]
        public void ObterPedidoTest()
        {
            //Ambiente
            _container.GetInstance<IPedidoRepository>().Inserir(new Core.Entidades.Pedido(_pedidoPadrao.Pedido, JsonConvert.SerializeObject(_pedidoPadrao.Itens)));

            //Ação
            var respApi = _controller.ObterPedido(_pedidoPadrao.Pedido);

            //Assertivas
            var (status, resp) = ValidarRetorno<PedidoDTO>(respApi);
            Assert.Multiple(() =>
            {
                Assert.That(status, Is.EqualTo(200));
                Assert.That(resp.Pedido, Is.EqualTo(_pedidoPadrao.Pedido));
                Assert.That(resp.Itens, Is.EqualTo(_pedidoPadrao.Itens));
            });
        }

        [Test]
        public void ObterPedidoNaoExistenteTest()
        {
            //Ação
            var respApi = _controller.ObterPedido(_pedidoPadrao.Pedido);

            //Assertivas
            var (status, resp) = ValidarRetorno<string>(respApi);
            Assert.That(status, Is.EqualTo(400));
            Assert.Multiple(() =>
            {
                Assert.That(status, Is.EqualTo(400));
                Assert.That(resp, Is.EqualTo($"{_pedidoPadrao.Pedido} não existe"));
            });
        }

        [Test]
        public void ExcluirPedidoTest()
        {
            var pedidoRepository = _container.GetInstance<IPedidoRepository>();

            //Ambiente
            _container.GetInstance<IPedidoRepository>().Inserir(new Core.Entidades.Pedido(_pedidoPadrao.Pedido, JsonConvert.SerializeObject(_pedidoPadrao.Itens)));

            //Ação
            var respApi = _controller.ExcluirPedido(_pedidoPadrao.Pedido);

            //Assertivas
            var status = ValidarRetorno(respApi);
            var pedidoPersistido = pedidoRepository.RetornarPorNomePedido(_pedidoPadrao.Pedido);
            Assert.Multiple(() =>
            {
                Assert.That(status, Is.EqualTo(200));
                Assert.That(pedidoPersistido, Is.Null);
            });
        }

        [Test]
        public void AlterarPedidoTest()
        {
            var pedidoRepository = _container.GetInstance<IPedidoRepository>();

            //Ambiente
            pedidoRepository.Inserir(new Core.Entidades.Pedido(_pedidoPadrao.Pedido, JsonConvert.SerializeObject(_pedidoPadrao.Itens)));

            var pedidoAlterado = _pedidoPadrao;
            pedidoAlterado.Itens = new ItemDTO[]
                {
                    new ItemDTO
                        {
                            Descricao = "Item A",
                            PrecoUnitario = 10,
                            Quantidade = 1
                        }
                };

            //Ação
            var respApi = _controller.AlterarPedido(pedidoAlterado);

            //Assertivas
            var status = ValidarRetorno(respApi);
            var pedidoPersistido = pedidoRepository.RetornarPorNomePedido(pedidoAlterado.Pedido);
            Assert.That(pedidoPersistido, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(status, Is.EqualTo(200));
                Assert.That(pedidoPersistido.Nome, Is.EqualTo(_pedidoPadrao.Pedido));
                Assert.That(JsonConvert.DeserializeObject<ItemDTO[]>(pedidoPersistido.Itens), Is.Not.EqualTo(_pedidoPadrao.Itens));
            });
        }
    }
}
