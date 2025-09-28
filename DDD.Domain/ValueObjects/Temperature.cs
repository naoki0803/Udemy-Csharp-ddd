using DDD.Domain.Helpers;
using DDD.Shared.Constants;


namespace DDD.Domain.ValueObjects;


// 自動プロパティでTemperatureを実装
public sealed class Temperature : ValueObject<Temperature>
{
    public const string UnitName = "℃";
    public const int DecimalPoint = 2;

    public Temperature(float value)
    {
        Value = value;
    }
    public float Value { get; }
    public string DisplayValue
    {
        get
        {
            return FloatHelper.RoundString(Value, DecimalPoint)
                    + " "
                    + UnitName;
        }
    }

    protected override bool EqualsCore(Temperature other)
    {
        return Value == other.Value;
    }
}




// 完全コンストラクターパターンでTemperatureを実装(極力モダンな記述かつコード量が少ない記述をする)
// public sealed class Temperature(float value)
// {
//     public float Value { get; } = value;
// }


// 普通のコンストラクターパターンでTemperatureを実装
// public sealed class Temperature
// {
//     private readonly float _value;
//     public Temperature(float value)
//     {
//         this._value = value;
//     }

//     public float Value
//     {
//         get { return _value; }
//     }
// }
