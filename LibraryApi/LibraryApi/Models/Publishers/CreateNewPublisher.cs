using Marten;
using MediatR;

namespace LibraryApi.Models.Publishers
{
  public class CreateNewPublisher
  {
    public class Request : IRequest<Response>
    {
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
        var item = new Publisher();
        item.Name = request.Name;

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