using Core.CommandHandler.Status;
using Core.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Selecao_ME.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : BaseController
    {
        private readonly IMediator _mediator;

        public StatusController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Status([FromBody] StatusDTO status)
            => Result(await _mediator.Send(new StatusCommand(status)));
    }
}