using Marten;
using MediatR;

namespace LibraryApi.Models.Publishers
{
  public class UpdatePublisher
  {
    public class Request : IRequest<Response>
    {
      public Guid Id { get; set; }
      public string Name { get; set; } = "";
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
        var publisher = await _session.Query<Publisher>().FirstOrDefaultAsync(e => e.Id == request.Id);

        if (publisher == null)
        {
          throw new ArgumentNullException("Publisher doesn't exist.");
        }

        publisher.Name = request.Name;

        _session.Update(publisher);
        await _session.SaveChangesAsync();

        var response = new Response()
        {
          Id = publisher.Id
        };

        return response;
      }
    }
  }
}