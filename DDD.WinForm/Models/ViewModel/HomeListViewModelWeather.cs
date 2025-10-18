using System.ComponentModel;
using DDD.Domain.Entities;

namespace DDD.WinForm.Models.ViewModel;

public sealed class HomeListViewModelWeather
{
    private WeatherEntity _entity;

    public HomeListViewModelWeather(WeatherEntity entity)
    {
        this._entity = entity;
    }
}
