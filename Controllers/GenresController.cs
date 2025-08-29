using LibraryManagementNet9.Models;
using LibraryManagementNet9.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementNet9.Controllers;

public class GenresController : Controller
{
    private readonly IGenreRepository _genres;

    public GenresController(IGenreRepository genres) => _genres = genres;

    public async Task<IActionResult> Index()
    {
        var data = await _genres.GetAllAsync(orderBy: q => q.OrderBy(g => g.Name));
        return View(data);
    }

    public IActionResult Create() => PartialView("_GenreForm", new Genre());

    [HttpPost]
    public async Task<IActionResult> Create(Genre genre)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _genres.AddAsync(genre);
        return Json(new { success = true });
    }

    public async Task<IActionResult> Edit(int id)
    {
        var g = await _genres.GetByIdAsync(id);
        if (g is null) return NotFound();
        return PartialView("_GenreForm", g);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Genre genre)
    {
        if (id != genre.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _genres.UpdateAsync(genre);
        return Json(new { success = true });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _genres.DeleteAsync(id);
        return Json(new { success = true });
    }
}
