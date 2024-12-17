namespace LibraryManagerProject.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public DateTime PublishedDate { get; set; }
    public bool IsAvailable { get; set; }
    public string CoverImagePath { get; set; } // Path to the stored image
}
