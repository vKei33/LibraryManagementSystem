namespace LibraryApi.Models
{
  public class Member
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public DateTime MembershipStartDate { get; set; }
    public DateTime MembershipExpirationDate { get; set; }
  }
}