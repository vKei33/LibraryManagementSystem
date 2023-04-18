using Marten;
using MediatR;

namespace LibraryApi.Models.Books
{
  public class UpdateBook
  {
    public class Request : IRequest<Response>
    {
      public Guid Id { get; set; }
      public string Title { get; set; } = "";
      public string Description { get; set; } = "";
      public int Stock { get; set; }
      public string Language { get; set; } = "";
      public Guid? GenreId { get; set; }
      public Guid? AuthorId { get; set; }
      public Guid? PublisherId { get; set; }
    }

    public class Response
    {
      public Guid Id { get; set; }
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
        var book = await _session.Query<Book>().FirstOrDefaultAsync(e => e.Id == request.Id);

        if (book == null)
        {
          throw new ArgumentNullException("Book doesn't exist.");
        }

        book.Title = request.Title;
        book.Description = request.Description;
        book.Stock = request.Stock;
        book.Language = request.Language;
        book.GenreId = request.GenreId;
        book.AuthorId = request.AuthorId;
        book.PublisherId = request.PublisherId;

        _session.Update(book);
        await _session.SaveChangesAsync();

        var response = new Response()
        {
          Id = book.Id
        };

        return response;
      }
    }
  }
}