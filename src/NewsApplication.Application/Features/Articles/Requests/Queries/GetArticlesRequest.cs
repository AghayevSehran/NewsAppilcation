using MediatR;
using NewsApplication.Application.DTOs;
using NewsApplication.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApplication.Application.Features.Articles.Requests.Queries
{
    public  class GetArticlesRequest : IRequest<PagedModel<ArticleDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
