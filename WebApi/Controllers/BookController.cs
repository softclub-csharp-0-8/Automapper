using System.Net;
using Domain.Entities;
using Domain.Filters;
using Domain.Wrapper;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly BookService _service;

    public BookController(BookService service)
    {
        _service = service;
    }
    
    [HttpGet("GetBook")]
    public PagedResponse<List<GetBookDto>> GetBook([FromQuery]GetBookFilter filter)
    {
        return _service.GetBook(filter);
    }

    [HttpGet("GetById")]
    public GetBookDto GetBookById(int id)
    {
        return _service.GetBookById(id);
    }

    [HttpPost("AddBook")]
    public AddBookDto AddBook(AddBookDto model)
    {
        return _service.AddBook(model);
    }

    [HttpPut("UpdateBook")]
    public async Task<IActionResult> UpdateBook(AddBookDto model)
    {
        
        var result  = await _service.UpdateBook(model);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("DeleteBook")]
    public async Task<Response<bool>> DeleteBook(int id)
    {
        return await _service.DeleteBook(id);
    }
}