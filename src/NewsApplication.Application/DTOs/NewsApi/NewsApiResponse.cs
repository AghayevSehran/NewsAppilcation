using NewsApplication.Application.DTOs;

namespace NewsApplication.Application.DTOs;

public record NewsApiResponse
{
    public string Status { get; set; }
    public int TotalResults { get; set; }
    public List<ArticleDto> Articles { get; set; }
}
