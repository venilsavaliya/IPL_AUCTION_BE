using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IplAuction.Entities.Models;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Token { get; set; } = null!;

    [Required]
    public DateTime Expires { get; set; }

    public DateTime? Revoked { get; set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsRevoked => Revoked != null;
    public bool IsActive => !IsRevoked && !IsExpired;

    [Required]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}