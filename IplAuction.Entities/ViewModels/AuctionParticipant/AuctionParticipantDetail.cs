public class AuctionParticipantDetail
{
    public int Id { get; set; }
    public int AuctionId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public int Points { get; set; }
    public int Rank { get; set; }
    public int TotalPlayers { get; set; }
}