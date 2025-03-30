namespace WebLibrary.Domain.Exceptions;

public class BadRequestException: Exception
{
    public BadRequestException(string message) : base("Bad request"){}
}