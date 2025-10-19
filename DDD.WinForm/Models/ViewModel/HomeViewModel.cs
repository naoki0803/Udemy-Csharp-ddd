using DDD.Domain.Entities;
using DDD.Domain.Repositories;

namespace DDD.WinForm.Models;

public class HomeViewModel
{
    private readonly IWeatherRepository _weather;
    private readonly IAreaRepository _areas;

    public HomeViewModel(IWeatherRepository Weather, IAreaRepository areas)
    {
        _weather = Weather;
        _areas = areas;

        // MVCでは読み取り専用リストとして公開
        Areas = _areas.GetData();
    }

    public string? SelectedAreaIdText { get; set; } = string.Empty;
    public string? DataDateText { get; set; } = string.Empty;
    public string? ConditionText { get; set; } = string.Empty;
    public string? TemperatureText { get; set; } = string.Empty;
    // MVCでは読み取り専用で十分
    public IReadOnlyList<AreaEntity> Areas { get; private set; }

    public void Search(string? areaId)
    {
        WeatherEntity? entity = _weather.GetLatest(Convert.ToInt32(areaId));

        if (entity == null)
        {
            SelectedAreaIdText = areaId;
            DataDateText = null;
            ConditionText = null;
            TemperatureText = null;
        }
        else
        {
            SelectedAreaIdText = entity?.AreaId.Value.ToString();
            DataDateText = entity?.DataDate.ToString();
            ConditionText = entity?.Condition.DisplayValue;
            TemperatureText = entity?.Temperature.DisplayValue;
        }
    }
}
