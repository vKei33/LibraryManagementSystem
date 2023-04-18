using Marten;
using MediatR;

namespace LibraryApi.Models.Members
{
  public class CreateNewMember
  {
    public class Request : IRequest<Response>
    {
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
        var item = new Member();
        item.FirstName = request.FirstName;
        item.LastName = request.LastName;
        item.PhoneNumber = request.PhoneNumber;
        item.MembershipStartDate = DateTime.Today.Date;
        item.MembershipExpirationDate = DateTime.Today.AddYears(1).Date;

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