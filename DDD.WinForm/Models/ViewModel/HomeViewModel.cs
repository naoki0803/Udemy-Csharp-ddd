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

    public HomeViewModel Search(string? areaId)
    {
        var dt = _Weather.GetLatest(Convert.ToInt32(areaId));

        if (dt.Rows.Count > 0)
        {
            var row = dt.Rows[0];

            AreaId = areaId;
            DataDate = row["DataDate"]?.ToString() ?? "データなし";
            Condition = row["Condition"]?.ToString() ?? "データなし";
            Temperature = CommonFunc.RoundString(Convert.ToSingle(row["Temperature"]),
                            CommonConst.TemperatureDecimalPoint)
                            + " "
                            + CommonConst.TemperatureUnitName
                            ?? "データなし";
        }
        else
        {
            AreaId = areaId;
            DataDate = "データなし";
            Condition = "データなし";
            Temperature = "データなし";
        }

        return this;
    }
}
