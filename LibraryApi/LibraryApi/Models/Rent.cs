using Marten.Schema;

namespace LibraryApi.Models
{
  public class Rent
  {
    public Guid Id { get; set; }

    [ForeignKey(typeof(Book))]
    public Guid? BookId { get; set; }

    [ForeignKey(typeof(Member))]
    public Guid? MemberId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";

    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public bool IsReturned { get; set; }
  }
}