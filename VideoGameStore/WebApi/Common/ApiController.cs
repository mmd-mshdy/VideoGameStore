using Microsoft.AspNetCore.Mvc;
using VideoGameStore.Domain.common;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected IActionResult FromResult(Result result)
    {
        if (result.IsSuccess)
            return Ok();

        return BadRequest(new
        {
            code = result.Error.Code,
            message = result.Error.Message
        });
    }

    protected IActionResult FromResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(new
        {
            code = result.Error.Code,
            message = result.Error.Message
        });
    }
}