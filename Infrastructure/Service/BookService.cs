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
        var joined = (
                from b in _context.Books
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
            ).ToList();
        return joined;
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
        _mapper.Map(model, find);
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