using Microsoft.AspNetCore.Mvc;

namespace todolist.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}