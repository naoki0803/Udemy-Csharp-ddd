using DDD.Domain.Repositories;
using DDD.Shared.Constants;
using DDD.Shared.Extensions;

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
            Condition = entity.Condition.ToString();
            Temperature = CommonFunc.RoundString(Convert.ToSingle(entity.Temperature),
                            CommonConst.TemperatureDecimalPoint)
                            + " "
                            + CommonConst.TemperatureUnitName
                            ?? "データなし";
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
