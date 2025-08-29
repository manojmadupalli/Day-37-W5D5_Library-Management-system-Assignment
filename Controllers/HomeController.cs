using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementNet9.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
