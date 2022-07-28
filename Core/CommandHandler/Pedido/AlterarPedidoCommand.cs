using Core.DTO;
using MediatR;

namespace Core.CommandHandler.Pedido
{
    public class AlterarPedidoCommand : IRequest<OperationResult>
    {
        public AlterarPedidoCommand(PedidoDTO novoPedido) => NovoPedido = novoPedido;

        public PedidoDTO NovoPedido { get; }
    }
}