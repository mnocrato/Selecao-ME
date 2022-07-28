using Core.DTO;
using Core.Repository;
using MediatR;
using Newtonsoft.Json;

namespace Core.CommandHandler.Status
{
    public class StatusCommandHandler : HandlerBase,
        IRequestHandler<StatusCommand, OperationResult<StatusRespostaDTO>>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public StatusCommandHandler(IPedidoRepository pedidoRepository) => _pedidoRepository = pedidoRepository;

        public Task<OperationResult<StatusRespostaDTO>> Handle(StatusCommand request, CancellationToken cancellationToken)
            => CallFunction(() => ObterStatusPedido(request));

        private StatusRespostaDTO ObterStatusPedido(StatusCommand request)
        {
            var statusResposta = new StatusRespostaDTO { Pedido = request.StatusPedido.Pedido, Status = new List<string>() };

            var pedido = _pedidoRepository.RetornarPorNomePedido(request.StatusPedido.Pedido);

            if (pedido == null)
                statusResposta.Status.Add("CODIGO_PEDIDO_INVALIDO");
            else
            {
                switch (request.StatusPedido.Status)
                {
                    case "REPROVADO":
                        statusResposta.Status.Add("REPROVADO");
                        break;
                    case "APROVADO":
                        statusResposta.Status.AddRange(ObterTiposStatusAprovado(request.StatusPedido, pedido));
                        break;
                    default:
                        throw new Exception("Status informado no request inválido");
                }
            }

            return statusResposta;
        }

        private List<string> ObterTiposStatusAprovado(StatusDTO statusPedido, Entidades.Pedido pedido)
        {
            var itensPedido = JsonConvert.DeserializeObject<ItemDTO[]>(pedido.Itens);
            var qtdItensPedido = itensPedido.Sum(s => s.Quantidade);
            var valorItensPedido = itensPedido.Sum(s => s.Quantidade * s.PrecoUnitario);

            var statusAprovados = new List<string>();

            if (statusPedido.ValorAprovado < valorItensPedido)
                statusAprovados.Add("APROVADO_VALOR_A_MENOR");
            else if (statusPedido.ValorAprovado > valorItensPedido)
                statusAprovados.Add("APROVADO_VALOR_A_MAIOR");

            if (statusPedido.ItensAprovados < qtdItensPedido)
                statusAprovados.Add("APROVADO_QTD_A_MENOR");
            else if (statusPedido.ItensAprovados > qtdItensPedido)
                statusAprovados.Add("APROVADO_QTD_A_MAIOR");

            if (!statusAprovados.Any())
                statusAprovados.Add("APROVADO");

            return statusAprovados;
        }
    }
}