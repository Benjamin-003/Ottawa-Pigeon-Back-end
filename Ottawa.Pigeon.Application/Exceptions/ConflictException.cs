﻿namespace Ottawa.Pigeon.Application.Exceptions;

/// <summary>
/// Gère les ecxeptions pour les conflits (409)
/// </summary>
public class ConflictException : Exception
{
    public ConflictException()
        : base()
    {
    }

    public ConflictException(string message)
        : base(message)
    {
    }

    public ConflictException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ConflictException(string name, object key)
        : base($"Entity \"{name}\" ({key}) unprocessable.")
    {
    }
}
