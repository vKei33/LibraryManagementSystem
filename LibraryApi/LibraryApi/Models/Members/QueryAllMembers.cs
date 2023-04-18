using Marten;
using MediatR;

namespace LibraryApi.Models.Members
{
  public class QueryAllMembers
  {
    public class Request : IRequest<Response> { }

    public class Response
    {
      public IEnumerable<Member>? Items { get; set; } = Enumerable.Empty<Member>();
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
        var members = await _session.Query<Member>().ToListAsync();
        var response = new Response()
        {
          Items = members
        };

        return response;
      }
    }
  }
}