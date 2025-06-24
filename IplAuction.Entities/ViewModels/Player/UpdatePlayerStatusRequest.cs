namespace IplAuction.Entities.ViewModels.Player;

public class UpdatePlayerStatusRequest
{   
    public int PlayerId { get; set; }
    public bool Status { get; set; }
}
