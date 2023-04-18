using Marten;
using MediatR;
using SqlKata.Execution;

namespace LibraryApi.Models.Books
{
  public class QueryAllBooks
  {
    public class Request : IRequest<Response>
    {
    }

    public class Response
    {
      public IEnumerable<BookDto> Items { get; set; } = Enumerable.Empty<BookDto>();
      public class BookDto
      {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int Stock { get; set; }
        public int Available { get; set; }
        public string Language { get; set; } = "";
        public Guid GenreId { get; set; }
        public string GenreName { get; set; } = "";
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; } = "";
        public string AuthorLastName { get; set; } = "";
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; } = "";
      }
    }

    public class Handler : IRequestHandler<Request, Response>
    {
      private readonly IDocumentSession _session;
      private readonly QueryFactory _queryFactory;

      public Handler(IDocumentSession session, QueryFactory queryFactory)
      {
        _session = session;
        _queryFactory = queryFactory;
      }

      public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
      {
        var query = _queryFactory
            .Query("mt_doc_book as book")
            .SelectRaw($"book.id as {nameof(QueryItem.Id)}")
            .SelectRaw($"book.title as {nameof(QueryItem.Title)}")
            .SelectRaw($"book.description as {nameof(QueryItem.Description)}")
            .SelectRaw($"book.stock as {nameof(QueryItem.Stock)}")
            .SelectRaw($"book.available as {nameof(QueryItem.Available)}")
            .SelectRaw($"book.language as {nameof(QueryItem.Language)}")
            .SelectRaw($"genre.id as {nameof(QueryItem.GenreId)}")
            .SelectRaw($"genre.name as {nameof(QueryItem.GenreName)}")
            .SelectRaw($"author.id as {nameof(QueryItem.AuthorId)}")
            .SelectRaw($"author.first_name as {nameof(QueryItem.AuthorFirstName)}")
            .SelectRaw($"author.last_name as {nameof(QueryItem.AuthorLastName)}")
            .SelectRaw($"publisher.id as {nameof(QueryItem.PublisherId)}")
            .SelectRaw($"publisher.name as {nameof(QueryItem.PublisherName)}")
            .LeftJoin("mt_doc_genre as genre", "genre.id", "book.genre_id")
            .LeftJoin("mt_doc_author as author", "author.id", "book.author_id")
            .LeftJoin("mt_doc_publisher as publisher", "publisher.id", "book.publisher_id")
            .OrderByDesc("book.id");

        var results = await query.GetAsync<QueryItem>();

        var response = new Response()
        {
          Items = results.Select(x => new Response.BookDto()
          {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Stock = x.Stock,
            Available = x.Available,
            Language = x.Language,
            GenreId = x.GenreId,
            GenreName = x.GenreName,
            AuthorId = x.AuthorId,
            AuthorFirstName = x.AuthorFirstName,
            AuthorLastName = x.AuthorLastName,
            PublisherId = x.PublisherId,
            PublisherName = x.PublisherName
          }).ToList()
        };

        return response;
      }

      public class QueryItem
      {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int Stock { get; set; }
        public int Available { get; set; }
        public string Language { get; set; } = "";
        public Guid GenreId { get; set; }
        public string GenreName { get; set; } = "";
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; } = "";
        public string AuthorLastName { get; set; } = "";
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; } = "";
      }
    }
  }
}