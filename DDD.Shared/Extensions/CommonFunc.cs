namespace DDD.Shared.Extensions;

public static class CommonFunc
{
    public static string RoundString(float value, int decimalPlaces)
    {
        return Math.Round(value, decimalPlaces).ToString("F" + decimalPlaces);
    }
}
