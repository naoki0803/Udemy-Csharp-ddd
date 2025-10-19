
using DDD.Domain.Repositories;

namespace DDD.WinForm.Models.ViewModel;

public class HomeListViewModel
{
    private IWeatherRepository _weather;

    public HomeListViewModel(IWeatherRepository weather)
    {
        this._weather = weather;

        foreach (var entity in _weather.GetData())
        {
            Weathers.Add(new HomeListViewModelWeather(entity));
        }
        ;
    }

    public List<HomeListViewModelWeather> Weathers { get; set; } = new List<HomeListViewModelWeather>();
}
