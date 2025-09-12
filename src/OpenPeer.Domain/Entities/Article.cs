namespace OpenPeer.Domain.Entities;

public enum ArticleStatus { Submitted, UnderReview, DecisionPending, Approved, Rejected}

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Abstract { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    public ArticleStatus Status { get; set; } = ArticleStatus.Submitted;
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public EditorialDecision? EditorialDecision { get; set; }
}