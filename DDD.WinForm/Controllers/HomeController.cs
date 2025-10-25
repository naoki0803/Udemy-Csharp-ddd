using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DDD.WinForm.Models;
using DDD.WinForm.Models.ViewModel;
using DDD.Domain.Repositories;
using DDD.Domain.Exceptions;

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

    public IActionResult List()
    {
        var viewModel = new HomeListViewModel(_weatherService);
        return View(viewModel);
    }
    // 天気登録画面
    [HttpGet]
    public IActionResult Save()
    {
        var viewModel = new WeatherSaveViewModel(_weatherService, _areaService);
        return View(viewModel);
    }
    // 天気登録処理
    [HttpPost]
    public IActionResult Save(WeatherSaveViewModel viewModel)
    {
        try
        {
            viewModel.Initialize(_weatherService, _areaService);
            viewModel.Save();
            return RedirectToAction("Index");
        }
        catch (InputException ex)
        {
            // エラーメッセージをModelStateに追加
            ModelState.AddModelError(string.Empty, ex.Message);

            // 選択肢を再設定（POSTで失われるため）
            viewModel.Initialize(_weatherService, _areaService);

            return View(viewModel);
        }
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


