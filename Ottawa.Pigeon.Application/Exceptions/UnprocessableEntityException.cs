namespace Ottawa.Pigeon.Application.Exceptions;

public class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException()
        : base()
    {
    }

    public UnprocessableEntityException(string message)
        : base(message)
    {
    }
}
