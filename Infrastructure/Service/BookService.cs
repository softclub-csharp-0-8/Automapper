using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;

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

    public List<GetBookDto> GetBook()
    {
        var books = _context.Books.ToList();
        return _mapper.Map<List<GetBookDto>>(books);
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

    public AddBookDto UpdateBook(AddBookDto model)
    {
        var find = _context.Books.Find(model.Isbn);
        _mapper.Map(find, model);
        _context.SaveChanges();
        return model;
    }

    public bool DeleteBook(int id)
    {
        var find = _context.Books.Find(id);
        _context.Books.Remove(find);
        _context.SaveChanges();
        return true;
    }
}