using DDD.Shared.Constants;
using DDD.Shared.Extensions;

namespace DDD.Domain.ValueObjects;


// 自動プロパティでTemperatureを実装
public sealed class Temperature
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
            return CommonFunc.RoundString(Value, DecimalPoint)
                    + " "
                    + UnitName;
        }
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
