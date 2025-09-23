using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DDD.WinForm.Models;
using DDD.WinForm.Common;
using System.Data;
using DDD.WinForm.Repositories;

namespace DDD.WinForm.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IWeatherRepository _weatherService;

    public HomeController(ILogger<HomeController> logger, IWeatherRepository weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    public IActionResult Index(int areaId)
    {
        var viewModel = new HomeViewModel(_weatherService);
        viewModel.Search(areaId.ToString());
        return View(viewModel);
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
