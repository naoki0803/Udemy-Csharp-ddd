using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DDD.WinForm.Models;
using DDD.WinForm.Common;
using System.Data;

namespace DDD.WinForm.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(int areaId = 1)
    {
        var dt = Data.WeatherSqlite.GetLatest(Convert.ToInt32(areaId));
        return View(dt);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
