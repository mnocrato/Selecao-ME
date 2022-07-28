using Core.DTO;
using Core.Repository;
using MediatR;
using Newtonsoft.Json;

namespace Core.CommandHandler.Pedido
{
    public class PedidoCommandHandler : HandlerBase,
        IRequestHandler<NovoPedidoCommand, OperationResult>,
        IRequestHandler<AlterarPedidoCommand, OperationResult>,
        IRequestHandler<ExcluirPedidoCommand, OperationResult>,
        IRequestHandler<ObterPedidoCommand, OperationResult<PedidoDTO>>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository) => _pedidoRepository = pedidoRepository;

        public Task<OperationResult> Handle(NovoPedidoCommand request, CancellationToken cancellationToken)
            => CallAction(() => InserirNovoPedido(request));

        public Task<OperationResult> Handle(AlterarPedidoCommand request, CancellationToken cancellationToken)
            => CallAction(() => AlterarPedido(request));

        public Task<OperationResult> Handle(ExcluirPedidoCommand request, CancellationToken cancellationToken)
            => CallAction(() => ExcluirPedido(request));

        public Task<OperationResult<PedidoDTO>> Handle(ObterPedidoCommand request, CancellationToken cancellationToken)
            => CallFunction(() => ObterPedido(request));

        private void InserirNovoPedido(NovoPedidoCommand request)
            => _pedidoRepository.Inserir(new Entidades.Pedido(request.NovoPedido.Pedido, JsonConvert.SerializeObject(request.NovoPedido.Itens)));

        private void AlterarPedido(AlterarPedidoCommand request)
        {
            var pedido = _pedidoRepository.RetornarPorNomePedido(request.NovoPedido.Pedido)
                ?? throw new Exception($"{request.NovoPedido.Pedido} não existe");

            pedido.Nome = request.NovoPedido.Pedido;
            pedido.Itens = JsonConvert.SerializeObject(request.NovoPedido.Itens);

            _pedidoRepository.Atualizar(pedido);
        }

        private void ExcluirPedido(ExcluirPedidoCommand request)
        {
            var pedido = _pedidoRepository.RetornarPorNomePedido(request.Pedido)
                 ?? throw new Exception($"{request.Pedido} não existe");

            _pedidoRepository.Remover(pedido);
        }

        private PedidoDTO ObterPedido(ObterPedidoCommand request)
        {
            var pedido = _pedidoRepository.RetornarPorNomePedido(request.Pedido)
                ?? throw new Exception($"{request.Pedido} não existe");

            return new PedidoDTO { Pedido = pedido.Nome, Itens = JsonConvert.DeserializeObject<ItemDTO[]>(pedido.Itens) };
        }
    }
}