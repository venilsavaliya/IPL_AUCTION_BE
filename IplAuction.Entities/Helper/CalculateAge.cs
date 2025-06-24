namespace IplAuction.Entities.Helper;

public class CalculateAge
{
    public static int CalculateAgeFromDbo(DateOnly dob)
    {
        return DateOnly.FromDateTime(DateTime.UtcNow).Year - dob.Year -
        (DateOnly.FromDateTime(DateTime.UtcNow) < dob.AddYears(DateOnly.FromDateTime(DateTime.UtcNow).Year - dob.Year) ? 1 : 0);
    }
}
