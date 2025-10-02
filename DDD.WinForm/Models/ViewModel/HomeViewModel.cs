using DDD.Domain.Repositories;

namespace DDD.WinForm.Models;

public class HomeViewModel
{
    private IWeatherRepository _Weather;

    public HomeViewModel(IWeatherRepository Weather)
    {
        this._Weather = Weather;
    }

    public string? AreaId { get; set; } = string.Empty;
    public string? DataDate { get; set; } = string.Empty;
    public string? Condition { get; set; } = string.Empty;
    public string? Temperature { get; set; } = string.Empty;

    public void Search(string? areaId)
    {
        var entity = _Weather.GetLatest(Convert.ToInt32(areaId));

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
            DataDate = null;
            Condition = null;
            Temperature = null;
        }
    }
}
