using Marten;
using MediatR;

namespace LibraryApi.Models.Members
{
  public class QueryMemberById
  {
    public class Request : IRequest<Response>
    {
      public Guid Id { get; set; }
    }

    public class Response
    {
      public Member? Item { get; set; }
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

        var response = new Response()
        {
          Item = member
        };

        return response;
      }
    }
  }
}