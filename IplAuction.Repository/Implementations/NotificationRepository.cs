using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class NotificationRepository(IplAuctionDbContext context) : GenericRepository<Notification>(context), INotificationRepository
{
}
