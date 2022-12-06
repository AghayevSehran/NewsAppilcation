using AutoMapper;
using MediatR;
using NewsApplication.Application.Contracts.Persistence;
using NewsApplication.Application.Contracts.Service;
using NewsApplication.Application.DTOs;
using NewsApplication.Application.DTOs.Common;
using NewsApplication.Application.Features.Articles.Requests.Queries;
using NewsApplication.Application.Models;
using NewsApplication.Domain;

namespace NewsApplication.Application.Features.Articles.Handlers.Queries;

public class GetArticlesRequestHandler : IRequestHandler<GetArticlesRequest, PagedModel<ArticleDto>>
{
    IPageService<Article,DateTime> _pageService;
    IGenericRepository<Article> _genericRepository;
    IMapper _mapper;
    public GetArticlesRequestHandler(IPageService<Article,DateTime> pageService,IMapper mapper,IGenericRepository<Article> genericRepository)
    {
        _pageService = pageService;
        _genericRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<PagedModel<ArticleDto>> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
    {       
        var paggingDto = new PagingDto() { PageNumber = request.PageNumber, PageSize = request.PageSize };
        var data  = await   _pageService.GetPagedList(_genericRepository.GetQuerable(),
             c=>c.PublishedAt,paggingDto, cancellationToken);
        var response  = _mapper.Map<PagedModel<ArticleDto>>(data);
        return response;
    }
}
