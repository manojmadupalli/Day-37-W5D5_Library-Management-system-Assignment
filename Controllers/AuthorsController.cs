using LibraryManagementNet9.Models;
using LibraryManagementNet9.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementNet9.Controllers;

public class AuthorsController : Controller
{
    private readonly IAuthorRepository _authors;

    public AuthorsController(IAuthorRepository authors) => _authors = authors;

    public async Task<IActionResult> Index()
    {
        var data = await _authors.GetAllAsync(orderBy: q => q.OrderBy(a => a.Name));
        return View(data);
    }

    public IActionResult Create() => PartialView("_AuthorForm", new Author());

    [HttpPost]
    public async Task<IActionResult> Create(Author author)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _authors.AddAsync(author);
        return Json(new { success = true });
    }

    public async Task<IActionResult> Edit(int id)
    {
        var a = await _authors.GetByIdAsync(id);
        if (a is null) return NotFound();
        return PartialView("_AuthorForm", a);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Author author)
    {
        if (id != author.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _authors.UpdateAsync(author);
        return Json(new { success = true });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _authors.DeleteAsync(id);
        return Json(new { success = true });
    }
}
