using Marten;
using MediatR;

namespace LibraryApi.Models.Genres
{
  public class QueryGenreById
  {
    public class Request : IRequest<Response>
    {
      public Guid Id { get; set; }
    }

    public class Response
    {
      public Genre? Item { get; set; }
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
        var genre = await _session.Query<Genre>().FirstOrDefaultAsync(e => e.Id == request.Id);

        var response = new Response()
        {
          Item = genre
        };

        return response;
      }
    }
  }
}