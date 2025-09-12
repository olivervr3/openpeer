namespace OpenPeer.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;
    public string Reviewer { get; set; } = null!;
    public int Score { get; set; } // 1..5
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
