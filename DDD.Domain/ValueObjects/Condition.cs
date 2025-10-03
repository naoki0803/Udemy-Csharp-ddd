using DDD.Domain.ValueObjects;

namespace DDD.Domain;

public sealed class Condition : ValueObject<Condition>
{
    /// <summary>
    /// 条件の定数
    /// </summary>
    public static readonly Condition None = new Condition(0);
    /// <summary>
    /// 晴れ
    /// </summary>
    public static readonly Condition Sunny = new Condition(1);
    /// <summary>
    /// 曇り
    /// </summary>
    public static readonly Condition Cloudy = new Condition(2);
    /// <summary>
    /// 雨
    /// </summary>
    public static readonly Condition Rain = new Condition(3);

    public Condition(int value)
    {
        Value = value;
    }
    public int Value { get; }
    public string DisplayValue
    {
        get
        {
            return Value switch
            {
                0 => "不明",
                1 => "晴れ",
                2 => "曇り",
                3 => "雨",
                _ => "不明"
            };
        }
    }

    protected override bool EqualsCore(Condition other)
    {
        return Value == other.Value;
    }
}