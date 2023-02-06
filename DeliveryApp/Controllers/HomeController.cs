using DeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeliveryApp.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        TempData["Nome"] = "Igor Leal";
        return View();
    }
    public IActionResult Demo()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}