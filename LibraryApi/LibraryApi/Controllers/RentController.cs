using LibraryApi.Models.Rents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
  [ApiController]
  [Route("api/rents")]
  public class RentController : ControllerBase
  {
    private readonly ISender _sender;

    public RentController(ISender sender)
    {
      _sender = sender;
    }

    [HttpGet("all")]
    public async Task<QueryAllRents.Response> QueryAll()
    {
      return await _sender.Send(new QueryAllRents.Request() { });
    }

    [HttpGet("{id}")]
    public async Task<QueryRentById.Response> QueryRentById([FromRoute] Guid id)
    {
      return await _sender.Send(new QueryRentById.Request() { Id = id });
    }

    [HttpPost("create-rent")]
    public async Task<CreateNewRent.Response> CreateNewRent([FromBody] CreateNewRent.Request request)
    {
      return await _sender.Send(request);
    }

    [HttpPut("update-rent/{id}")]
    public async Task<UpdateRent.Response> UpdateRent([FromRoute] Guid id, [FromBody] UpdateRent.Request request)
    {
      request = request ?? new UpdateRent.Request();
      request.Id = id;
      return await _sender.Send(request);
    }

    [HttpDelete("delete-rent/{id}")]
    public async Task<DeleteRent.Response> DeleteRent([FromRoute] Guid id)
    {
      return await _sender.Send(new DeleteRent.Request { Id = id });
    }
  }
}