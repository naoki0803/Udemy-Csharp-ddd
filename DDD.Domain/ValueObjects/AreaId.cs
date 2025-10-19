using System;

namespace DDD.Domain.ValueObjects;

public sealed class AreaId : ValueObject<AreaId>
{
    public AreaId(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public string DisplayValue => Value.ToString().PadLeft(4, '0');

    protected override bool EqualsCore(AreaId other)
    {
        return Value == other.Value;
    }
}
