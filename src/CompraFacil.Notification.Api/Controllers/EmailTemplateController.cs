using CompraFacil.Notification.Application.Common.Models;
using CompraFacil.Notification.Application.Handlers.v1.CreateTemplate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompraFacil.Notification.Api.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/email-template")]
[ApiController]
public class EmailTemplateController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmailTemplateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<CreateEmailTemplateResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(
        CreateEmailTemplateCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response is not null ? CreatedAtAction(nameof(Post), response) : NoContent();
    }
}
