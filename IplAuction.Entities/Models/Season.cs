namespace IplAuction.Entities.Models;

public class Season
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Auction> Auctions { get; set; } = [];

    public ICollection<Match> Matches { get; set; } = [];
}
