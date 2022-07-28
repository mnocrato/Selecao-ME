using Core.DTO;
using MediatR;

namespace Core.CommandHandler.Pedido
{
    public class ObterPedidoCommand : IRequest<OperationResult<PedidoDTO>>
    {
        public ObterPedidoCommand(string pedido) => Pedido = pedido;

        public string Pedido { get; }
    }
}