namespace LibraryManagerProject.Models;

public class Librarian
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } // e.g., "Admin", "Staff"
}
