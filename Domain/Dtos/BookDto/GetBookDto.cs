namespace Domain.Entities;

public class GetBookDto : BookBaseDto
{
    public string PublisherName { get; set; }
    public List<AuthorBaseDto> AuthorNames { get; set; }
}