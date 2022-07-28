using Core.DTO;
using Core.Repository;
using Newtonsoft.Json;
using Selecao_ME.Controllers;

namespace Teste
{
    class StatusControllerTest : BaseTest
    {
        private StatusController _controller;

        [SetUp]
        public void ConfiguracaoInicial()
        {
            _controller = _container.GetInstance<StatusController>();
            _container.GetInstance<IPedidoRepository>().Inserir(new Core.Entidades.Pedido(_pedidoPadrao.Pedido, JsonConvert.SerializeObject(_pedidoPadrao.Itens)));
        }

        [Test]
        public void StatusAprovadoTest()
        {
            //Ambiente
            var request = new StatusDTO
            {
                Status = "APROVADO",
                ItensAprovados = 3,
                ValorAprovado = 20,
                Pedido = "123456"
            };

            //Ação
            var respApi = _controller.ObterStatus(request);

            //Assertivas
            var (status, resp) = ValidarRetorno<StatusRespostaDTO>(respApi);
            Assert.That(status, Is.EqualTo(200));
            Assert.Multiple(() =>
            {
                Assert.That(resp.Pedido, Is.EqualTo(_pedidoPadrao.Pedido));
                Assert.That(resp.Status, Is.EqualTo(new List<string>() { "APROVADO" }));
            });
        }

        [Test]
        public void StatusAprovadoValorMenorTest()
        {
            //Ambiente
            var request = new StatusDTO
            {
                Status = "APROVADO",
                ItensAprovados = 3,
                ValorAprovado = 10,
                Pedido = "123456"
            };

            //Ação
            var respApi = _controller.ObterStatus(request);

            //Assertivas
            var (status, resp) = ValidarRetorno<StatusRespostaDTO>(respApi);
            Assert.That(status, Is.EqualTo(200));
            Assert.Multiple(() =>
            {
                Assert.That(resp.Pedido, Is.EqualTo(_pedidoPadrao.Pedido));
                Assert.That(resp.Status, Is.EqualTo(new List<string>() { "APROVADO_VALOR_A_MENOR" }));
            });
        }

        [Test]
        public void StatusMultiplosAprovadoTest()
        {
            //Ambiente
            var request = new StatusDTO
            {
                Status = "APROVADO",
                ItensAprovados = 4,
                ValorAprovado = 21,
                Pedido = "123456"
            };

            //Ação
            var respApi = _controller.ObterStatus(request);

            //Assertivas
            var (status, resp) = ValidarRetorno<StatusRespostaDTO>(respApi);
            Assert.That(status, Is.EqualTo(200));
            Assert.Multiple(() =>
            {
                Assert.That(resp.Pedido, Is.EqualTo(_pedidoPadrao.Pedido));
                Assert.That(resp.Status, Is.EqualTo(new List<string>() { "APROVADO_VALOR_A_MAIOR", "APROVADO_QTD_A_MAIOR" }));
            });
        }

        [Test]
        public void StatusAprovadoQuantidadeMenorTest()
        {
            //Ambiente
            var request = new StatusDTO
            {
                Status = "APROVADO",
                ItensAprovados = 2,
                ValorAprovado = 20,
                Pedido = "123456"
            };

            //Ação
            var respApi = _controller.ObterStatus(request);

            //Assertivas
            var (status, resp) = ValidarRetorno<StatusRespostaDTO>(respApi);
            Assert.That(status, Is.EqualTo(200));
            Assert.Multiple(() =>
            {
                Assert.That(resp.Pedido, Is.EqualTo(_pedidoPadrao.Pedido));
                Assert.That(resp.Status, Is.EqualTo(new List<string>() { "APROVADO_QTD_A_MENOR" }));
            });
        }

        [Test]
        public void StatusReprovadoTest()
        {
            //Ambiente
            var request = new StatusDTO
            {
                Status = "REPROVADO",
                ItensAprovados = 0,
                ValorAprovado = 0,
                Pedido = "123456"
            };

            //Ação
            var respApi = _controller.ObterStatus(request);

            //Assertivas
            var (status, resp) = ValidarRetorno<StatusRespostaDTO>(respApi);
            Assert.That(status, Is.EqualTo(200));
            Assert.Multiple(() =>
            {
                Assert.That(resp.Pedido, Is.EqualTo(_pedidoPadrao.Pedido));
                Assert.That(resp.Status, Is.EqualTo(new List<string>() { "REPROVADO" }));
            });
        }

        [Test]
        public void StatusCodigoInvalidoTest()
        {
            //Ambiente
            var request = new StatusDTO
            {
                Status = "APROVADO",
                ItensAprovados = 3,
                ValorAprovado = 20,
                Pedido = "123456-N"
            };

            //Ação
            var respApi = _controller.ObterStatus(request);

            //Assertivas
            var (status, resp) = ValidarRetorno<StatusRespostaDTO>(respApi);
            Assert.That(status, Is.EqualTo(200));
            Assert.Multiple(() =>
            {
                Assert.That(resp.Pedido, Is.EqualTo(request.Pedido));
                Assert.That(resp.Status, Is.EqualTo(new List<string>() { "CODIGO_PEDIDO_INVALIDO" }));
            });
        }
    }
}
