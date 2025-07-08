namespace IplAuction.Entities;


public static class UserRoles
{
    public const string Admin = "Admin";
    public const string Operator = "Operator";
    public const string User = "User";
}

public static class Messages
{
    public const string UnexpectedError = "Unexpected Error Occured";
    public const string BadRequest = "Can Not Process Request";
    public const string UnAuthorize = "User Is UnAuthorize";
    public const string InvalidUserRole = "Invalid User Role For Assignment";
    public const string UserNotFound = "User Not Found";
    public const string AccountCredential = "Account Credentials";
    public const string UserCreated = "User Created Successfully";
    public const string UserUpdated = "User Updated Successfully";
    public const string UserDeleted = "User Deleted Successfully";
    public const string InvalidCredentials = "Invalid Credentials";
    public const string LoggedIn = "Logged In Successfully";
    public const string LoggedOut = "Logged Out Successfully";
    public const string InvalidOrExpiredToken = "Invalid or Expired Token";
    public const string TokenRefreshed = "Token Refreshed Successfully";
    public const string EmailAlreadyExisted = "Email Already Existed";
    public const string InternalServerError = "Internal Server Error";
    public const string UserFetched = "User Fetched Successfully";
    public const string PasswordChanged = "Password Changed Successfully";
    public const string ResetPasswordEmailSent = "Reset Password Link Sent to Email";
    public const string PlayerCreated = "Player Created Successfully";
    public const string PlayerUpdated = "Player Updated Successfully";
    public const string PlayerDeleted = "Player Deleted Successfully";
    public const string PlayerFetched = "Player(s) Fetched Successfully";
    public const string PlayerNotFound = "Player(s) Not Found";
    public const string PlayerAddedToAuction = "Player Added To Auction Successfully";
    public const string PlayerRemovedFromAuction = "Player Removed From Auction Successfully";
    public const string NoPlayerLeft = "Player(s) Not Left For Auction";
    public const string TeamNotFound = "Team(s) Not Found";
    public const string NotFound = "{0} Not Found";
    public const string AlreadyExisted = "{0} Already Existed";
    public const string TeamCreated = "Team Created Successfully";
    public const string TeamNotCreated = "Team Created Successfully";
    public const string TeamFetched = "Team(s) Fetched Successfully";
    public const string TeamUpdated = "Team(s) Updated Successfully";
    public const string TeamDeleted = "Team(s) Fetched Successfully";
    public const string AuctionCreated = "Auction Created Successfully";
    public const string AuctionUpdated = "Auction Updated Successfully";
    public const string AuctionDeleted = "Auction Deleted Successfully";
    public const string AuctionFetched = "Auction(s) Fetched Successfully";
    public const string AuctionNotFound = "Auction(s) Not Found";
    public const string AuctionJoined = "Auction Joined Successfully";
    public const string CanNotJoinAuction = "You Can Not Join This Auction";
    public const string ManagerAlreadyAssigned = "Manager Is Already Assigned To Another Auction";
    public const string AuctionNotLive = "Auction Is Not Live";
    public const string PlayerAlreadySold = "Player Is Already Sold";
    public const string BidMustHigher = "Bid Should be Higher Than Current Bid";
    public const string BidAdded = "Bid Added Successfully";
    public const string BidWillProcess = "Bid Will be Processed";
    public const string InsufficientBalance = "You Don't Have Sufficient Balance";
    public const string RegistrationOver = "Registration Time Is Over";

    public const string Congratulations = "Congratulations!";
    
    public const string PlayerSoldToUser = "{0} has been sold to you";
}

public static class JwtClaims
{
    public const string Id = "Id";
    public const string Email = "Email";
    public const string Role = "Role";
    public const string Name = "Name";
}