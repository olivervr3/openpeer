namespace OpenPeer.Domain.Entities;

public enum Decision { Accept, Reject, Revise }

public class EditorialDecision
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;
    public Decision Decision { get; set; }
    public string? Notes { get; set; }
    public DateTime DecidedAt { get; set; } = DateTime.UtcNow;
}
