using Core.DTO;
using MediatR;

namespace Core.CommandHandler.Status
{
    public class StatusCommand : IRequest<OperationResult<StatusRespostaDTO>>
    {
        public StatusCommand(StatusDTO statusPedido) => StatusPedido = statusPedido;

        public StatusDTO StatusPedido { get; }
    }
}