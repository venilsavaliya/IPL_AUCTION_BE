using System;

namespace IplAuction.Entities.Models;

public class InningState
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int InningNumber { get; set; }
    public int? StrikerId { get; set; }
    public int? NonStrikerId { get; set; }
    public int? BowlerId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Match Match { get; set; } = null!; 
    public Player Striker { get; set; } = null!;
    public Player NonStriker { get; set; } = null!;
    public Player Bowler { get; set; } = null!;
} 