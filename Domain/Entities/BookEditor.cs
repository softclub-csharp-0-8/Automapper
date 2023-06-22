using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class BookEditor
{
    public BookEditor()
    {
        
    }
    public BookEditor(int modelEditorId, int modelBookIsbn)
    {
        EditorId = modelEditorId;
        BookIsbn = modelBookIsbn;
    }

    public int EditorId { get; set; }
    public int BookIsbn { get; set; }
    public DateTime EditedAt { get; set; }
    public string EditedBy { get; set; }
    public string EditedByName { get; set; }
    public Editor Editor { get; set; }
    public Book Book { get; set; }
}