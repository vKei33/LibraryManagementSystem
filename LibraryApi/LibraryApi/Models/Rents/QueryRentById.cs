using Marten;
using MediatR;

namespace LibraryApi.Models.Rents
{
  public class QueryRentById
  {
    public class Request : IRequest<Response>
    {
      public Guid Id { get; set; }
    }

    public class Response
    {
      public Rent? Item { get; set; }
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
        var rent = await _session.Query<Rent>().FirstOrDefaultAsync(e => e.Id == request.Id);

        if (rent == null)
        {
          throw new ArgumentNullException("Rent doesn't exist.");
        }

        var response = new Response()
        {
          Item = rent
        };

        return response;
      }
    }
  }
}