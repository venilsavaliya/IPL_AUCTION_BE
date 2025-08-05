public class PlayerCsvModel
{
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Role { get; set; } = null!;
    public int? BasePrice { get; set; }
    public DateOnly? DateOfBirth { get; set; }  
    public string? ImageUrl { get; set; }       
}
