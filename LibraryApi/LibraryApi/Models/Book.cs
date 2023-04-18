using Marten;
using Marten.Schema;
using SqlKata.Execution;

namespace LibraryApi.Models
{
  public class Book
  {
    // private readonly QueryFactory _queryFactory;

    // public Book(QueryFactory queryFactory)
    // {
    //   _queryFactory = queryFactory;
    // }

    // public Book() { }

    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int Stock { get; set; }
    public string Language { get; set; } = "";
    public int Available
    {
      get
      {
        return Stock;
      }
    }

    [ForeignKey(typeof(Genre))]
    public Guid? GenreId { get; set; }

    [ForeignKey(typeof(Author))]
    public Guid? AuthorId { get; set; }

    [ForeignKey(typeof(Publisher))]
    public Guid? PublisherId { get; set; }

    // private int CountRentedBooks()
    // {
    //   var query = _queryFactory
    //     .Query("mt_doc_rent as rent")
    //     .Select("rent.book_id")
    //     .SelectRaw("COUNT(*) as count")
    //     .GroupByRaw("rent.book_id");

    //   var rentedBookCount = query.First();

    //   return rentedBookCount;
    // }
  }
}