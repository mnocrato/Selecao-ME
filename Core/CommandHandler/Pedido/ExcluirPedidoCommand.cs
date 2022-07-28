using MediatR;

namespace Core.CommandHandler.Pedido
{
    public class ExcluirPedidoCommand : IRequest<OperationResult>
    {
        public ExcluirPedidoCommand(string pedido) => Pedido = pedido;

        public string Pedido { get; }
    }
}