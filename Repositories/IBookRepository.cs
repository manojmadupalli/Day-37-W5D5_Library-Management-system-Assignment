using LibraryManagementNet9.Models;

namespace LibraryManagementNet9.Repositories;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<IEnumerable<Book>> SearchAsync(string? term, string? sort, int page, int pageSize);
}
