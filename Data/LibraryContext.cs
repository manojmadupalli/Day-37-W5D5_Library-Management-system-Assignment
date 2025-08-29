using LibraryManagementNet9.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementNet9.Data;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author!)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Genre>()
            .HasMany(g => g.Books)
            .WithOne(b => b.Genre!)
            .HasForeignKey(b => b.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed data
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "George Orwell" },
            new Author { Id = 2, Name = "Jane Austen" },
            new Author { Id = 3, Name = "J.K. Rowling" }
        );

        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Dystopian" },
            new Genre { Id = 2, Name = "Romance" },
            new Genre { Id = 3, Name = "Fantasy" }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "1984", AuthorId = 1, GenreId = 1, PublishedYear = 1949 },
            new Book { Id = 2, Title = "Pride and Prejudice", AuthorId = 2, GenreId = 2, PublishedYear = 1813 },
            new Book { Id = 3, Title = "Harry Potter and the Philosopher's Stone", AuthorId = 3, GenreId = 3, PublishedYear = 1997 }
        );
    }
}
