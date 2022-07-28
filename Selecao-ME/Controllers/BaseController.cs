using Core;
using Microsoft.AspNetCore.Mvc;

namespace Selecao_ME.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult Result(OperationResult result) => result.IsSuccess ? Ok() : ResultForException(result.Exception);

        protected IActionResult Result<T>(OperationResult<T> result) => result.IsSuccess ? Ok(result.Result) : ResultForException(result.Exception);

        protected IActionResult ResultForException(Exception exception) => BadRequest(exception.Message);
    }
}
