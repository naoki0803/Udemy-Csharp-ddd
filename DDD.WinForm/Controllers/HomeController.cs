using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DDD.WinForm.Models;
using DDD.Domain.Repositories;

namespace DDD.WinForm.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IWeatherRepository _weatherService;
    private IAreaRepository _areaService;
    public HomeController(ILogger<HomeController> logger, IWeatherRepository weatherService, IAreaRepository areaService)
    {
        _logger = logger;
        _weatherService = weatherService;
        _areaService = areaService;
    }

    public IActionResult Index(int areaId = 1)
    {
        var viewModel = new HomeViewModel(_weatherService, _areaService);
        viewModel.Search(areaId.ToString());
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Test()
    {
        return View();
    }
    public IActionResult Test2()
    {
        return View();
    }
    public IActionResult Test3()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


