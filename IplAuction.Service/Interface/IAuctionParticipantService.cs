namespace IplAuction.Service.Interface;

public interface IAuctionParticipantService
{
    Task AddParticipantAsync(int auctionId, int userId);
}
