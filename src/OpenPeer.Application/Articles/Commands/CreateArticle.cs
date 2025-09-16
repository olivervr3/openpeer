using MediatR;
using OpenPeer.Domain.Entities; 
using OpenPeer.Infrastructure;

namespace OpenPeer.Application.Articles.Commands;   

public record CreateArticle(string Title, string Abstract) : IRequest<int>;

public sealed class CreateArticleHandler(AppDbContext db) : IRequestHandler<CreateArticle, int>
{
    public async Task<int> Handle(CreateArticle request, CancellationToken ct)
    {
        var entity = new Article
        {
            Title = request.Title,
            Abstract = request.Abstract
        };

        db.Articles.Add(entity);
        await db.SaveChangesAsync(ct);
        return entity.Id;
    }
}