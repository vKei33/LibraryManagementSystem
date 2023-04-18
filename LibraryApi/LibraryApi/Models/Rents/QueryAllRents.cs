using Marten;
using MediatR;
using SqlKata.Execution;

namespace LibraryApi.Models.Rents
{
  public class QueryAllRents
  {
    public class Request : IRequest<Response> { }

    public class Response
    {
      public IEnumerable<RentDto>? Items { get; set; } = Enumerable.Empty<RentDto>();
      public class RentDto
      {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = "";
        public Guid? MemberId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsReturned { get; set; }
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
            .Query("mt_doc_rent as rent")
            .SelectRaw($"rent.id as {nameof(QueryItem.Id)}")
            .SelectRaw($"book.id as {nameof(QueryItem.BookId)}")
            .SelectRaw($"book.title as {nameof(QueryItem.BookTitle)}")
            .SelectRaw($"member.id as {nameof(QueryItem.MemberId)}")
            .SelectRaw($"rent.first_name as {nameof(QueryItem.FirstName)}")
            .SelectRaw($"rent.last_name as {nameof(QueryItem.LastName)}")
            .SelectRaw($"rent.phone_number as {nameof(QueryItem.PhoneNumber)}")
            .SelectRaw($"rent.rental_date as {nameof(QueryItem.RentalDate)}")
            .SelectRaw($"rent.return_date as {nameof(QueryItem.ReturnDate)}")
            .SelectRaw($"rent.is_returned as {nameof(QueryItem.IsReturned)}")
            .LeftJoin("mt_doc_book as book", "book.id", "rent.book_id")
            .LeftJoin("mt_doc_member as member", "member.id", "rent.member_id")
            .OrderByDesc("rent.id");

        var results = await query.GetAsync<QueryItem>();

        var response = new Response()
        {
          Items = results.Select(x => new Response.RentDto()
          {
            Id = x.Id,
            BookId = x.BookId,
            BookTitle = x.BookTitle,
            MemberId = x.MemberId,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber,
            RentalDate = x.RentalDate,
            ReturnDate = x.ReturnDate,
            IsReturned = x.IsReturned
          }).ToList()
        };

        return response;
      }

      public class QueryItem
      {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = "";
        public Guid? MemberId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsReturned { get; set; }
      }
    }
  }
}