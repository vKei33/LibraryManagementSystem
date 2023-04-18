using LibraryApi.Models.Genres;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
  [ApiController]
  [Route("api/genres")]
  public class GenreController : ControllerBase
  {
    private readonly ISender _sender;

    public GenreController(ISender sender)
    {
      _sender = sender;
    }

    [HttpGet("all")]
    public async Task<QueryAllGenres.Response> QueryAll()
    {
      return await _sender.Send(new QueryAllGenres.Request() { });
    }

    [HttpGet("{id}")]
    public async Task<QueryGenreById.Response> QueryById([FromRoute] Guid id)
    {
      return await _sender.Send(new QueryGenreById.Request { Id = id });
    }

    [HttpPost("create-genre")]
    public async Task<CreateNewGenre.Response> CreateNewGenre([FromBody] CreateNewGenre.Request request)
    {
      return await _sender.Send(request);
    }

    [HttpPut("update-genre/{id}")]
    public async Task<UpdateGenre.Response> UpdateGenre([FromRoute] Guid id, [FromBody] UpdateGenre.Request request)
    {
      request = request ?? new UpdateGenre.Request();
      request.Id = id;
      return await _sender.Send(request);
    }

    [HttpDelete("delete-genre/{id}")]
    public async Task<DeleteGenre.Response> DeleteGenre([FromRoute] Guid id)
    {
      return await _sender.Send(new DeleteGenre.Request { Id = id });
    }
  }
}