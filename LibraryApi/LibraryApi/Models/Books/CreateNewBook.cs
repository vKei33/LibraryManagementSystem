using Marten;
using MediatR;

namespace LibraryApi.Models.Books
{
  public class CreateNewBook
  {
    public class Request : IRequest<Response>
    {
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
        var item = new Book();
        item.Title = request.Title;
        item.Description = request.Description;
        item.Stock = request.Stock;
        item.Language = request.Language;
        item.GenreId = request.GenreId;
        item.AuthorId = request.AuthorId;
        item.PublisherId = request.PublisherId;

        _session.Store(item);
        await _session.SaveChangesAsync();

        var response = new Response()
        {
          Id = item.Id
        };

        return response;
      }
    }
  }
}