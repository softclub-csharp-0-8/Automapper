namespace Domain.Filters;

public class GetBookFilter : PaginationFilter
{
    public string? Title { get; set; }

    public GetBookFilter(): base()
    {
        
    }
    public GetBookFilter(int pageNumber,int pageSize) : base(pageNumber,pageSize)
    {
        
    }
}