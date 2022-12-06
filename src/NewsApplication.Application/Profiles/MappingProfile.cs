using AutoMapper;
using NewsApplication.Application.DTOs;
using NewsApplication.Application.Models;
using NewsApplication.Domain;

namespace NewsApplication.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
    
        CreateMap<Article, ArticleDto>().ReverseMap();
        CreateMap<PagedModel<Article>, PagedModel<ArticleDto>>();
        CreateMap<NewsSource, SourceDto>().ReverseMap();
    }
}
