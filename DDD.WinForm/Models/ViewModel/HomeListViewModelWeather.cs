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

    public string? AreaId => _entity.AreaId.DisplayValue;
    public string? AreaName => _entity.AreaName;
    public string? DataDate => _entity.DataDate.ToString();
    public string? Condition => _entity.Condition.DisplayValue;
    public string? Temperature => _entity.Temperature.DisplayValue;
}
