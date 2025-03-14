using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EFCoreApp.Models;

namespace EFCoreApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
