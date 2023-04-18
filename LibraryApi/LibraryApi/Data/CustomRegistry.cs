using LibraryApi.Models;
using Marten;
using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Data
{
  public class CustomRegistry : MartenRegistry
  {
    public CustomRegistry()
    {
      For<IdentityUser>().Duplicate(x => x.Email);
      For<IdentityUser>().Duplicate(x => x.UserName);

      For<Genre>().Duplicate(x => x.Name);

      For<Publisher>().Duplicate(x => x.Name);

      For<Author>().Duplicate(x => x.FirstName);
      For<Author>().Duplicate(x => x.LastName);

      For<Member>().Duplicate(x => x.FirstName);
      For<Member>().Duplicate(x => x.LastName);
      For<Member>().Duplicate(x => x.MembershipStartDate);
      For<Member>().Duplicate(x => x.MembershipExpirationDate);

      For<Book>().Duplicate(x => x.Title);
      For<Book>().Duplicate(x => x.Description);
      For<Book>().Duplicate(x => x.Stock);
      For<Book>().Duplicate(x => x.Language);
      For<Book>().Duplicate(x => x.Available);

      For<Rent>().Duplicate(x => x.FirstName);
      For<Rent>().Duplicate(x => x.LastName);
      For<Rent>().Duplicate(x => x.PhoneNumber);
      For<Rent>().Duplicate(x => x.RentalDate);
      For<Rent>().Duplicate(x => x.ReturnDate);
      For<Rent>().Duplicate(x => x.IsReturned);
    }
  }
}