using LibraryManagementNet9.Data;
using LibraryManagementNet9.Models;

namespace LibraryManagementNet9.Repositories;

public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
    public AuthorRepository(LibraryContext context) : base(context) { }
}
