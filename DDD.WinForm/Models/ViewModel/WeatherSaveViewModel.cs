using DDD.Domain;
using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.Domain.ValueObjects;


namespace DDD.WinForm.Models.ViewModel;

public class WeatherSaveViewModel
{
    private IWeatherRepository? _weather;
    private IAreaRepository? _areas;

    // 引数なしコンストラクター（モデルバインディング用）
    public WeatherSaveViewModel()
    {
        // SelectedAreaId = string.Empty;
        DataDateValue = GetDateTime();
        SelectedCondition = Condition.Sunny.Value;
        TemperatureTextText = string.Empty;
        Areas = new List<AreaEntity>();
    }

    // 引数ありコンストラクター（DI用）
    public WeatherSaveViewModel(IWeatherRepository weather, IAreaRepository areas) : this()
    {
        Initialize(weather, areas);
    }

    public object? SelectedAreaId { get; set; }
    public DateTime DataDateValue { get; set; }
    public object SelectedCondition { get; set; }
    public string? TemperatureTextText { get; set; }
    public string TemperatureUnitName => Temperature.UnitName;
    public IReadOnlyList<AreaEntity> Areas { get; set; }
    public IReadOnlyList<Condition> Conditions { get; set; } = Condition.ToList();

    public virtual DateTime GetDateTime()
    {
        return DateTime.Now;
    }
    // POSTで失われた選択肢を再設定するメソッド
    public void Initialize(IWeatherRepository weather, IAreaRepository areas)
    {
        _weather = weather;
        _areas = areas;
        Areas = _areas.GetData();
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
        _weather!.Save(entity);

    }
}
