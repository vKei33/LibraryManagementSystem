using Marten;
using MediatR;

namespace LibraryApi.Models.Rents
{
  public class DeleteRent
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
        var item = await _session.Query<Rent>().FirstOrDefaultAsync(e => e.Id == request.Id);

        if (item == null)
        {
          throw new ArgumentNullException("Rent doesn't exist.");
        }

        _session.Delete(item);
        await _session.SaveChangesAsync();

        return new Response();
      }
    }
  }
}