using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.Models.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
  [ApiController]
  [Route("api/books")]
  public class BookController : ControllerBase
  {
    private readonly ISender _sender;

    public BookController(ISender sender)
    {
      _sender = sender;
    }

    [HttpGet("all")]
    public async Task<QueryAllBooks.Response> QueryAll()
    {
      return await _sender.Send(new QueryAllBooks.Request() { });
    }

    [HttpGet("{id}")]
    public async Task<QueryBookById.Response> QueryById([FromRoute] Guid id)
    {
      return await _sender.Send(new QueryBookById.Request { Id = id });
    }

    [HttpPost("create-book")]
    public async Task<CreateNewBook.Response> CreateNewBook([FromBody] CreateNewBook.Request request)
    {
      return await _sender.Send(request);
    }

    [HttpPut("update-book/{id}")]
    public async Task<UpdateBook.Response> UpdateBook([FromRoute] Guid id, [FromBody] UpdateBook.Request request)
    {
      request = request ?? new UpdateBook.Request();
      request.Id = id;
      return await _sender.Send(request);
    }

    [HttpDelete("delete-book/{id}")]
    public async Task<DeleteBook.Response> DeleteBook([FromRoute] Guid id)
    {
      return await _sender.Send(new DeleteBook.Request { Id = id });
    }
  }
}