namespace IplAuction.Entities.Models;

public class SeasonTeamPlayer
{
    public int Id { get; set; }
    public int SeasonId { get; set; }
    public int TeamId { get; set; }
    public int PlayerId { get; set; }
}   
