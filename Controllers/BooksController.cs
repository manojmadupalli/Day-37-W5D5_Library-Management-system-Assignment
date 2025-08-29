using LibraryManagementNet9.Models;
using LibraryManagementNet9.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementNet9.Controllers;

public class BooksController : Controller
{
    private readonly IBookRepository _books;
    private readonly IAuthorRepository _authors;
    private readonly IGenreRepository _genres;
    private const int PageSize = 5;

    public BooksController(IBookRepository books, IAuthorRepository authors, IGenreRepository genres)
    {
        _books = books;
        _authors = authors;
        _genres = genres;
    }

    public async Task<IActionResult> Index(string? term, string? sort, int page = 1)
    {
        var items = await _books.SearchAsync(term, sort, page, PageSize);
        var total = await _books.CountAsync(b =>
            string.IsNullOrWhiteSpace(term) ||
            (b.Title.ToLower().Contains(term!.ToLower())));

        ViewBag.Term = term;
        ViewBag.Sort = sort;
        ViewBag.Page = page;
        ViewBag.TotalPages = (int)Math.Ceiling(total / (double)PageSize);

        return View(items);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Authors = await _authors.GetAllAsync(orderBy: q => q.OrderBy(a => a.Name));
        ViewBag.Genres  = await _genres.GetAllAsync(orderBy: q => q.OrderBy(g => g.Name));
        return PartialView("_BookForm", new Book());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _books.AddAsync(book);
        return Json(new { success = true, message = "Book created" });
    }

    public async Task<IActionResult> Edit(int id)
    {
        var book = await _books.GetByIdAsync(id);
        if (book is null) return NotFound();
        ViewBag.Authors = await _authors.GetAllAsync(orderBy: q => q.OrderBy(a => a.Name));
        ViewBag.Genres  = await _genres.GetAllAsync(orderBy: q => q.OrderBy(g => g.Name));
        return PartialView("_BookForm", book);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        if (id != book.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _books.UpdateAsync(book);
        return Json(new { success = true, message = "Book updated" });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _books.DeleteAsync(id);
            return Json(new { success = true, message = "Book deleted" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }
}
