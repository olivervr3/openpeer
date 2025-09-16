using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenPeer.Application.Articles.Dtos;
using OpenPeer.Infrastructure;

namespace OpenPeer.Application.Articles.Queries;

public record GetArticleById(int Id) : IRequest<ArticleDto?>;

public sealed class GetArticleByIdHandler(AppDbContext db) : IRequestHandler<GetArticleById, ArticleDto?>
{
    public async Task<ArticleDto?> Handle(GetArticleById request, CancellationToken ct)
    {
        return await db.Articles
            .Where(a => a.Id == request.Id)
            .Select(a => new ArticleDto(a.Id, a.Title, a.Abstract, a.CreatedAt, a.Status.ToString()))
            .FirstOrDefaultAsync(ct);
    }
}