namespace IplAuction.Entities.ViewModels.User;

public class UserNameResponseViewModel
{
    public UserNameResponseViewModel() { }

    public UserNameResponseViewModel(Models.User user)
    {
        Id = user.Id;
        FullName = user.FirstName + " " + (user.LastName ?? string.Empty);
    }
    
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
}
