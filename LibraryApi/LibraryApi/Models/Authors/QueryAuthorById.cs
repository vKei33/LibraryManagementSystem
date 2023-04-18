using Marten;
using MediatR;

namespace LibraryApi.Models.Authors
{
  public class QueryAuthorById
  {
    public class Request : IRequest<Response>
    {
      public Guid Id { get; set; }
    }

    public class Response
    {
      public Author? Item { get; set; }
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
        var author = await _session.Query<Author>().FirstOrDefaultAsync(e => e.Id == request.Id);

        var response = new Response()
        {
          Item = author
        };

        return response;
      }
    }
  }
}