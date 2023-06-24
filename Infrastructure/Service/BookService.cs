using System.Net;
using AutoMapper;
using Domain.Entities;
using Domain.Filters;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class BookService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BookService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public PagedResponse<List<GetBookDto>> GetBook(GetBookFilter filter)
    {
        var validFilters = new PaginationFilter(filter.PageNumber, filter.PageSize);

        var books = _context.Books.AsQueryable();

        if (string.IsNullOrEmpty(filter.Title) == false)
        {
            books = books.Where(b => b.Title.ToLower().Contains(filter.Title.ToLower()));
        }

        var joined = (
                from b in books
                select new GetBookDto()
                {
                   Price = b.Price,
                   Isbn = b.Isbn,
                   Advance = b.Advance,
                   Title = b.Title,
                   Type = b.Type,
                   Ytdsales = b.Ytdsales,
                   PublisherId = b.PublisherId,
                   PublisherName = b.Publisher.Name,
                   AuthorNames = _mapper.Map<List<AuthorBaseDto>>(b.BookAuthors.Select(x=>x.Author).ToList())
                }
            ).Skip((validFilters.PageNumber - 1) * validFilters.PageSize)
             .Take(validFilters.PageSize).ToList();

        var totalRecords = books.Count();
        
        return new PagedResponse<List<GetBookDto>>(joined, totalRecords,filter.PageSize,filter.PageNumber);
    }
    
    public GetBookDto? GetBookById(int id)
    {
        var book =  _context.Books.Find(id);
        return _mapper.Map<GetBookDto>(book);
    }

    public AddBookDto AddBook(AddBookDto model)
    {
        var books = _mapper.Map<Book>(model);
        _context.Books.Add(books);
        _context.SaveChanges();
        return model;
    }

    public async Task<Response<AddBookDto>> UpdateBook(AddBookDto model)
    {
        try
        {
            var find = await _context.Books.FindAsync(model.Isbn);
            _mapper.Map(model, find);
            _context.Entry(find).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var response = _mapper.Map<AddBookDto>(find);
            return new Response<AddBookDto>(response);
        }
        catch (Exception ex)
        {
            return new Response<AddBookDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<bool>> DeleteBook(int id)
    {
        try
        {
            var find =await _context.Books.FindAsync(id);
            _context.Books.Remove(find);
            var result  = await _context.SaveChangesAsync();
            return new Response<bool>(result == 1);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}