namespace LibraryManagerProject.Data;

using Microsoft.EntityFrameworkCore;  // Required for DbContext and ModelBuilder
using LibraryManagerProject.Models;   // Required if your models (Book, Member, etc.) are in a separate namespace

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Librarian> Librarians { get; set; }

    // Constructor to use DI for DbContextOptions
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    // Configuring relationships using Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuring Loan to Book relationship
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany()
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Cascade); // Optional: set cascading behavior

        // Configuring Loan to Member relationship
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Member)
            .WithMany()
            .HasForeignKey(l => l.MemberId)
            .OnDelete(DeleteBehavior.Cascade); // Optional: set cascading behavior
    }

    // Optional: If you're not using DI for configuration, you can configure the connection string here
    // If you are using DI, remove this method.
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    // {
    //     options.UseNpgsql("Host=localhost;Database=Library;Username=postgres;Password=youssef.05.");
    // }
}