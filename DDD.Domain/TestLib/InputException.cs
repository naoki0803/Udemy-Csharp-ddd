using System;

namespace DDD.Domain.TestLib;

public sealed class InputException : Exception
{
    public InputException(string message) : base(message)
    {
    }
}
