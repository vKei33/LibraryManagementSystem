using Marten;
using MediatR;

namespace LibraryApi.Models.Members
{
  public class UpdateMember
  {
    public class Request : IRequest<Response>
    {
      public Guid Id { get; set; }
      public string FirstName { get; set; } = "";
      public string LastName { get; set; } = "";
      public string PhoneNumber { get; set; } = "";
      public DateTime MembershipStartDate { get; set; }
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
        var member = await _session.Query<Member>().FirstOrDefaultAsync(e => e.Id == request.Id);

        if (member == null)
        {
          throw new ArgumentNullException("Member doesn't exist.");
        }

        member.FirstName = request.FirstName;
        member.LastName = request.LastName;
        member.PhoneNumber = request.PhoneNumber;
        member.MembershipStartDate = request.MembershipStartDate.Date;
        member.MembershipExpirationDate = request.MembershipStartDate.Date.AddYears(1);

        _session.Update(member);
        await _session.SaveChangesAsync();

        var response = new Response()
        {
          Id = member.Id
        };

        return response;
      }
    }
  }
}