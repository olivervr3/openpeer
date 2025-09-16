namespace OpenPeer.Application.Articles.Dtos;

public record ArticleDto(int Id, string Title, string Abstract, DateTime CreatedAt, string Status);