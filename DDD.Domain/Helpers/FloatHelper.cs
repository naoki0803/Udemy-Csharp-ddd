using System;

namespace DDD.Domain.Helpers;

public static class FloatHelper
{
    public static string RoundString(float value, int decimalPlaces)
    {
        return Math.Round(value, decimalPlaces).ToString("F" + decimalPlaces);
    }
}
