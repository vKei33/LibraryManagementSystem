using Marten;
using MediatR;

namespace LibraryApi.Models.Publishers
{
  public class DeletePublisher
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
        var publisher = await _session.Query<Publisher>().FirstOrDefaultAsync(e => e.Id == request.Id);

        if (publisher == null)
        {
          throw new ArgumentNullException("Publisher doesn't exist.");
        }

        _session.Delete(publisher);
        await _session.SaveChangesAsync();

        return new Response();
      }
    }
  }
}