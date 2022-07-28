using Core.CommandHandler.Pedido;
using Core.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Selecao_ME.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : BaseController
    {
        private readonly IMediator _mediator;

        public PedidoController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> NovoPedido([FromBody] PedidoDTO pedido)
            => Result(await _mediator.Send(new NovoPedidoCommand(pedido)));

        [HttpDelete]
        public async Task<IActionResult> ExcluirPedido(string pedido)
            => Result(await _mediator.Send(new ExcluirPedidoCommand(pedido)));

        [HttpGet]
        public async Task<IActionResult> ObterPedido(string pedido)
            => Result(await _mediator.Send(new ObterPedidoCommand(pedido)));

        [HttpPut]
        public async Task<IActionResult> AlterarPedido(PedidoDTO pedido)
            => Result(await _mediator.Send(new AlterarPedidoCommand(pedido)));
    }
}