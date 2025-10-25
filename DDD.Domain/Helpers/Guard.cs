using DDD.Domain.Exceptions;

namespace DDD.Domain;

public static class Guard
{
    public static void IsNull(object? o, string message)
    {
        if (o == null)
        {
            throw new InputException(message);
        }
    }

    public static float IsFloat(string? text, string message)
    {
        float floatValue;
        if (!float.TryParse(text, out floatValue))
        {
            throw new InputException(message);
        }
        return floatValue;
    }
}
