using Marten;
using MediatR;

namespace LibraryApi.Models.Authors
{
  public class QueryAllAuthors
  {
    public class Request : IRequest<Response> { }

    public class Response
    {
      public IEnumerable<Author>? Items { get; set; } = Enumerable.Empty<Author>();
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
        var authors = await _session.Query<Author>().ToListAsync();
        var response = new Response()
        {
          Items = authors
        };

        return response;
      }
    }
  }
}