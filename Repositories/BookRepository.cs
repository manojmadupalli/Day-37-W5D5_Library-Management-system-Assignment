using LibraryManagementNet9.Data;
using LibraryManagementNet9.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementNet9.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(LibraryContext context) : base(context) { }

    public async Task<IEnumerable<Book>> SearchAsync(string? term, string? sort, int page, int pageSize)
    {
        var query = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(term))
        {
            var t = term.Trim().ToLower();
            query = query.Where(b => b.Title.ToLower().Contains(t) ||
                                     b.Author!.Name.ToLower().Contains(t) ||
                                     b.Genre!.Name.ToLower().Contains(t));
        }

        query = sort switch
        {
            "title_desc" => query.OrderByDescending(b => b.Title),
            "author"     => query.OrderBy(b => b.Author!.Name),
            "author_desc"=> query.OrderByDescending(b => b.Author!.Name),
            "genre"      => query.OrderBy(b => b.Genre!.Name),
            "genre_desc" => query.OrderByDescending(b => b.Genre!.Name),
            "year"       => query.OrderBy(b => b.PublishedYear),
            "year_desc"  => query.OrderByDescending(b => b.PublishedYear),
            _            => query.OrderBy(b => b.Title)
        };

        return await query
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
