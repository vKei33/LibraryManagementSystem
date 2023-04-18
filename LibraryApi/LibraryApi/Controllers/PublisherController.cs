using LibraryApi.Models.Publishers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
  [ApiController]
  [Route("api/publishers")]
  public class PublisherController : ControllerBase
  {
    private readonly ISender _sender;

    public PublisherController(ISender sender)
    {
      _sender = sender;
    }

    [HttpGet("all")]
    public async Task<QueryAllPublishers.Response> QueryAll()
    {
      return await _sender.Send(new QueryAllPublishers.Request() { });
    }

    [HttpGet("{id}")]
    public async Task<QueryPublisherById.Response> QueryById([FromRoute] Guid id)
    {
      return await _sender.Send(new QueryPublisherById.Request { Id = id });
    }

    [HttpPost("create-publisher")]
    public async Task<CreateNewPublisher.Response> CreateNewPublisher(CreateNewPublisher.Request request)
    {
      return await _sender.Send(request);
    }

    [HttpPut("update-publisher/{id}")]
    public async Task<UpdatePublisher.Response> UpdatePublisher([FromRoute] Guid id, [FromBody] UpdatePublisher.Request request)
    {
      request = request ?? new UpdatePublisher.Request();
      request.Id = id;
      return await _sender.Send(request);
    }

    [HttpDelete("delete-publisher/{id}")]
    public async Task<DeletePublisher.Response> DeletePublisher([FromRoute] Guid id)
    {
      return await _sender.Send(new DeletePublisher.Request { Id = id });
    }
  }
}