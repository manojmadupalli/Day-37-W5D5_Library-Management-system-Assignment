using LibraryManagementNet9.Data;
using LibraryManagementNet9.Models;

namespace LibraryManagementNet9.Repositories;

public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    public GenreRepository(LibraryContext context) : base(context) { }
}
