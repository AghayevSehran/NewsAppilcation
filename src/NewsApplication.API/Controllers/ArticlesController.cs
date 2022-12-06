using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsApplication.Application.DTOs;
using NewsApplication.Application.DTOs.Common;
using NewsApplication.Application.Features.Articles.Requests.Queries;
using NewsApplication.Application.Models;

namespace NewsApplication.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticlesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedModel<ArticleDto>>> Get([FromQuery] PagingDto page,
            CancellationToken cancellationToken)
    {
        var leaveAllocations = await _mediator.Send(new GetArticlesRequest() { PageNumber = page.PageNumber, PageSize = page.PageSize }, cancellationToken);
        return Ok(leaveAllocations);
    }
}
