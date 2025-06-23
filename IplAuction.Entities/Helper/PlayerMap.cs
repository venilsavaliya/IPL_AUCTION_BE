using CsvHelper.Configuration;

namespace IplAuction.Entities.ViewModels.Player;

public class PlayerMap : ClassMap<AddPlayerRequest>
{
    public PlayerMap()
    {
        Map(p => p.Name).Name("PlayerName");

        Map(p => p.BasePrice).Name("Price");
    }
}
