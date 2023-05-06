using Microsoft.AspNetCore.Mvc;

namespace todolist.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}