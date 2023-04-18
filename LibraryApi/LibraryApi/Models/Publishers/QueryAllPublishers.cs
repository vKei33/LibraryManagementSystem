using Marten;
using MediatR;

namespace LibraryApi.Models.Publishers
{
  public class QueryAllPublishers
  {
    public class Request : IRequest<Response> { }

    public class Response
    {
      public IEnumerable<Publisher>? Items { get; set; } = Enumerable.Empty<Publisher>();
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
        var publishers = await _session.Query<Publisher>().ToListAsync();
        var response = new Response()
        {
          Items = publishers
        };

        return response;
      }
    }
  }
}