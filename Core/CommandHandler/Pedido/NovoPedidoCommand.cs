using Core.DTO;
using MediatR;

namespace Core.CommandHandler.Pedido
{
    public class NovoPedidoCommand : IRequest<OperationResult>
    {
        public NovoPedidoCommand(PedidoDTO novoPedido) => NovoPedido = novoPedido;

        public PedidoDTO NovoPedido { get; }
    }
}