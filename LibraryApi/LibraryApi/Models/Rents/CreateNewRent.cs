using Marten;
using MediatR;

namespace LibraryApi.Models.Rents
{
  public class CreateNewRent
  {
    public class Request : IRequest<Response>
    {
      public Guid BookId { get; set; }
      public Guid? MemberId { get; set; }
      public string FirstName { get; set; } = "";
      public string LastName { get; set; } = "";
      public string PhoneNumber { get; set; } = "";
    }

    public class Response
    {
      public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Request, Response>
    {
      private readonly IDocumentSession _session;

      public Handler(IDocumentSession session)
      {
        _session = session;
      }

      public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
      {
        var item = new Rent();
        item.BookId = request.BookId;
        item.MemberId = request.MemberId;
        if (!item.MemberId.HasValue)
        {
          item.FirstName = request.FirstName;
          item.LastName = request.LastName;
          item.PhoneNumber = request.PhoneNumber;
        }
        else
        {
          var member = await _session.Query<Member>().FirstOrDefaultAsync(e => e.Id == item.MemberId);
          if (member == null)
          {
            throw new ArgumentNullException("Member doesn't exist.");
          }
          item.FirstName = member.FirstName;
          item.LastName = member.LastName;
          item.PhoneNumber = member.PhoneNumber;
        }
        item.RentalDate = DateTime.Today.Date;
        item.ReturnDate = DateTime.Today.AddDays(14).Date;
        item.IsReturned = false;

        _session.Store(item);
        await _session.SaveChangesAsync();

        var response = new Response()
        {
          Id = item.Id
        };

        return response;
      }
    }
  }
}