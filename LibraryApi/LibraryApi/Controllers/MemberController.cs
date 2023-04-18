using LibraryApi.Models.Members;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
  [ApiController]
  [Route("api/members")]
  public class MemberController : ControllerBase
  {
    private readonly ISender _sender;

    public MemberController(ISender sender)
    {
      _sender = sender;
    }

    [HttpGet("all")]
    public async Task<QueryAllMembers.Response> QueryAll()
    {
      return await _sender.Send(new QueryAllMembers.Request() { });
    }

    [HttpGet("{id}")]
    public async Task<QueryMemberById.Response> QueryById([FromRoute] Guid id)
    {
      return await _sender.Send(new QueryMemberById.Request { Id = id });
    }

    [HttpPost("create-member")]
    public async Task<CreateNewMember.Response> CreateNewMember(CreateNewMember.Request request)
    {
      return await _sender.Send(request);
    }

    [HttpPut("update-member/{id}")]
    public async Task<UpdateMember.Response> UpdateMember([FromRoute] Guid id, [FromBody] UpdateMember.Request request)
    {
      request = request ?? new UpdateMember.Request();
      request.Id = id;
      return await _sender.Send(request);
    }

    [HttpDelete("delete-member/{id}")]
    public async Task<DeleteMember.Response> DeleteMember([FromRoute] Guid id)
    {
      return await _sender.Send(new DeleteMember.Request { Id = id });
    }
  }
}