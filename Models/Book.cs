using System.ComponentModel.DataAnnotations;

namespace LibraryManagementNet9.Models;

public class Book
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int AuthorId { get; set; }
    public Author? Author { get; set; }

    [Required]
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }

    [Range(1000, 2100)]
    public int? PublishedYear { get; set; }
}
