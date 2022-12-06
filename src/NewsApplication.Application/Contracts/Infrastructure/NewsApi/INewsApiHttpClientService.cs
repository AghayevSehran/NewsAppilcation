using NewsApplication.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApplication.Application.Contracts.Infrastructure.NewsApi
{
    public interface INewsApiHttpClientService
    {
        Task<NewsApiResponse> Execute();
    }
}
