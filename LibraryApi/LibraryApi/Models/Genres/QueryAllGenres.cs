using Marten;
using MediatR;
using SqlKata.Execution;

namespace LibraryApi.Models.Genres
{
  public class QueryAllGenres
  {
    public class Request : IRequest<Response> { }

    public class Response
    {
      public IEnumerable<Genre> Items { get; set; } = Enumerable.Empty<Genre>();
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
        var genres = await _session.Query<Genre>().ToListAsync();
        var response = new Response()
        {
          Items = genres
        };

        return response;
      }
    }
  }
}