using IplAuction.Entities.ViewModels.Bid;

namespace IplAuction.Service.Interface;

public interface IBidQueueService
{
    void Enqueue(PlaceBidRequestModel bid);
    bool TryDequeue(out PlaceBidRequestModel bid);
}
