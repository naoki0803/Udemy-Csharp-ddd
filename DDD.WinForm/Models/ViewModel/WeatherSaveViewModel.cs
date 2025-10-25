using DDD.Domain;
using DDD.Domain.Entities;
using DDD.Domain.Repositories;


namespace DDD.WinForm.Models.ViewModel;

public class WeatherSaveViewModel
{
    private readonly IWeatherRepository _weather;
    private readonly IAreaRepository _areas;

    public WeatherSaveViewModel(IWeatherRepository weather, IAreaRepository areas)
    {
        _weather = weather;
        _areas = areas;

        DataDateValue = GetDateTime();
        SelectedCondition = Condition.Sunny.Value;
        TemperatureTextText = string.Empty;

        Areas = _areas.GetData();
    }
    public object SelectedAreaId { get; set; }
    public DateTime DataDateValue { get; set; }
    public object SelectedCondition { get; set; }
    public string? TemperatureTextText { get; set; }
    public IReadOnlyList<AreaEntity> Areas { get; private set; }

    public IReadOnlyList<Condition> Conditions { get; private set; }
    = Condition.ToList();

    public virtual DateTime GetDateTime()
    {
        return DateTime.Now;
    }

    public void Save()
    {
        Guard.IsNull(SelectedAreaId, "AreaIdを選択してください。");
        Guard.IsNull(TemperatureTextText, "温度を入力してください。");
        // if (!float.TryParse(TemperatureTextText, out float Temperature))
        // {
        //     throw new InputException("有効な数値を入力してください");
        // }
        // 上記をGuardクラスにIsFloatとして実装した場合は↓
        var temperature = Guard.IsFloat(TemperatureTextText, "有効な数値を入力してください");

        var entity = new WeatherEntity(
                Convert.ToInt32(SelectedAreaId),
                DataDateValue,
                Convert.ToInt32(SelectedCondition),
                temperature
            );
        _weather.Save(entity);

    }
}
