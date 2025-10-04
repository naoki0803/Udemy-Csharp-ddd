using System.ComponentModel;
using DDD.Domain.Entities;
using DDD.Domain.Repositories;

namespace DDD.WinForm.Models;

public class HomeViewModel
{
    private IWeatherRepository _weather;
    private IAreaRepository _areas;

    public HomeViewModel(IWeatherRepository Weather, IAreaRepository areas)
    {
        this._weather = Weather;
        this._areas = areas;

        // Areas.Add(_areas.GetData().ToList);

        foreach (var area in _areas.GetData())
        {
            Areas.Add(new AreaEntity(area.AreaId, area.AreaName));
        }
    }

    public string? AreaId { get; set; } = string.Empty;
    public string? DataDate { get; set; } = string.Empty;
    public string? Condition { get; set; } = string.Empty;
    public string? Temperature { get; set; } = string.Empty;
    public BindingList<AreaEntity> Areas { get; set; }
    = new BindingList<AreaEntity>();
    public void Search(string? areaId)
    {
        var entity = _weather.GetLatest(Convert.ToInt32(areaId));

        if (entity != null)
        {
            AreaId = entity.AreaId.ToString();
            DataDate = entity.DataDate.ToString();
            Condition = entity.Condition.DisplayValue;
            Temperature = entity.Temperature.DisplayValue;
        }
        else
        {
            AreaId = areaId;
            DataDate = "データがありません";
            Condition = "データがありません";
            Temperature = "データがありません";
        }
    }
}
