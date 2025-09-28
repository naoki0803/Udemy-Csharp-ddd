using System;

namespace DDD.Domain.Helpers;

public static class FloatHelper
{
    /// <summary>
    /// 小数点以下の桁数を指定して、数値を四捨五入して文字列に変換する(拡張メソッドとして定義)
    /// </summary>
    /// <param name="value">四捨五入する数値</param>
    /// <param name="decimalPlaces">小数点以下の桁数</param>
    /// <returns>四捨五入した数値の文字列</returns>
    public static string RoundString(this float value, int decimalPlaces)
    {
        return Math.Round(value, decimalPlaces).ToString("F" + decimalPlaces);
    }
}
