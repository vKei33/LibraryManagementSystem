using LibraryApi.Models.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
  [ApiController]
  [Route("api/authors")]
  public class AuthorController : ControllerBase
  {
    private readonly ISender _sender;

    public AuthorController(ISender sender)
    {
      _sender = sender;
    }

    [HttpGet("all")]
    public async Task<QueryAllAuthors.Response> QueryAll()
    {
      return await _sender.Send(new QueryAllAuthors.Request() { });
    }

    [HttpGet("{id}")]
    public async Task<QueryAuthorById.Response> QueryById([FromRoute] Guid id)
    {
      return await _sender.Send(new QueryAuthorById.Request { Id = id });
    }

    [HttpPost("create-author")]
    public async Task<CreateNewAuthor.Response> CreateNewAuthor(CreateNewAuthor.Request request)
    {
      return await _sender.Send(request);
    }

    [HttpPut("update-author/{id}")]
    public async Task<UpdateAuthor.Response> UpdateAuthor([FromRoute] Guid id, [FromBody] UpdateAuthor.Request request)
    {
      request = request ?? new UpdateAuthor.Request();
      request.Id = id;
      return await _sender.Send(request);
    }

    [HttpDelete("delete-author/{id}")]
    public async Task<DeleteAuthor.Response> DeleteAuthor([FromRoute] Guid id)
    {
      return await _sender.Send(new DeleteAuthor.Request { Id = id });
    }
  }
}