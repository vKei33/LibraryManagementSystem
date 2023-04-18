using Marten;
using MediatR;
using SqlKata.Execution;

namespace LibraryApi.Models.Genres
{
  public class UpdateGenre
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
        var genre = await _session.Query<Genre>().FirstOrDefaultAsync(e => e.Id == request.Id);

        if (genre == null)
          throw new ArgumentNullException("Genre doesn't exist.");

        genre.Name = request.Name;

        _session.Update(genre);
        await _session.SaveChangesAsync();

        var response = new Response()
        {
          Id = genre.Id
        };

        return response;
      }
    }
  }
}