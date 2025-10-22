using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DDD.WinForm.Models;
using DDD.WinForm.Models.ViewModel;
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

    [HttpPost]
    public IActionResult Save(int areaId, DateTime dataDate, int condition, float temperature)
    {
        var viewModel = new HomeViewModel(_weatherService, _areaService);
        viewModel.Save(areaId, dataDate, condition, temperature);
        return RedirectToAction("Index", new { areaId });
    }

    public IActionResult List()
    {
        var viewModel = new HomeListViewModel(_weatherService);
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


