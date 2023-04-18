using Marten;
using MediatR;

namespace LibraryApi.Models.Members
{
  public class DeleteMember
  {
    public class Request : IRequest<Response>
    {
      public Guid Id { get; set; }
    }

    public class Response { }

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

        _session.Delete(member);
        await _session.SaveChangesAsync();

        return new Response();
      }
    }
  }
}