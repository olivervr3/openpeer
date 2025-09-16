using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenPeer.Application.Articles.Dtos;
using OpenPeer.Infrastructure;

namespace OpenPeer.Application.Articles.Queries;

public record GetArticlesPaged(int Page = 1, int PageSize = 10) : IRequest<IReadOnlyList<ArticleDto>>;

public sealed class GetArticlesPagedHandler(AppDbContext db) : IRequestHandler<GetArticlesPaged, IReadOnlyList<ArticleDto>>
{
    public async Task<IReadOnlyList<ArticleDto>> Handle(GetArticlesPaged request, CancellationToken ct)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var size = Math.Clamp(request.PageSize, 1, 100);

        return await db.Articles
            .AsNoTracking()
            .OrderByDescending(a => a.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .Select(a => new ArticleDto(a.Id, a.Title, a.Abstract, a.CreatedAt, a.Status.ToString()))
            .ToListAsync(ct);
    }
}