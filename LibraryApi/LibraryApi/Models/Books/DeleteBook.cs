using Marten;
using MediatR;

namespace LibraryApi.Models.Books
{
  public class DeleteBook
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
        var book = await _session.Query<Book>().FirstOrDefaultAsync(e => e.Id == request.Id);

        if (book == null)
        {
          throw new ArgumentNullException("Book doesn't exist.");
        }

        _session.Delete(book);
        await _session.SaveChangesAsync();

        return new Response();
      }
    }
  }
}