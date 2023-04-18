using Marten;
using MediatR;

namespace LibraryApi.Models.Authors
{
  public class DeleteAuthor
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
        var author = await _session.Query<Author>().FirstOrDefaultAsync(e => e.Id == request.Id);

        if (author == null)
        {
          throw new ArgumentNullException("Author doesn't exist.");
        }

        _session.Delete(author);
        await _session.SaveChangesAsync();

        return new Response();
      }
    }
  }
}